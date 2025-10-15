using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    [Header("Carriles")]
    public int laneCount = 3;
    public float laneSpacing = 4f;                // distancia entre carriles (X)
    public int startLane = 1;                     // índice inicial (0..laneCount-1)

    [Header("Movimiento")]
    public float forwardSpeed = 8f;               // velocidad constante en +Y
    public float laneChangeSmoothTime = 0.12f;    // tiempo de suavizado para el desplazamiento X
    public float inputCooldown = 0.12f;           // evita inputs muy seguidos

    [Header("Swipe (móvil)")]
    public float swipeDeadzone = 50f;             // px mínimos para considerar swipe

    // estado interno
    private int currentLane;
    private float centerX;
    private float xVelocity = 0f;                 // usado por SmoothDamp
    private float inputTimer = 0f;

    // para swipe
    private Vector2 touchStart;
    private bool touchStarted = false;

    void Start()
    {
        // centra respecto a la posición inicial X del transform
        centerX = transform.position.x;
        // asegurar índice válido
        currentLane = Mathf.Clamp(startLane, 0, laneCount - 1);
        // llevar al carril inicial (sin "snap" visual brusco: sí, hacemos posición inmediata al Start)
        float initX = GetLaneX(currentLane);
        transform.position = new Vector3(initX, transform.position.y, transform.position.z);
    }

    void Update()
    {
        // timers para evitar inputs muy rápidos
        if (inputTimer > 0f) inputTimer -= Time.deltaTime;

        // --- Keyboard / Debug input ---
        if (inputTimer <= 0f)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                TryChangeLane(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                TryChangeLane(1);
            }
        }

        // --- Touch / Swipe input (simple) ---
        HandleTouchInput();

        // --- Movimiento ---
        // avanzar en +Y
        float newY = transform.position.y + forwardSpeed * Time.deltaTime;

        // suavizar X hacia target
        float targetX = GetLaneX(currentLane);
        float newX = Mathf.SmoothDamp(transform.position.x, targetX, ref xVelocity, laneChangeSmoothTime);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private float GetLaneX(int laneIndex)
    {
        // cálculo: centrosados alrededor de centerX
        // (laneCount - 1) * 0.5f centra el rango
        return centerX + ((laneIndex - (laneCount - 1) * 0.5f) * laneSpacing);
    }

    private void TryChangeLane(int direction)
    {
        int newLane = Mathf.Clamp(currentLane + direction, 0, laneCount - 1);
        if (newLane == currentLane) return;

        currentLane = newLane;
        // reinicia suavizado (opcional)
        // xVelocity = 0f;
        inputTimer = inputCooldown;
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 0)
        {
            touchStarted = false;
            return;
        }

        Touch t = Input.GetTouch(0);

        if (t.phase == TouchPhase.Began)
        {
            touchStarted = true;
            touchStart = t.position;
        }
        else if (touchStarted && (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Ended))
        {
            Vector2 delta = t.position - touchStart;

            // swipe horizontal dominante
            if (Mathf.Abs(delta.x) > swipeDeadzone && Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                if (delta.x > 0) TryChangeLane(1);
                else TryChangeLane(-1);

                touchStarted = false; // procesado
            }
            // si es vertical o pequeño, no hacemos nada
            if (t.phase == TouchPhase.Ended) touchStarted = false;
        }
    }

    // utilidad pública: fuerza un carril (útil desde spawners / power-ups)
    public void SetLane(int laneIndex)
    {
        currentLane = Mathf.Clamp(laneIndex, 0, laneCount - 1);
        xVelocity = 0f;
    }
}

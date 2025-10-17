using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    [Header("Carriles")]
    public int laneCount = 3;
    public float laneSpacing = 4f;
    public int startLane = 1;

    [Header("Movimiento")]
    public float forwardSpeed = 8f;
    public float laneChangeSmoothTime = 0.12f;
    public float inputCooldown = 0.12f;

    [Header("Swipe (móvil)")]
    public float swipeDeadzone = 50f;

    private int currentLane;
    private float centerX;
    private float xVelocity = 0f;
    private float inputTimer = 0f;

     
    private Vector2 touchStart;
    private bool touchStarted = false;

    private bool canMove = true;

    void Start()
    {
        centerX = transform.position.x;
        currentLane = Mathf.Clamp(startLane, 0, laneCount - 1);
        float initX = GetLaneX(currentLane);
        transform.position = new Vector3(initX, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (!canMove) return; 

        if (inputTimer > 0f) inputTimer -= Time.deltaTime;

        if (inputTimer <= 0f)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) TryChangeLane(-1);
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) TryChangeLane(1);
        }

        HandleTouchInput();

      
        float newY = transform.position.y + forwardSpeed * Time.deltaTime;
        float targetX = GetLaneX(currentLane);
        float newX = Mathf.SmoothDamp(transform.position.x, targetX, ref xVelocity, laneChangeSmoothTime);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private float GetLaneX(int laneIndex)
    {
        return centerX + ((laneIndex - (laneCount - 1) * 0.5f) * laneSpacing);
    }

    private void TryChangeLane(int direction)
    {
        int newLane = Mathf.Clamp(currentLane + direction, 0, laneCount - 1);
        if (newLane == currentLane) return;

        currentLane = newLane;
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
            if (Mathf.Abs(delta.x) > swipeDeadzone && Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                if (delta.x > 0) TryChangeLane(1);
                else TryChangeLane(-1);

                touchStarted = false;
            }
            if (t.phase == TouchPhase.Ended) touchStarted = false;
        }
    }

    public void SetLane(int laneIndex)
    {
        currentLane = Mathf.Clamp(laneIndex, 0, laneCount - 1);
        xVelocity = 0f;
    }

    
    public void StopMovement()
    {
        canMove = false;
    }
}

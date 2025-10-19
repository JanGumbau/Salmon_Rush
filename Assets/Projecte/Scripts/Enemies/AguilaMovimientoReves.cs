using UnityEngine;

public class AguilaMovimientoReves : MonoBehaviour
{
    [Header("Duración total de la trayectoria (segundos)")]
    public float duracion = 3f;

    [Header("Altura media de la V (0 = parte baja, 1 = parte alta de la cámara)")]
    public float alturaMedia = 0.5f;

    [Header("Desfase vertical respecto al borde superior (en unidades del mundo)")]
    public float offsetVertical = 2f;

    private Camera cam;
    private Vector3 puntoIzquierda;
    private Vector3 puntoMedio;
    private Vector3 puntoDerecha;
    private float t = 0f;
    private bool movimientoTerminado = false;
    private float tiempoDesaparicion = 0f; // tiempo en segundos para desaparecer
    private float tiempoDesdeFin = 0f;

    void Start()
    {
        cam = Camera.main;
        CalcularPuntosV();
        transform.position = puntoDerecha; // 👈 empieza en la derecha
    }

    void Update()
    {
        if (movimientoTerminado)
        {
            tiempoDesdeFin += Time.deltaTime;
            if (tiempoDesdeFin >= tiempoDesaparicion)
            {
                Destroy(gameObject);
            }
            return;
        }

        CalcularPuntosV(); // recalcular si la cámara se mueve
        t += Time.deltaTime / duracion;

        if (t < 0.5f)
        {
            // Baja: derecha -> medio
            float tLerp = t / 0.5f;
            transform.position = Vector3.Lerp(puntoDerecha, puntoMedio, tLerp);
        }
        else if (t < 1f)
        {
            // Sube: medio -> izquierda
            float tLerp = (t - 0.5f) / 0.5f;
            transform.position = Vector3.Lerp(puntoMedio, puntoIzquierda, tLerp);
        }
        else
        {
            transform.position = puntoIzquierda;
            movimientoTerminado = true;
        }
    }

    void CalcularPuntosV()
    {
        float camTop = cam.transform.position.y + cam.orthographicSize;
        float camBottom = cam.transform.position.y - cam.orthographicSize;

        float ySuperior = camTop + offsetVertical;
        float yMedio = Mathf.Lerp(camBottom, camTop, alturaMedia);

        float xIzquierda = cam.transform.position.x - cam.aspect * cam.orthographicSize;
        float xDerecha = cam.transform.position.x + cam.aspect * cam.orthographicSize;

        puntoIzquierda = new Vector3(xIzquierda, ySuperior, 0);
        puntoMedio = new Vector3(cam.transform.position.x, yMedio, 0);
        puntoDerecha = new Vector3(xDerecha, ySuperior, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.PerderVida();
            }
        }
    }
}

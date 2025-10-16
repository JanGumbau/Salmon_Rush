using UnityEngine;

public class AguilaMovimiento : MonoBehaviour
{
    public float duracion = 3f;  // tiempo que tarda en recorrer la V
    private Vector3 puntoIzquierda;
    private Vector3 puntoMedio;
    private Vector3 puntoDerecha;
    private float t = 0f;

    void Start()
    {
        // Coordenadas fijas
        float ySuperior = 10f;  // altura máxima en unidades del mundo
        float yMedio = 5f;      // punto medio
        float xIzquierda = -7f; // izquierda de la pantalla (ajusta según tu mapa)
        float xDerecha = 7f;    // derecha de la pantalla

        puntoIzquierda = new Vector3(xIzquierda, ySuperior, 0);
        puntoMedio = new Vector3(0f, yMedio, 0);
        puntoDerecha = new Vector3(xDerecha, ySuperior, 0);

        transform.position = puntoIzquierda;
    }


    void Update()
    {
        t += Time.deltaTime / duracion;

        if (t < 0.5f)
        {
            // Primer tramo: izquierda -> medio
            transform.position = Vector3.Lerp(puntoIzquierda, puntoMedio, t * 2f);
        }
        else
        {
            // Segundo tramo: medio -> derecha
            transform.position = Vector3.Lerp(puntoMedio, puntoDerecha, (t - 0.5f) * 2f);
        }

        if (t >= 1f) t = 0f; // reinicia ciclo para repetir
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().PerderVida();
        }
    }
}

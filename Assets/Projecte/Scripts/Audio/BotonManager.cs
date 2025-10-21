using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Necesario para Coroutines

public class BotonManager : MonoBehaviour
{
    // Asigna tu AudioSource aquí desde el Inspector
    public AudioSource audioSource;

    // Nombre de la escena a cargar
    public string nombreDeLaEscena = "NombreDeTuNuevaEscena";

    // Tiempo de retardo para que el sonido se complete
    public float tiempoDeRetardo = 0.5f;

    // Este es el método que llamarás desde el OnClick del botón
    public void ClickBoton()
    {
        // 1. Inicia la reproducción del sonido
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // 2. Inicia la Coroutine para esperar y luego cambiar de escena
        StartCoroutine(CargarEscenaConRetardo(nombreDeLaEscena, tiempoDeRetardo));
    }

    private IEnumerator CargarEscenaConRetardo(string nombreEscena, float retardo)
    {
        // Espera la cantidad de segundos especificada
        yield return new WaitForSeconds(retardo);

        // 3. Carga la nueva escena
        SceneManager.LoadScene(nombreEscena);
    }
}
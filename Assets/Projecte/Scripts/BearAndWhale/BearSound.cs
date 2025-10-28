using UnityEngine;

public class BearSound : MonoBehaviour
{
    // 1. Arrastra tu clip de sonido aquí en el Inspector
    public AudioClip bearSoundClip;

    // 2. Control para que suene solo una vez (opcional)
    private bool hasPlayed = false;

    // Esta función se llama cuando otro Collider2D entra en este trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // Si el sonido ya sonó, no hagas nada
        if (hasPlayed) return;

        // Comprueba si el objeto que entró es el "Player" (usando su Tag)
        if (other.CompareTag("Player"))
        {
            // Si es el Player y tenemos un sonido asignado
            if (bearSoundClip != null)
            {
                // Toca el sonido en la posición de la cámara
                AudioSource.PlayClipAtPoint(bearSoundClip, Camera.main.transform.position);

                // Marcamos que ya sonó, para que no suene repetidamente
                hasPlayed = true;
            }
            else
            {
                Debug.LogWarning("BearSound: ¡No se asignó ningún bearSoundClip!");
            }
        }
    }
}
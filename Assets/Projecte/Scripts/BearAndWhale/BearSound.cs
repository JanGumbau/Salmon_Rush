using UnityEngine;
using UnityEngine.Audio; // 1. ¡MUY IMPORTANTE! Necesitas esto para usar AudioMixerGroup

// 2. Esto añade un AudioSource automáticamente si no lo tienes
[RequireComponent(typeof(AudioSource))]
public class BearSound : MonoBehaviour
{
    // 3. Arrastra tu clip de sonido aquí
    public AudioClip bearSoundClip;

    // 4. ¡NUEVO! Arrastra tu AudioMixerGroup "SFX" aquí desde la ventana del Mixer
    public AudioMixerGroup sfxMixerGroup;

    // 5. Referencia al AudioSource que está en este mismo objeto
    private AudioSource audioSource;

    // Control para que suene solo una vez
    private bool hasPlayed = false;

    // Usamos Awake() para configurar la referencia
    void Awake()
    {
        // 6. Obtenemos el componente AudioSource de este GameObject
        audioSource = GetComponent<AudioSource>();

        // 7. ¡AQUÍ ESTÁ LA MAGIA! Asignamos el mixer group al AudioSource
        if (sfxMixerGroup != null)
        {
            audioSource.outputAudioMixerGroup = sfxMixerGroup;
        }
        else
        {
            Debug.LogWarning("BearSound: ¡No se asignó ningún AudioMixerGroup (SFX)!", this);
        }

        // 8. Opcional: Nos aseguramos de que no suene al empezar la escena
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasPlayed) return;

        if (other.CompareTag("Player"))
        {
            if (bearSoundClip != null)
            {
                // 9. REEMPLAZO: En lugar de PlayClipAtPoint, usamos PlayOneShot
                // Esto usará el AudioSource que ya hemos configurado con el mixer SFX
                audioSource.PlayOneShot(bearSoundClip);

                hasPlayed = true;
            }
            else
            {
                Debug.LogWarning("BearSound: ¡No se asignó ningún bearSoundClip!", this);
            }
        }
    }
}
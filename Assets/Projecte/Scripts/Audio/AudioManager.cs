using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Una referencia estática para la instancia única
    public static AudioManager instancia;

    void Awake()
    {
        // 1. Verificar si ya existe una instancia
        if (instancia == null)
        {
            // Si no existe, esta es la instancia y la conservamos
            instancia = this;

            // ¡Esta es la línea clave! Evita que se destruya al cargar una nueva escena
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe otra instancia (porque volvimos a la escena inicial)
            // se destruye este duplicado.
            Destroy(gameObject);
        }
    }

    // Opcional: método para detener la música si es necesario
    public void DetenerMusica()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Stop();
        }
        // Destruir el objeto si ya no se necesita
        Destroy(gameObject);
        instancia = null; // Liberar la referencia estática
    }
}
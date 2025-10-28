using UnityEngine;

// Este script SÍ se destruye y se recarga con la escena del Menú.
// NO le añadas DontDestroyOnLoad.
public class MainMenuUI : MonoBehaviour
{
    // Esta función la llamarás desde el botón "Play"
    public void HandlePlayButton()
    {
        // Llama al GameManager que SÍ está vivo
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
        }
    }

    // Esta función la llamarás desde el botón "Credits"
    public void HandleCreditsButton()
    {
        // Llama al GameManager
        // (Asegúrate de tener esta función "GoToCredits" en tu GameManager)
        // if (GameManager.Instance != null)
        // {
        //     GameManager.Instance.GoToCredits();
        // }

        // O simplemente maneja la lógica de la UI aquí
        Debug.Log("Botón de Créditos presionado");
    }

    // Esta función la llamarás desde el botón "Configuration"
    public void HandleConfigButton()
    {
        Debug.Log("Botón de Configuración presionado");
        // Aquí abrirías tu panel de configuración
    }

    // ... añade más funciones para cada uno de tus botones ...
}
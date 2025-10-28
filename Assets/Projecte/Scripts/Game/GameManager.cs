using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para gestionar escenas

public class GameManager : MonoBehaviour
{
    // --- EL SINGLETON ---
    // Esto permite que cualquier script llame a GameManager.Instance
    public static GameManager Instance;

    // --- ESTADOS DEL JUEGO ---
    // Usamos un 'enum' para definir los posibles estados del juego
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    // Guardamos el estado actual
    public GameState currentState;

    // --- ESTADÍSTICAS (Ejemplo) ---
    [Header("Game Stats")]
    public int score = 0;

    // Awake se llama antes que Start()
    void Awake()
    {
        // --- LÓGICA DEL SINGLETON ---
        // Si no existe ninguna instancia...
        if (Instance == null)
        {
            // ...esta se convierte en la instancia.
            Instance = this;

            // ¡Importante! Esto evita que el GameManager se destruya al cambiar de escena.
            // Así puede persistir desde el Menú hasta la escena de Juego.
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe una instancia (porque volvimos al menú),
            // destruye este *nuevo* objeto duplicado.
            Destroy(gameObject);
        }
    }

    // Start se llama una vez después de Awake
    void Start()
    {
        // Cuando el juego arranca por primera vez, asumimos que está en el menú.
        // (Esto puedes cambiarlo si tu primera escena es el juego)
        if (currentState == 0) // Si no se ha asignado estado en el Inspector
        {
            UpdateGameState(GameState.MainMenu);
        }
    }

    // --- GESTIÓN DE ESTADOS ---
    // Esta es la función principal que controla el juego
    public void UpdateGameState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                // Nos aseguramos de que el tiempo corra en el menú
                Time.timeScale = 1f;
                break;

            case GameState.Playing:
                // ¡LA SOLUCIÓN A TU PROBLEMA!
                // Al (re)iniciar el juego, nos aseguramos de que el tiempo corra.
                Time.timeScale = 1f;
                // Aquí podrías reiniciar el score, vidas, etc.
                score = 0;
                break;

            case GameState.Paused:
                // Al pausar, congelamos el tiempo.
                Time.timeScale = 0f;
                break;

            case GameState.GameOver:
                // En Game Over, el tiempo sigue corriendo (para animaciones de UI)
                Time.timeScale = 1f;
                // Aquí llamarías a tu UI para mostrar la pantalla de Game Over
                // UIManager.Instance.ShowGameOverScreen();
                break;
        }
    }


    // --- GESTIÓN DE ESCENAS ---
    // Estas son funciones públicas que puedes llamar desde tus botones de UI

    // (Asegúrate de que los nombres "GameScene" y "MainMenu"
    // coincidan con los nombres de tus escenas en Build Settings)

    public void StartGame()
    {
        // Cambia a la escena del juego
        SceneManager.LoadScene("GameScene");
        // Actualiza el estado
        UpdateGameState(GameState.Playing);
    }

    public void GoToMainMenu()
    {
        // Vuelve a la escena del menú
        SceneManager.LoadScene("MainMenu");
        // Actualiza el estado
        UpdateGameState(GameState.MainMenu);
    }

    public void RestartGame()
    {
        // Vuelve a cargar la escena que está activa ahora mismo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Actualiza el estado
        UpdateGameState(GameState.Playing);
    }

    // --- LÓGICA DE PAUSA ---

    public void TogglePause()
    {
        if (currentState == GameState.Playing)
        {
            UpdateGameState(GameState.Paused);
            // Aquí mostrarías el menú de pausa
            // UIManager.Instance.ShowPauseMenu(true);
        }
        else if (currentState == GameState.Paused)
        {
            UpdateGameState(GameState.Playing);
            // Aquí ocultarías el menú de pausa
            // UIManager.Instance.ShowPauseMenu(false);
        }
    }

    // --- FUNCIONES DEL JUEGO (Ejemplos) ---

    public void AddScore(int amount)
    {
        if (currentState != GameState.Playing) return; // No sumar puntos si no estás jugando

        score += amount;
        Debug.Log("Score: " + score);
        // Aquí llamarías a tu UI para actualizar el texto del score
        // UIManager.Instance.UpdateScoreText(score);
    }
}
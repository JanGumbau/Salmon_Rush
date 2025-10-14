using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject finishPanel;           // Panel de UI
    public Character_Controller player;      // referencia al jugador

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Detener al jugador
            player.StopMovement();

            // Mostrar panel
            finishPanel.SetActive(true);

            // Opcional: pausar juego
            // Time.timeScale = 0f;
        }
    }
}



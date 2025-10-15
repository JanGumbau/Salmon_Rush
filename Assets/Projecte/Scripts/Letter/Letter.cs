using UnityEngine;

public class Letter : MonoBehaviour
{
    // Configura en el prefab
    public char letterChar = 'S';      // la letra (A, B, C...)
    public string wordId = "EVENT1";   // identificador de conjunto/palabra
    public int letterIndex = 0;        // índice opcional (0..N-1)

    // Sonido / animación opcionales
    public AudioClip collectSfx;
    public Animator animator; // opcional

    private bool collected = false;

    void Start()
    {
        // Ajustes iniciales si necesitas (por ejemplo sprite desde letterChar)
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        // Determina colisión con el jugador
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        if (collected) return;
        collected = true;

        if (animator != null) animator.SetTrigger("Collect");
        if (collectSfx != null) AudioSource.PlayClipAtPoint(collectSfx, Camera.main.transform.position);

        // 👉 Mostrar popup arriba
        LetterPopupUI.Instance?.ShowLetter(letterChar);

        // Notificar al gestor central
        LetterManager.Instance?.RegisterCollectedLetter(wordId, letterIndex, letterChar);

        Destroy(gameObject, 0.1f);
    }

}


using UnityEngine;

public class Letter : MonoBehaviour
{
    public char letterChar = 'S';
    public string wordId = "EVENT1";
    public int letterIndex = 0;

    public SpriteRenderer spriteRenderer;
    public Sprite uncollectedSprite; // Nuevo: Sprite para letra NO recogida
    public Sprite collectedSprite;   // Nuevo: Sprite para letra YA recogida

    // Los colores se pueden mantener para efectos visuales si se desea, pero son menos cruciales ahora
    public Color collectedColor = Color.gray;
    public Color uncollectedColor = Color.white;

    public AudioClip collectSfx;
    public Animator animator;

    private bool collected = false;

    void Start()
    {
        // 1. Verificar el estado de recolección desde LetterManager
        if (LetterManager.Instance != null)
        {
            collected = LetterManager.Instance.IsLetterCollected(wordId, letterIndex);
        }

        // 2. Aplicar el sprite y color correctos al inicio
        if (spriteRenderer != null)
        {
            if (collected)
            {
                // Si ya está recogida: usar el sprite recogido, deshabilitar el collider
                spriteRenderer.sprite = collectedSprite;
                spriteRenderer.color = collectedColor;

                // Deshabilitar el Collider2D para que no pueda interactuar de nuevo
                var collider = GetComponent<Collider2D>();
                if (collider != null) collider.enabled = false;
            }
            else
            {
                // Si no está recogida: usar el sprite sin recoger
                spriteRenderer.sprite = uncollectedSprite;
                spriteRenderer.color = uncollectedColor;
            }
        }

        // Asumiendo que el animator solo tiene efectos para la recolección
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // El return si ya está collected no es estrictamente necesario si el collider se deshabilita, 
        // pero es una buena práctica de seguridad.
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        if (collected) return;
        collected = true;

        // --- Lógica de Recolección Única ---
        if (animator != null) animator.SetTrigger("Collect");
        if (collectSfx != null) AudioSource.PlayClipAtPoint(collectSfx, Camera.main.transform.position);

        LetterPopupUI.Instance?.ShowLetter(letterChar);

        // Registrar la letra recogida en el sistema
        LetterManager.Instance?.RegisterCollectedLetter(wordId, letterIndex, letterChar);

        // --- Cambios Visuales y de Interacción (Permanencia en Escena) ---
        var collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false; // Desactivar para que no se pueda coger de nuevo

        if (spriteRenderer != null)
        {
            spriteRenderer.color = collectedColor; // Cambiar color
            spriteRenderer.sprite = collectedSprite; // ¡Cambiar la imagen!
        }

        // ¡QUITAR EL DESTRUIR!
        // Antes: Destroy(gameObject, 0.1f);
        // Ahora, la letra permanece en la escena con el nuevo sprite.
    }
}
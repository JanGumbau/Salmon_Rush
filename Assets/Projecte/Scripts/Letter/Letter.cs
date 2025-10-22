using UnityEngine;

public class Letter : MonoBehaviour
{

    public char letterChar = 'S';
    public string wordId = "EVENT1";
    public int letterIndex = 0;

    public SpriteRenderer spriteRenderer;
    public Color collectedColor = Color.gray;
    public Color uncollectedColor = Color.white;


    public AudioClip collectSfx;
    public Animator animator;

    private bool collected = false;

    void Start()
    {
        if (LetterManager.Instance != null)
        {
            collected = LetterManager.Instance.IsLetterCollected(wordId, letterIndex);
        }


        if (spriteRenderer != null)
        {
            if (collected)
            {
                spriteRenderer.color = collectedColor;

            }
            else
            {
                spriteRenderer.color = uncollectedColor;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

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


        if (animator != null) animator.SetTrigger("Collect");
        if (collectSfx != null) AudioSource.PlayClipAtPoint(collectSfx, Camera.main.transform.position);


        LetterPopupUI.Instance?.ShowLetter(letterChar);

        LetterManager.Instance?.RegisterCollectedLetter(wordId, letterIndex, letterChar);


        var collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        if (spriteRenderer != null) spriteRenderer.color = collectedColor;

        Destroy(gameObject, 0.1f);
    }
}
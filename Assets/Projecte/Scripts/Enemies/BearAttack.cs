using System.Collections;
using UnityEngine;

public class BearAttack : MonoBehaviour
{
    public enum AttackSide { Left = -1, Right = 1 }

    [Header("Costat manual de la hitbox")]
    [Tooltip("Selecciona manualment cap a quin costat sortirà la hitbox.")]
    public AttackSide manualSide = AttackSide.Right;

    [Header("Hitbox")]
    [Tooltip("Mida de la hitbox d'atac.")]
    public Vector2 hitboxSize = new Vector2(1.5f, 1.0f);
    [Tooltip("Desplaçament horitzontal de la hitbox respecte al centre de l'enemic.")]
    public float sideOffset = 1.2f;
    [Tooltip("Temps (segons) que la hitbox estarà activa després d'avisar.")]
    public float hitboxActiveTime = 0.05f;

    [Header("Visualització ingame de la hitbox")]
    [Tooltip("Mostra una representació visual de la hitbox ingame.")]
    public bool showHitboxInGame = true;
    [Tooltip("Si està activat, només es mostra mentre l'atac està actiu.")]
    public bool showOnlyWhenActive = true;
    [Tooltip("Color de la hitbox visible.")]
    public Color hitboxColor = new Color(1f, 0.3f, 0.1f, 0.35f);
    [Tooltip("Order in layer del SpriteRenderer de la hitbox perquè es vegi per sobre.")]
    public int hitboxSortingOrder = 50;

    private BoxCollider2D hitbox;
    private GameObject hitboxVisualGO;
    private SpriteRenderer hitboxSR;

    private static Sprite oneUnitSquareSprite;

    private void Reset()
    {
        EnsureHitboxCollider();
        EnsureHitboxVisual();
        ApplyVisualVisibilityInitial();
        UpdateVisualTransform();
    }

    private void Awake()
    {
        EnsureHitboxCollider();
        EnsureHitboxVisual();
        ApplyVisualVisibilityInitial();
        UpdateVisualTransform();
    }

    private void OnValidate()
    {
        // Manté la vista actualitzada a l'editor i en play
        EnsureHitboxVisual();
        UpdateVisualTransform();
        if (hitboxSR != null) hitboxSR.color = hitboxColor;
    }

    private void EnsureHitboxCollider()
    {
        hitbox = GetComponent<BoxCollider2D>();
        if (hitbox == null)
            hitbox = gameObject.AddComponent<BoxCollider2D>();

        hitbox.isTrigger = true;
        hitbox.enabled = false; // només s'activa quan es crida TriggerAttack
    }

    private void EnsureHitboxVisual()
    {
        if (hitboxVisualGO == null)
        {
            // Busca un fill existent per reutilitzar
            var existing = transform.Find("HitboxVisual");
            if (existing != null)
                hitboxVisualGO = existing.gameObject;
        }

        if (hitboxVisualGO == null)
        {
            hitboxVisualGO = new GameObject("HitboxVisual");
            hitboxVisualGO.transform.SetParent(transform, false);
        }

        hitboxSR = hitboxVisualGO.GetComponent<SpriteRenderer>();
        if (hitboxSR == null)
            hitboxSR = hitboxVisualGO.AddComponent<SpriteRenderer>();

        hitboxSR.sprite = GetOrCreateOneUnitSquare();
        hitboxSR.color = hitboxColor;
        hitboxSR.sortingOrder = hitboxSortingOrder;
        hitboxSR.drawMode = SpriteDrawMode.Simple; // escala uniforme d'1x1 unitats
        hitboxSR.enabled = showHitboxInGame && !showOnlyWhenActive;
    }

    private static Sprite GetOrCreateOneUnitSquare()
    {
        if (oneUnitSquareSprite != null) return oneUnitSquareSprite;

        // Crea una textura 1x1 blanca i una Sprite d'1 unitat
        var tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        tex.SetPixel(0, 0, Color.white);
        tex.Apply();

        oneUnitSquareSprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f);
        oneUnitSquareSprite.name = "OneUnitSquareRuntime";
        return oneUnitSquareSprite;
    }

    private void ApplyVisualVisibilityInitial()
    {
        if (hitboxSR == null) return;
        hitboxSR.enabled = showHitboxInGame && !showOnlyWhenActive;
    }

    private void Update()
    {
        // Manté la visual alineada encara que es mogui l'os o canviïs paràmetres en runtime
        if (showHitboxInGame)
            UpdateVisualTransform();
    }

    private void UpdateVisualTransform()
    {
        if (hitboxVisualGO == null) return;

        int dir = (int)manualSide; // -1 esquerra, +1 dreta
        hitboxVisualGO.transform.localPosition = new Vector3(sideOffset * dir, 0f, 0f);
        hitboxVisualGO.transform.localRotation = Quaternion.identity;
        hitboxVisualGO.transform.localScale = new Vector3(hitboxSize.x, hitboxSize.y, 1f);
    }

    // Cridat pel detector: activa temporalment la hitbox
    public void TriggerAttack()
    {
        EnsureHitboxCollider();
        EnsureHitboxVisual();

        int dir = (int)manualSide; // -1 esquerra, +1 dreta
        hitbox.size = hitboxSize;
        hitbox.offset = new Vector2(sideOffset * dir, 0f);

        StopAllCoroutines();
        StartCoroutine(ActivateHitboxOnce());
    }

    private IEnumerator ActivateHitboxOnce()
    {
        // Mostra la visual si només ha d'aparèixer quan és activa
        if (showHitboxInGame && showOnlyWhenActive && hitboxSR != null)
            hitboxSR.enabled = true;

        hitbox.enabled = true;

        float t = 0f;
        while (t < hitboxActiveTime)
        {
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        hitbox.enabled = false;

        if (showHitboxInGame && showOnlyWhenActive && hitboxSR != null)
            hitboxSR.enabled = false;
    }

    // Aplicar dany quan el jugador entra a la hitbox (trigger)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.PerderVida();
            }
        }
    }

    private void OnDisable()
    {
        if (hitbox != null) hitbox.enabled = false;
        if (hitboxSR != null && showOnlyWhenActive) hitboxSR.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        int dir = (int)manualSide;
        Vector3 center = transform.position + new Vector3(sideOffset * dir, 0f, 0f);
        Vector3 size = new Vector3(hitboxSize.x, hitboxSize.y, 0.1f);

        Gizmos.color = new Color(1f, 0.5f, 0.2f, 0.2f);
        Gizmos.DrawCube(center, size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
}
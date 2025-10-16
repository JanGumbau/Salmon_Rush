using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int vidas = 3;
    private SpriteRenderer sr;           // SpriteRenderer del jugador
    public float duracionRojo = 0.1f;    // tiempo en rojo
    public float tiempoInvulnerable = 1.5f; // tiempo sin recibir daño
    private bool invulnerable = false;   // estado de invulnerabilidad

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            PerderVida();
        }
    }

    public void PerderVida()
    {
        if (invulnerable) return; // si es invulnerable, ignora daño

        vidas--;
        Debug.Log("Has perdido una vida. Vidas restantes: " + vidas);

        StartCoroutine(EfectoRojo());
        StartCoroutine(InvulnerabilidadTemporal());

        if (vidas <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("El jugador ha muerto.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator EfectoRojo()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(duracionRojo);
        sr.color = Color.white;
    }

    IEnumerator InvulnerabilidadTemporal()
    {
        invulnerable = true;

        // Parpadeo mientras es invulnerable
        float tiempo = 0f;
        while (tiempo < tiempoInvulnerable)
        {
            sr.enabled = false; // desaparece
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true; // reaparece
            yield return new WaitForSeconds(0.1f);
            tiempo += 0.2f;
        }

        invulnerable = false;
        sr.enabled = true;
    }
}

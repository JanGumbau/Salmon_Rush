using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int vidas = 3;
    private SpriteRenderer sr;  // SpriteRenderer del jugador
    public float duracionRojo = 0.02f; // tiempo en rojo

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            PerderVida();
            StartCoroutine(EfectoRojo());
        }
    }

    public void PerderVida()
    {
        vidas--;
        Debug.Log("Has perdido una vida. Vidas restantes: " + vidas);

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
        sr.color = Color.red;       // cambia a rojo
        yield return new WaitForSeconds(duracionRojo); // espera 1 segundo
        sr.color = Color.white;     // vuelve al color normal
    }
}

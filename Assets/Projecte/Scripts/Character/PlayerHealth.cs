using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int vidas = 3;
    private SpriteRenderer sr;           
    public float duracionRojo = 0.1f;    
    public float tiempoInvulnerable = 1.5f; 
    private bool invulnerable = false;   

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
        if (invulnerable) return; 

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

        
        float tiempo = 0f;
        while (tiempo < tiempoInvulnerable)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true; 
            yield return new WaitForSeconds(0.1f);
            tiempo += 0.2f;
        }

        invulnerable = false;
        sr.enabled = true;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UIVidas : MonoBehaviour
{
    public PlayerHealth playerHealth;     // referencia al script del jugador
    public Image[] corazones;             // array con las imágenes de corazones
    public Sprite corazonLleno;
    public Sprite corazonVacio;

    void Update()
    {
        // Actualiza las imágenes según las vidas
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < playerHealth.vidas)
                corazones[i].sprite = corazonLleno;
            else
                corazones[i].sprite = corazonVacio;
        }
    }
}

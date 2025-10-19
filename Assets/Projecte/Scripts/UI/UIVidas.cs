using UnityEngine;
using UnityEngine.UI;

public class UIVidas : MonoBehaviour
{
    public PlayerHealth playerHealth;    
    public Image[] corazones;             
    public Sprite corazonLleno;
    public Sprite corazonVacio;

    void Update()
    {
       
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < playerHealth.vidas)
                corazones[i].sprite = corazonLleno;
            else
                corazones[i].sprite = corazonVacio;
        }
    }
}

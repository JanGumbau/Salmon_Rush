
using UnityEngine;
using TMPro; // Necesario para TextMeshPro

public class ProgressMapController : MonoBehaviour
{
    // Arrastra el objeto de texto '25%' aquí desde el Inspector de Unity
    public GameObject text25PercentUI;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ...
            if (text25PercentUI != null)
            {
                text25PercentUI.SetActive(true);

                
                Invoke("HideText", 5f); 
            }
        }
    }

    // Si usas Invoke para ocultar el texto
    void HideText()
    {
        if (text25PercentUI != null)
        {
            text25PercentUI.SetActive(false);
            Debug.Log("Ocultando 25%.");
        }
    }

    // Opcional: Si quieres que el texto desaparezca cuando el personaje sale del trigger
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (text25PercentUI != null)
            {
                text25PercentUI.SetActive(false);
                Debug.Log("Personaje salió del trigger. Ocultando 25%.");
            }
        }
    }
}
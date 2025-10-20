using UnityEngine;

using UnityEngine.SceneManagement;

public class GestorDeEscenas : MonoBehaviour
{

    public void CargarEscena(string nombreDeLaEscena)
    {
    
        Time.timeScale = 1f;

    
        SceneManager.LoadScene(nombreDeLaEscena);
    }
}
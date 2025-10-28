using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerDeEscenario : MonoBehaviour
{
    [Header("Configuració")]
    public int sceneToLoad = 1;


    public void LoadSceneByName(string sceneName)
    {
        
        Time.timeScale = 1.0f;

        SceneManager.LoadScene(sceneName);
    }
}
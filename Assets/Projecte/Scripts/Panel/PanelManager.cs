using UnityEngine;

public class PanelManager : MonoBehaviour
{
    
    public GameObject panelDePausa;

    
    private bool estaPausado = false;

    void Start()
    {
        
        
        if (panelDePausa != null)
        {
            panelDePausa.SetActive(false);
        }
        Time.timeScale = 1f; 
        estaPausado = false;
    }

   

   
    public void AlternarPausa()
    {
        
        estaPausado = !estaPausado;

        if (estaPausado)
        {
           
            Time.timeScale = 0f;

          
            if (panelDePausa != null)
            {
                panelDePausa.SetActive(true);
            }
        }
        else
        {
       
            Time.timeScale = 1f;

           
            if (panelDePausa != null)
            {
                panelDePausa.SetActive(false);
            }
        }
    }
}
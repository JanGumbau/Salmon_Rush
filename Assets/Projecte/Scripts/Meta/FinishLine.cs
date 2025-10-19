using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject finishPanel;         
    public Character_Controller player;     

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            player.StopMovement();

           
            finishPanel.SetActive(true);

        }
    }
}



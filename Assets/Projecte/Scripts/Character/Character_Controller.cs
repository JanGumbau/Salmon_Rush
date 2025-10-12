using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    
     private float step = 2f;
    void Start()
    {
        
    }

    // Update is called once per frame

   private  void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * step;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * step;
        }
     
            
    }
}

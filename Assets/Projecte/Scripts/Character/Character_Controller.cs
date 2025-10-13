using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    //contar Carrils  i definir velocitat
    private int laneCount = 3;
    private float laneSpacing = 4f;
    public float coyoteTime = 0.25f;

    
    

    private float centerX;
    private int currentLane;
    private float targetX;
    private float coyote;

    void Start()
    {
        centerX = transform.position.x;
        currentLane = laneCount / 2; //3 / 2 = 1 -> carril central
        Snap();

    }

    // Update is called once per frame

    private void Update()
    {
        if(coyote > 0f) coyote -= Time.deltaTime; 
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            ChangeLane(-1);


        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            ChangeLane(1);
        
        
           
        

    }

    private void ChangeLane(int direction)
    {
        if(coyote > 0f) return;
        int newLane = Mathf.Clamp(currentLane + direction, 0, laneCount - 1);
        if (newLane == currentLane) return;

        currentLane = newLane;
        Snap();
        coyote = coyoteTime;

        
        
            
        
    }
    private void Snap()
    {
        
        float x = centerX + ((currentLane - (laneCount - 1) * 0.5f) * laneSpacing);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

}
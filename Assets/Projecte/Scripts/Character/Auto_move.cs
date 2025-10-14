using UnityEngine;

public class Auto_move : MonoBehaviour
{
   private enum ForwardAxis { X, Y }
   private ForwardAxis forwardAxis = ForwardAxis.Y; 
   private float speedIncreasePerSecond = 0f;

   
   private float baseSpeed = 2f;
   private float currentSpeed = 0f;
   private Rigidbody2D rb;
   
   void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        if (speedIncreasePerSecond != 0f)
            currentSpeed += speedIncreasePerSecond * Time.deltaTime;

    }
   
    void FixedUpdate()
    {
        Vector2 v = rb.linearVelocity;

        if (forwardAxis == ForwardAxis.Y)
        {
            v.y = currentSpeed;  
        }
        else
        {
            v.x = currentSpeed;  
        }
        rb.linearVelocity = v;

    }
}

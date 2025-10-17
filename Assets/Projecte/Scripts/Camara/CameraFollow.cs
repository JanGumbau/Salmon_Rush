using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Referencia al jugador")]
    public Transform player;     

    [Header("Offset")]
    public Vector3 offset = new Vector3(0, 2, -10); 

    [Header("Suavizado")]
    public float smoothSpeed = 5f; 

    void LateUpdate()
    {
        if (!player) return;

       
        Vector3 targetPos = player.position + offset;

       
        targetPos.x = transform.position.x;

        
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}

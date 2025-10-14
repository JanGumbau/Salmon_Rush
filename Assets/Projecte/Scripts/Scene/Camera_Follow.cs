using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
   public Transform target;
   private Vector3 offset = new Vector3(0, 2f, -10f);
   private float smoothSpeed = 0.125f;
   private Vector3 velocity;

   void LateUpdate()
   {
       if(target == null) return;
       
       Vector3 desired = new Vector3(target.position.x, target.position.y, transform.position.z)+ new Vector3(offset.x, offset.y, 0f);
       transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothSpeed);
   }
   
}

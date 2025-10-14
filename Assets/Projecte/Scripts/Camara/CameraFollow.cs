using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Referencia al jugador")]
    public Transform player;      // Arrastra aquí el jugador

    [Header("Offset")]
    public Vector3 offset = new Vector3(0, 2, -10); // separación respecto al jugador

    [Header("Suavizado")]
    public float smoothSpeed = 5f;  // cuánto tarda en seguir

    void LateUpdate()
    {
        if (!player) return;

        // Posición deseada
        Vector3 targetPos = player.position + offset;

        // Solo seguir Y y Z (opcional: puedes bloquear X si los carriles son fijos)
        targetPos.x = transform.position.x;

        // Movimiento suave
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}

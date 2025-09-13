// using UnityEngine;

// public class CameraFollow : MonoBehaviour
// {
//     public Transform player;
//     public Vector3 positionOffset;
//     public Vector3 rotationOffset;
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         transform.position = player.position + positionOffset;
//         transform.rotation = player.rotation * Quaternion.Euler(rotationOffset);
//     }
// }

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 positionOffset = new Vector3(0, 3, -6); // adjust in Inspector
    public Vector3 rotationOffset; // optional tilt offset
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        // Desired position
        Vector3 desiredPosition = player.position + player.TransformDirection(positionOffset);

        // Smooth movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Look at player (with optional rotation offset)
        Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = lookRotation * Quaternion.Euler(rotationOffset);
    }
}

using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Camera cam;
    public Transform playerTransform;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // transform.rotation = Quaternion.LookRotation(transform.position - playerTransform.position);
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}

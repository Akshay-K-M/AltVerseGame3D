using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]public Camera cam;
    [SerializeField] public float final_angle = 299.9f;
    [SerializeField] public float rotation_speed = 0.5f;

    public void Start()
    {
        cam.transform.position = new Vector3(16.62f, -1.4f, -7.32f);
        cam.transform.rotation = Quaternion.Euler(0f, 113.17f, 0f);
    }

    public void Update()
    {
        if (cam.transform.rotation.eulerAngles.y < final_angle)
        {
            cam.transform.Rotate(Vector3.up * rotation_speed * Time.deltaTime);
            return;
        }

        else
        {
            Debug.Log("Done");
        }


    }
}

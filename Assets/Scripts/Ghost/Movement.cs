using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public float mouseSensitivity;

    private CharacterController controller;

    // rotation state
    float pitch = 0f; // up/down rotation
    float yaw = 0f;   // left/right rotation

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Lock cursor in center of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY; // subtract to invert controls like FPS
        pitch = Mathf.Clamp(pitch, -89f, 89f); // prevent flipping upside down

        // Apply rotation
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S

        // move only on the horizontal plane (ignore eyes.up)
        Vector3 move = transform.right * x + transform.forward * z;
        move.y = 0f; // ensure no vertical movement

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}

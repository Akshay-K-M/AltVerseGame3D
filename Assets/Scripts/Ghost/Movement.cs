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
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        yaw += mouseX;

        // Apply rotation (only yaw, no pitch)
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
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

using UnityEngine;
using System.Collections;


public class MyMovement : MonoBehaviour
{
    public GameObject Ghost;
    public float moveSpeed;
    public float mouseSensitivity;
    public bool IsPossessing = false;
    private CharacterController controller;

    private Vector3 OriginalScale;

    public AudioSource possessionSound;


    // rotation state
    float pitch = 0f; // up/down rotation
    float yaw = 0f;   // left/right rotation

    Vector3 initial_pos;
    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Lock cursor in center of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OriginalScale = transform.localScale;
        // possessionSound = GetComponent<AudioSource>();
        initial_pos = Ghost.transform.position;

    }

    void Update()
    {
        HandleMouseLook();
        if (!IsPossessing)
        {
            HandleMovement();

            // --- Correct snapping to floating height ---

        }
    }

    void LateUpdate()
    {
        if (!IsPossessing)
        {
            StartCoroutine(ForceHeightAtEndOfFrame());
        }
    }

    IEnumerator ForceHeightAtEndOfFrame()
    {
        yield return new WaitForEndOfFrame(); // wait until physics/CC is done
        Vector3 pos = controller.transform.position;
        pos.y = initial_pos.y;
        controller.transform.position = pos;
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

    public void DisappearIntoFurniture()
    {
        if (!IsPossessing && possessionSound != null)
        {
            possessionSound.Play();
        }

        IsPossessing = true;
        transform.localScale = Vector3.zero;
        if (controller != null) controller.enabled = false;

    }
    public void ReappearFromFurniture()
    {
        IsPossessing = false;
        transform.localScale = OriginalScale;
        if (controller != null) controller.enabled = true;
    }
}

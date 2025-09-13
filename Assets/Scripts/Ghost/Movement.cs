using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector2 movement;
    CharacterController controller;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;
        if(direction.magnitude >= 0.1f)
        {
            controller.Move(direction * Time.deltaTime * speed);
        }
    }
}

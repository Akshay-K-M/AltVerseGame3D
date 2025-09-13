using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]public Camera cam;
    [SerializeField] public float final_angle = 299.9f;
    [SerializeField] public float rotation_speed = 0.5f;

    [SerializeField] public float focus_time_sleep = 1;

    [SerializeField] public float focus_ghost = 1;

    private float time_elapsed = 0;

    private float time_elapsed_UI = 0;

    [SerializeField]public UIEnableDisable UI;
    [SerializeField] public DialogueManager m_Dialogue;

    public void Start()
    {
        cam.transform.position = new Vector3(16.62f, -1.4f, -7.32f);
        cam.transform.rotation = Quaternion.Euler(0f, 113.17f, 0f);
        UI.HideDialogueBox();

    }

    public void Update()
    {
        if (time_elapsed < focus_time_sleep)
        {
            time_elapsed += Time.deltaTime;
        }

        else if (cam.transform.rotation.eulerAngles.y < final_angle)
        {
            cam.transform.Rotate(Vector3.up * rotation_speed * Time.deltaTime);
            // return;
        }

        else if (time_elapsed_UI < focus_ghost)
        {
            time_elapsed_UI += Time.deltaTime;
        }

        else
        {
            UI.ShowDialogueBox();
            m_Dialogue.UpdateDialogue();
            Debug.Log("Done");
        }


    }
}

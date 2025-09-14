// Create a new C# script named SceneController and use this code.

using UnityEngine;

public class SceneController : MonoBehaviour
{
    [Header("Scene References")]
    [Tooltip("Drag the GameObject that has your DialogueManager script on it here.")]
    [SerializeField] public DialogueManager m_Dialogue;
    
    [Tooltip("Drag the GameObject that has your UIEnableDisable script on it here.")]
    [SerializeField] public UIEnableDisable UI;

    void Start()
    {
        // First, make sure the dialogue box is visible.
        if (UI != null)
        {
            UI.ShowDialogueBox();
        }
        else
        {
            Debug.LogError("UI object is not assigned in the SceneController!");
            return;
        }
        
        // Then, start the dialogue immediately.
        if (m_Dialogue != null)
        {
            m_Dialogue.StartDialogue();
        }
        else
        {
            Debug.LogError("Dialogue Manager is not assigned in the SceneController!");
        }
    }
}
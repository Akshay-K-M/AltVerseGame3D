using UnityEngine;

public class UIEnableDisable : MonoBehaviour
{
    [SerializeField] public GameObject m_canvas;

    public void ShowDialogueBox()
    {
        m_canvas.SetActive(true);
    }

    public void HideDialogueBox()
    {
        m_canvas.SetActive(false);
    }
}

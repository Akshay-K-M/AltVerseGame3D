using UnityEngine;
using System.Collections;
using TMPro;


public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public float typingspeed = 0.05f;


    private string fullText;
    private Coroutine typingCouroutine;

    public void StartTyping(string textToType)
    {
        fullText = textToType;

        if (typingCouroutine != null)
        {
            StopCoroutine(typingCouroutine);
        }

        typingCouroutine = StartCoroutine(TypeText());
    }

    public IEnumerator TypeText()
    {
        textUI.text = "";
        foreach (char letter in fullText.ToCharArray())
        {
            textUI.text += letter;
            yield return new WaitForSeconds(typingspeed);
        }
    }
}

using UnityEngine;
using System.Collections;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;

    private string fullText;
    private Coroutine typingCoroutine;
    private bool isTyping;

    public TMP_Text textUI;

    // Start typing new text
    public void StartTyping(string textToType)
    {
        // stop previous coroutine if still running
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        fullText = textToType;
        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        isTyping = true;
        textUI.text = "";

        foreach (char letter in fullText.ToCharArray())
        {
            textUI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        typingCoroutine = null;
    }

    // Instantly complete current text
    public void CompleteText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        textUI.text = fullText;
        isTyping = false;
    }

    public bool IsTyping()
    {
        return isTyping;
    }
}

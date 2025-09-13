using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text dialogueText;

    [Header("Settings")]
    public float typeSpeed = 0.05f;

    [Header("Dialogue Lines")]
    public string[] lines;

    private int currentLine = 0;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    void Start()
    {
        if (lines.Length > 0)
            StartTyping(lines[currentLine]);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping)
            {
                // If still typing → instantly complete
                CompleteLine();
            }
            else
            {
                // Move to next line if available
                if (currentLine < lines.Length - 1)
                {
                    currentLine++;
                    StartTyping(lines[currentLine]);
                }
                else
                {
                    Debug.Log("Dialogue finished!");
                }
            }
        }
    }

    void StartTyping(string line)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }

    void CompleteLine()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueText.text = lines[currentLine];
        isTyping = false;
    }
}

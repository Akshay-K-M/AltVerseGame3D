using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text dialogueText;

    [Header("Settings")]
    public float typeSpeed = 0.05f;

    [Header("Dialogue Lines")]
    public string[] lines;

    [Header("Next Scene")]
    [SerializeField] public string NextScene;

    private int currentLine = 0;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    public bool IsActive = false; // tracks if dialogue is currently active

    void Update()
    {
        if (!IsActive) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping)
            {
                CompleteLine();
            }
            else
            {
                NextLine();
            }
        }
    }

    public void StartDialogue()
    {
        if (lines.Length == 0) return;

        IsActive = true;
        currentLine = 0;
        StartTyping(lines[currentLine]);
    }

    private void NextLine()
    {
        if (currentLine < lines.Length - 1)
        {
            currentLine++;
            StartTyping(lines[currentLine]);
        }
        else
        {
            EndDialogue();
        }
    }

    private void StartTyping(string line)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }

    private void CompleteLine()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueText.text = lines[currentLine];
        isTyping = false;
    }

    private void EndDialogue()
    {
        IsActive = false;
        dialogueText.text = "";
        Debug.Log("Dialogue finished!");
        SceneManager.LoadScene(NextScene);
    }
}

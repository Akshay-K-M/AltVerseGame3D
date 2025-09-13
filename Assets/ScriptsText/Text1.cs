using TMPro;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class Text1 : MonoBehaviour
{
    public TMP_Text myText;
    
    public TypewriterEffect myEffect;
    void Start()
    {
        myEffect.StartTyping(myText.text, myText);
    }
}

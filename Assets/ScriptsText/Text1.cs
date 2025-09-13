using UnityEditor.Rendering.Universal;
using UnityEngine;

public class Text1 : MonoBehaviour
{
    public TypewriterEffect myEffect;
    void Start()
    {
        myEffect.StartTyping("Hello There I am your Friendly Neighbourhood SpiderMan");
    }
}

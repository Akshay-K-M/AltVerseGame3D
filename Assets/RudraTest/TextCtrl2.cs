using UnityEngine;
using PixelBattleText;


public class TextControl2 : MonoBehaviour
{
    public TextAnimation textAnimation;

    [SerializeField]public float AnimationDuration = 3;
    [SerializeField]public string text;
    [SerializeField]public float x_pos = 0, y_pos = 0;
    private float time_elapsed;

    void Start()
    {
        time_elapsed = AnimationDuration;
    }
    void Update()
    {
        if (time_elapsed >= AnimationDuration)
        {
            PixelBattleTextController.DisplayText(text , textAnimation, new Vector2(x_pos, y_pos));
            time_elapsed = 0;
        }

        else
        {
            time_elapsed += Time.deltaTime;
        }

        
        
    }
}
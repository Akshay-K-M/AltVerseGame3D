using UnityEngine;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{
    public UnlockCursor unlockCursor;  // Assign this in the Inspector
    public GameObject gameOverScreen;

    public void restartGame()
    {
        if (unlockCursor != null)
        {
            unlockCursor.UnpauseGame();  // Resume time and cursor before restart
        }
        gameOverScreen.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}

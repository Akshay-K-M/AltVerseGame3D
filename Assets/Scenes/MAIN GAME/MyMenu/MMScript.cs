using UnityEngine;

public class MMScript : MonoBehaviour
{

    public bool isPaused = false;
    public GameObject btn;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                btn.SetActive(false);
                UnpauseGame();
                isPaused = false;
            }

            else
            {
                PauseGame();
                isPaused = true;
                btn.SetActive(true);
            }
        }
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

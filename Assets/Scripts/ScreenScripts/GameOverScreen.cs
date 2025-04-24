using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverPanel;

    void Update()
    {
        if (LevelExit.isGameOver)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0; // Zastav� hru
        }

        if (LevelExit.isGameOver && Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Obnov� hru
        SceneManager.LoadScene(0); // P�epne na level 1
    }
}

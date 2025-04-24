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
            Time.timeScale = 0; // Zastaví hru
        }

        if (LevelExit.isGameOver && Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Obnoví hru
        SceneManager.LoadScene(0); // Pøepne na level 1
    }
}

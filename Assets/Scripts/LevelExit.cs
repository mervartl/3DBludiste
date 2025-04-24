using System.Collections;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoad = 0.1f;

    private bool hasExited = false;

    public static bool isGameOver = false;

    private int currentIndex;

    void Start()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasExited && other.CompareTag("Player"))
        {
            hasExited = true;
            StartCoroutine(LoadNextLevelAfterDelay()); // StartCoroutine èeká zatímco zbytek programu mùže bìžet
        }
    }

    private IEnumerator LoadNextLevelAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoad);       

        if (currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            Key.keysCollected = 0;
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            isGameOver = true;
        }
    }

    void Update()
    {
        //Pøepínání levelù pro test
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currentIndex == SceneManager.sceneCountInBuildSettings-1)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(currentIndex + 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentIndex == 0)
            {
                SceneManager.LoadScene(3);
            }
            else
            {
                SceneManager.LoadScene(currentIndex - 1);
            }
        }
    }
}

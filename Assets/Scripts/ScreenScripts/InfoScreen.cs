using UnityEngine;
using UnityEngine.UI;

public class InfoScreen : MonoBehaviour
{
    public GameObject infoBox;
    public bool gamePaused = true;

    void Start()
    {
        Time.timeScale = 0; //Zastav� hru
        infoBox.SetActive(true);
    }

    void Update()
    {
        // Po stisknut� Enteru infobox zmiz�
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            infoBox.SetActive(false);
        }
    }
}

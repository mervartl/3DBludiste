using UnityEngine;
using UnityEngine.UI;

public class KeyCounter : MonoBehaviour
{
    public Text keyCounterText;

    private void Start()
    {
        keyCounterText.text = "Keys: " + Key.keysCollected;
    }

    void Update()
    {

        keyCounterText.text = "Keys: " + Key.keysCollected;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;
    public KeyCode toggleKey = KeyCode.F;

    public float maxBattery = 60f;
    private float currentBattery;

    public Slider batterySlider;

    private bool isOn = false;

    void Start()
    {
        flashlight.enabled = false;
        currentBattery = maxBattery;
        UpdateUI();
    }

    void Update()
    {
        // Pøepínání baterky
        if (Input.GetKeyDown(toggleKey))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }

        // Ubírání baterky
        if (isOn && currentBattery > 0f)
        {
            currentBattery -= Time.deltaTime;

            if (currentBattery <= 0f)
            {
                currentBattery = 0f;
                flashlight.enabled = false;
                isOn = false;
            }
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (batterySlider != null)
        {
            batterySlider.value = currentBattery / maxBattery;
        }
    }

    public void Recharge(float amount)
    {
        currentBattery = Mathf.Min(currentBattery + amount, maxBattery);
        UpdateUI();
    }

    public bool getBatteryState() //Kvùli enemy mechanice
    {
        return isOn;
    }
}

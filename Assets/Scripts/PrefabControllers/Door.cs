using System.Security.Permissions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f; // o kolik stup�� se m� otev��t
    [SerializeField] private float openSpeed = 2f;  // rychlost otev�r�n�

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;
    private int keysNeeded;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, transform.eulerAngles.y + openAngle, 0); // oto�en� kolem Y
        keysNeeded = SceneManager.GetActiveScene().buildIndex + 2;
    }

    void Update()
    {
        if (Key.keysCollected >= keysNeeded && !isOpen)
        {
            isOpen = true;
        }

        if (isOpen)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        }
    }
}

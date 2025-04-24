using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public FlashlightController flashlight;

    public float followDelay = 1.5f;
    public float moveSpeed = 2f;

    private Queue<Vector3> positionQueue = new Queue<Vector3>();
    private float timer = 0f;

    void Start()
    {
        // Skryj nepøítele do aktivace
        GetComponentInChildren<Renderer>().enabled = false;
        if (TryGetComponent<Collider>(out Collider col)) col.enabled = false;
    }

    void Update()
    {
        if (player == null || flashlight == null) return;

        Activate();

        float distance = Vector3.Distance(transform.position, player.position);

        timer += Time.deltaTime;
        positionQueue.Enqueue(player.position);

        // Pokud je už v bufferu dost pozic (požadovaný delay)
        if (timer >= followDelay && positionQueue.Count > 0)
        {
            Vector3 targetPos = positionQueue.Dequeue();
            targetPos.y = 0.1f;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }

        LookAtPlayer();
    }

    private void OnTriggerEnter(Collider other) // Resetuje level
    {
        if (other.CompareTag("Player") && flashlight.getBatteryState())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Activate()
    {
        GetComponentInChildren<Renderer>().enabled = true;
        if (TryGetComponent<Collider>(out Collider col)) col.enabled = true;
    }

    private void LookAtPlayer()
    {
        // Vypoèítáme smìr k hráèi (ignorujeme Y osu, aby duch neotáèel hlavu nahoru nebo dolù)
        Vector3 directionToPlayer = new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position;

        // Otoèíme ducha smìrem k hráèi
        if (directionToPlayer != Vector3.zero) // Abychom se vyhnuli dìlení nulou
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToPlayer), Time.deltaTime * 5f); // 5f je rychlost otáèení
        }
    }
}

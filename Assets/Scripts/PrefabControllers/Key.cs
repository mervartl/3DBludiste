using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static int keysCollected = 0;  // Poèet sebraných klíèù

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keysCollected++;
            Destroy(gameObject);  // Smaže klíè po sebrání
        }
    }
}


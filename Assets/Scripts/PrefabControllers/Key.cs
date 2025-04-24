using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static int keysCollected = 0;  // Po�et sebran�ch kl���

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keysCollected++;
            Destroy(gameObject);  // Sma�e kl�� po sebr�n�
        }
    }
}


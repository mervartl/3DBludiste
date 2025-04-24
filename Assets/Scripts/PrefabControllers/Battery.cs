using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public float rechargeAmount = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FlashlightController flashlight = FindObjectOfType<FlashlightController>();

            if (flashlight != null)
            {
                flashlight.Recharge(20f);
                Destroy(gameObject);
            }
        }
    }
}

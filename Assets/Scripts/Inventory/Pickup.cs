using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Attach this script onto a gameObject to make it a collectable
 */
public class Pickup : MonoBehaviour
{
    // param@ float - Amount of HP the player gains
    public static event Action<float> OnPickupHP;

    // param@ float - Amount of Ammo the player gains
    public static event Action<float> OnPickupAmmo;

    public void OnTriggerEnter(Collider other)
    {
        bool isPlayer = other.gameObject.CompareTag("Player");
        if (!isPlayer) return;

        this.gameObject.SetActive( false );

        float chance = UnityEngine.Random.Range( 0f, 100f );
        if (chance <= 50f)
        {
            float hpGain = UnityEngine.Random.Range( 10f, 40f );
            OnPickupHP?.Invoke( hpGain );
        }
        else if (chance >= 50f)
        {
            // TODO : Work with Gabriel on this one
        }
    }
}

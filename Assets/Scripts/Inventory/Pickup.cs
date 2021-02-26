using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Attach this script onto a gameObject to make it a collectable
 */
public class Pickup : MonoBehaviour
{
    public Material m1;
    public Material m2;
    int chance;
    public float timerToDespawn;

    // param@ float - Amount of HP the player gains
    public static event Action<float> OnPickupHP;

    // param@ float - Amount of Ammo the player gains
    public static event Action OnPickupAmmo;

    private void Start()
    {
        chance = UnityEngine.Random.Range(0, 2);
        if (chance == 0)
        {
            gameObject.GetComponent<Renderer>().material = m1;
            gameObject.transform.localScale *= 0.9f;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = m2;
            gameObject.transform.localScale *= 1.1f;
        }
    }

    private void Update()
    {
        // Despawn object after _ seconds
        timerToDespawn -= Time.deltaTime;
        if (timerToDespawn <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //bool isPlayer = other.gameObject.CompareTag("Player");
        //if (!isPlayer) return;

        //this.gameObject.SetActive( false );

        //float chance = UnityEngine.Random.Range( 0f, 100f );
        //if (chance <= 50f)
        //{
        //    float hpGain = UnityEngine.Random.Range( 10f, 40f );
        //    OnPickupHP?.Invoke( hpGain );
        //}
        //else if (chance >= 50f)
        //{
        //    OnPickupAmmo?.Invoke();
        //}
        if (other.gameObject.name == "Player")
        {
            if (chance == 0)
            {
                AudioManager.instance.Play("PickUpSound");
                PickUpMovement.pickUp = 2;
                float hpGain = UnityEngine.Random.Range(10f, 40f);
                OnPickupHP?.Invoke(hpGain);
                PickUpSpawn.show = true;
                Destroy(this.gameObject);
            }
            else
            {
                AudioManager.instance.Play("PickUpSound");
                PickUpMovement.pickUp = 1;
                OnPickupAmmo?.Invoke();
                PickUpSpawn.show = true;
                Destroy(this.gameObject);
            }
        }
    }
}

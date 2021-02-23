using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public int weaponNum;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (weaponNum == 1)
            {
                WeaponInfo.SGAccess = true;
                Destroy(this.gameObject);
                PickUpMovement.pickUp = 3;
                PickUpSpawn.show = true;
            }
            else if (weaponNum == 2)
            {
                WeaponInfo.SPAccess = true;
                Destroy(this.gameObject);
                PickUpMovement.pickUp = 4;
                PickUpSpawn.show = true;
            }
            else if (weaponNum == 3)
            {
                WeaponInfo.MGAccess = true;
                Destroy(this.gameObject);
                PickUpMovement.pickUp = 5;
                PickUpSpawn.show = true;
            }
        }
    }
}

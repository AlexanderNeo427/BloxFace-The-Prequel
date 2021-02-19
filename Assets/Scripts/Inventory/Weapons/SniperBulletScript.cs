using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBulletScript : MonoBehaviour
{
    public float speed; // speed of bullet
    public float maxDistance; // max dist before bullet is deleted
    private int limit = 0;
    private Vector3 dir;
    private float dmg;

    void Start()
    {
        Quaternion bulletRotation = Quaternion.Euler(0, 0, 0);
        dir = bulletRotation * transform.forward;

        transform.localRotation *= Quaternion.Euler(90, 0, 0);
        dmg = 200f;
    }

    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed, Space.World);
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= 3.0f)
        {
            Destroy(this.gameObject);
        }
        if (limit >= 3)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Unit Cube Wall(Clone)")
        {
            limit++;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            limit++;
            //Destroy(other.gameObject);
            RegularZombie regularZombie = other.gameObject.GetComponent<RegularZombie>();
            if (regularZombie != null)
            {
                regularZombie.TakeDamage( dmg );
            }

            SuicideBomberZombie suicideBomberZombie = other.gameObject.GetComponent<SuicideBomberZombie>();
            if (suicideBomberZombie != null)
            {
                suicideBomberZombie.TakeDamage(dmg);
            }

            BossZombie bossZombie = other.gameObject.GetComponent<BossZombie>();
            if (bossZombie != null)
            {
                bossZombie.TakeDamage(dmg);
            }
        }
    }


}

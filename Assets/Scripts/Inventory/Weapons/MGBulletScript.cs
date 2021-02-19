using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGBulletScript : MonoBehaviour
{
    public float speed; // speed of bullet
    public float maxDistance; // max dist before bullet is deleted
    private Vector3 dir;
    private float dmg;

    void Start()
    {
        float yRotation = Random.Range(-2, 2);

        Quaternion bulletRotatio = Quaternion.Euler(0, yRotation, 0);
        dir = bulletRotatio * transform.forward;

        transform.localRotation *= Quaternion.Euler(90, 0, 0);
        dmg = 50f;
    }

    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed, Space.World);
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= 1.9f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Unit Cube Wall(Clone)")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            //Destroy(other.gameObject);
            RegularZombie regularZombie = other.gameObject.GetComponent<RegularZombie>();
            if (regularZombie != null)
            {
                regularZombie.TakeDamage(dmg);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletScript : MonoBehaviour
{
    public float speed; // speed of bullet
    public float maxDistance; // max dist before bullet is deleted
    private Vector3 dir;
    private float dmg;

    void Start()
    {
        float yRotation = Random.Range(-40, 40);
        float xRotation = Random.Range(-4, 4);

        Quaternion bulletRotatio = Quaternion.Euler(xRotation, yRotation, 0);
        dir = bulletRotatio * transform.forward;

        transform.localRotation *= Quaternion.Euler(90, 0, 0);
        dmg = 10f;
    }

    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed, Space.World);
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= 0.1f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Unit Cube Wall(Clone)")
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            RegularZombie regularZombie = other.gameObject.GetComponent<RegularZombie>();
            if (regularZombie != null)
            {
                if (regularZombie.GetCurrentHP() > 0f)
                {
                    regularZombie.TakeDamage(dmg);
                }
            }

            SuicideBomberZombie suicideBomberZombie = other.gameObject.GetComponent<SuicideBomberZombie>();
            if (suicideBomberZombie != null)
            {
                if (suicideBomberZombie.GetCurrentHP() > 0f)
                {
                    suicideBomberZombie.TakeDamage(dmg);
                }
            }

            RunnerZombie runnerZombie = other.gameObject.GetComponent<RunnerZombie>();
            if (runnerZombie != null)
            {
                if (runnerZombie.GetCurrentHP() > 0f)
                {
                    runnerZombie.TakeDamage(dmg);
                }
            }

            BossZombie bossZombie = other.gameObject.GetComponent<BossZombie>();
            if (bossZombie != null)
            {
                if (bossZombie.GetCurrentHP() > 0f)
                {
                    bossZombie.TakeDamage(dmg);
                }
            }
            Destroy(this.gameObject);
        }
    }
}

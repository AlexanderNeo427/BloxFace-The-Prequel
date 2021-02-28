using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed; // speed of bullet
    public float maxDistance; // max dist before bullet is deleted
    private Vector3 dir;
    private float dmg;

    void Start()
    {
        float yRotation = Random.Range(-1, 1);

        Quaternion bulletRotation = Quaternion.Euler(0, yRotation, 0);
        dir = bulletRotation * transform.forward;

        transform.localRotation *= Quaternion.Euler(90, 0, 0);
        dmg = 18f; // Damage should be 18f
    }

    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed, Space.World);
        maxDistance += Time.deltaTime;

        if (maxDistance >= 0.9f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            // Updated dmg given
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

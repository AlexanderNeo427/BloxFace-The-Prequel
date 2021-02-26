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
        dmg = 20f; // Damage should be 20f
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

            RunnerZombie runnerZombie = other.gameObject.GetComponent<RunnerZombie>();
            if (runnerZombie != null)
            {
                runnerZombie.TakeDamage(dmg);
            }

            BossZombie bossZombie = other.gameObject.GetComponent<BossZombie>();
            if (bossZombie != null)
            {
                bossZombie.TakeDamage(dmg);
            }
            Destroy(this.gameObject);
        }
    }
}

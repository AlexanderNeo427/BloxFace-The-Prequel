using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletScript : MonoBehaviour
{
    public float speed; // speed of bullet
    public float maxDistance; // max dist before bullet is deleted
    private Vector3 dir;

    void Start()
    {
        float yRotation = Random.Range(-10, 10);
        float xRotation = Random.Range(-5, 5);

        Quaternion bulletRotatio = Quaternion.Euler(xRotation, yRotation, 0);
        dir = bulletRotatio * Vector3.forward;
    }

    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed);
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
    }
}

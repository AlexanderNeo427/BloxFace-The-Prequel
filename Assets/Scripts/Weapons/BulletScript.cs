using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed; // speed of bullet
    public float maxDistance; // max dist before bullet is deleted
    private Vector3 dir;

    void Start()
    {
        float yRotation = Random.Range(-1, 1);

        Quaternion bulletRotatio = Quaternion.Euler(0, yRotation, 0);
        dir = bulletRotatio * Vector3.forward;
    }

    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed);
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= 10f)
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

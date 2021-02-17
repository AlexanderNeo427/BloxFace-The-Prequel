using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBulletScript : MonoBehaviour
{
    public float speed; // speed of bullet
    public float maxDistance; // max dist before bullet is deleted
    private int limit = 0;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= 2.0f)
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
    }
}

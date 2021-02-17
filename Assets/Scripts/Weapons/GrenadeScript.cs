using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    public float delay;
    private float countdown;
    private bool hasExploded;
    public GameObject explosionObject;
    public float blastRadius;
    public float explforce;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
        hasExploded = false;
        rb = GetComponent<Rigidbody>();
        //blastRadius = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Unit Cube Wall(Clone)")
        {
            rb.AddForce(-transform.forward * 5, ForceMode.Impulse);
        }
    }

    void Explode()
    {
        Instantiate(explosionObject, transform.position, transform.rotation);

        Collider[] collider = Physics.OverlapSphere(transform.position, blastRadius);
        
        foreach(Collider nearbyObject in collider)
        {
            if (nearbyObject.name == "Unit Cube Wall(Clone)")
            {
                Destroy(nearbyObject.gameObject);
            }
        }
        Destroy(gameObject);
    }
}
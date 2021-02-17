using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    public float throwForce;
    public GameObject grenade;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && PlayerInfo.grenadeAmount > 0)
        {
            ThrowGrenade();
            PlayerInfo.grenadeAmount--;
        }
    }
    void ThrowGrenade()
    {
        GameObject grnd = Instantiate(grenade, transform.position, transform.rotation);
        Rigidbody rb = grnd.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        //grnd.transform.Translate(Vector3.forward * Time.deltaTime * throwForce);
    }
}

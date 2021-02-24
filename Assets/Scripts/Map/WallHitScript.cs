using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHitScript : MonoBehaviour
{
    private bool sfx;
    // Start is called before the first frame update
    void Start()
    {
        sfx = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sfx)
        {
            //GetComponent<AudioSource>().Play();
            sfx = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            sfx = true;
        }
    }

}

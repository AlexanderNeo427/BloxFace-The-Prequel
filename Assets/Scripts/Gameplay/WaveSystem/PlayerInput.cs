using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject WSystem;

    private GameObject[] GOs;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        GOs = GameObject.FindGameObjectsWithTag("Enemy");

        if (Input.GetKeyDown("space"))
        {
            foreach (GameObject go in GOs)
                Destroy(go);
        }
            
    }
}

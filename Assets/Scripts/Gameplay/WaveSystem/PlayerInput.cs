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
            {
                if (go.GetComponent<RegularZombie>())
                {
                    go.GetComponent<RegularZombie>().TakeDamage(500);
                }
                else if (go.GetComponent<RunnerZombie>())
                {
                    go.GetComponent<RunnerZombie>().TakeDamage(500);
                }
                else if (go.GetComponent<SuicideBomberZombie>())
                {

                    go.GetComponent<SuicideBomberZombie>().TakeDamage(500);
                }
                else if (go.GetComponent<BossZombie>())
                {
                    go.GetComponent<BossZombie>().TakeDamage(500);
                }
            }
        }
            
    }
}

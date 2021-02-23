using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawn : MonoBehaviour
{
    public GameObject pickUp;
    public static bool show = false;
    public static int showNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (show)
        {
            Spawn();
            showNum++;
            show = false;
        }
    }

    void Spawn()
    {
        Vector3 pos = gameObject.transform.position;
        //pos.y = gameObject.transform.position.y - 200;
        GameObject pu = Instantiate(pickUp, new Vector3(0, -200, 0), Quaternion.identity) as GameObject;
        pu.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }


}

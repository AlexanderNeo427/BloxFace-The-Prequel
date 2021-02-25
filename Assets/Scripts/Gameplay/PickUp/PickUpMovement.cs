using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpMovement : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    public static int pickUp = 0;
    private float del = 1f;
    private float MaxTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        if (pickUp == 1)
        {
            textMesh.text = "Ammo picked up!";
        }
        else if (pickUp == 2)
        {
            textMesh.text = "Health picked up!";
        }
        else if (pickUp == 3)
        {
            textMesh.text = "ShotGun Picked Up!";
        }
        else if (pickUp == 4)
        {
            textMesh.text = "SniperRifle Picked Up!";
        }
        else if (pickUp == 5)
        {
            textMesh.text = "MiniGun Picked Up!";
        }
        else
        {
            textMesh.text = "_";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PickUpSpawn.showNum >= 2)
        {
            MaxTime = 0.375f;
        }
        else
        {
            MaxTime = 1f;
        }
        LeanTween.moveLocalY(gameObject, -300f, MaxTime);
        if (del <= 0)
        {
            Destroy(gameObject);
            PickUpSpawn.showNum--;
        }
        del -= Time.deltaTime;
        if (PickUpSpawn.showNum >= 2)
        {
            del -= Time.deltaTime;
        }
    }
}

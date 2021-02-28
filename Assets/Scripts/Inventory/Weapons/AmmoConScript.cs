using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AmmoConScript : MonoBehaviour
{
    public GameObject pistolGO;
    public GameObject shotgunGO;
    public GameObject sniperGO;
    public GameObject MGGO;
    TextMeshProUGUI txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = gameObject.GetComponent<TextMeshProUGUI>();
        txt.text = "?";
    }

    // Update is called once per frame
    void Update()
    {
        if (pistolGO.activeSelf)
        {
            txt.text = " ";
        }
        else if (shotgunGO.activeSelf)
        {
            txt.text = "12"; // Shotgun Takes 12 ammo
        }
        else if (sniperGO.activeSelf)
        {
            txt.text = "6"; // Sniper takes 6 ammo
        }
        else if (MGGO.activeSelf)
        {
            txt.text = "1";
        }
    }
}

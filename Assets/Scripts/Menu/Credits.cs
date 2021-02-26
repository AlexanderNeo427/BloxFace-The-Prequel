using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public bool CreditIsActive = false;

    public Button EnableCreditsButton;
    public Button DisableCreditsButton;

    public GameObject CreditsUI;

    void Start()
    {
        Button btn1 = EnableCreditsButton.GetComponent<Button>();
        Button btn2 = DisableCreditsButton.GetComponent<Button>();
        btn1.onClick.AddListener(TaskOnClick);
        btn2.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if (CreditIsActive)
        {
            EnableCredits();
        }
        else
        {
            DisableCredits();
        }
    }

    public void EnableCredits()
    {
        CreditsUI.SetActive(true);
        Time.timeScale = 1f;
        CreditIsActive = true;
    }

    public void DisableCredits()
    {
        CreditsUI.SetActive(false);
        Time.timeScale = 0f;
        CreditIsActive = false;
    }
}

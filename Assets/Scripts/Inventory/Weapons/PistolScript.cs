using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private GameObject bulletSpawnPoint;
    [SerializeField] private GameObject bullet;

    [Header("Customisations")]
    [SerializeField] private float waitTime;

    private float m_waitTime;

    private void Start()
    {
        m_waitTime = waitTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] [Range (0.1f, 0.8f)]
    private float lifeTime = 0.3f;

    // private float
    private float m_lifeTime;

    private void OnEnable()
    {
        m_lifeTime = lifeTime;
    }

    private void Update()
    {
        m_lifeTime -= Time.deltaTime;

        if (m_lifeTime <= 0f)
            this.gameObject.SetActive( false );
    }
}

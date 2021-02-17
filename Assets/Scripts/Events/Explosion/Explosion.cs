using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] [Range (0.1f, 0.8f)]
    private float lifeTime = 0.3f;

    private void Start()
    {
        Destroy( this.gameObject, lifeTime );
    }
}

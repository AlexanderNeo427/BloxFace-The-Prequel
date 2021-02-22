using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("Customisations")]

    [SerializeField] [Range(3f, 8f)]
    private float lifeTime;

    [SerializeField] [Range (8f, 50f)]
    private float speed;

    [SerializeField] [Range(10f, 30f)]
    private float damage;

    public float Damage { get { return damage; } }

    private void Start()
    {
        Destroy( this.gameObject, lifeTime );
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInfo playerInfo = other.gameObject.GetComponent<PlayerInfo>();
            playerInfo.TakeDamage( damage );

            Destroy( this.gameObject );
        }
        else if (other.gameObject.CompareTag("Teammate"))
        {
            TeammateController teammateController = null;
            teammateController = other.gameObject.GetComponent<TeammateController>();
            teammateController.TakeDamage( damage );

            Destroy( this.gameObject );
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Destroy( this.gameObject );
        }
    }
}

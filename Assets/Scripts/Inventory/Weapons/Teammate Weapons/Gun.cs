using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  All concrete gun types should have 
 *  an instance of Gun
 */
[CreateAssetMenu (fileName = "New gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    [SerializeField] 
    private new string name;

    [SerializeField] 
    private int maxAmmo;

    [SerializeField] [Tooltip ("Time between shots")]
    private float fireRate;

    [SerializeField] 
    private int bulletsPerShot;

    [SerializeField] 
    private float range;

    [SerializeField] [Tooltip ("How long the gun takes to reload (in seconds)")]
    private float reloadTime;

    [SerializeField] [Tooltip ("How far the bullet spray will deviate (in degrees)")]
    private float spread;

    [SerializeField] [Tooltip ("How good the gun is (Higher -> Better)")]
    private int rating;

    [SerializeField]
    private GameObject gunPrefab;

    public string     Name           => name;
    public int        MaxAmmo        => maxAmmo;
    public float      FireRate       => fireRate;
    public int        BulletsPerShot => bulletsPerShot;
    public float      Range          => range;
    public float      ReloadTime     => reloadTime;
    public float      Spread         => spread;
    public int        Rating         => rating;
    public GameObject GunPrefab      => gunPrefab;
}

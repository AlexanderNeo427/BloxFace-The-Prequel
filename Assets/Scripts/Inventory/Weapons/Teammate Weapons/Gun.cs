using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "New gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    [SerializeField] 
    private new string name;

    [SerializeField] 
    private int maxAmmo;

    [SerializeField] [Tooltip ("Rounds fired per second")]
    private int fireRate;

    [SerializeField] 
    private int bulletsPerShot;

    [SerializeField] 
    private float range;

    [SerializeField] [Tooltip ("How long the gun takes to reload (in seconds)")]
    private float reloadTime;

    [SerializeField] [Tooltip ("How far the bullet spray will deviate (in degrees)")]
    private float spread;

    [SerializeField] [Tooltip ("How good the gun is (Higher -> better)")]
    private int rating;

    [SerializeField]
    private GameObject gunModel;

    public string Name => name;
    public int MaxAmmo => maxAmmo;
    public int FireRate => fireRate;
    public int BulletsPerShot => bulletsPerShot;
    public float Range => range;
    public float ReloadTIme => reloadTime;
    public float Spread => spread;
    public int Rating => rating;
    public GameObject GunModel => gunModel;
}

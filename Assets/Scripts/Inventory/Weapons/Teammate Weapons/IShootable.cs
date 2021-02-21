using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Interface for all guns 
 */
public interface IShootable
{
    int  GetAmmo();
    int  GetMaxAmmo();
    void Reload();
    void Shoot();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Interface for concrete gun types to implement
 */
public interface IShootable
{
    int  GetMaxAmmo();
    void Reload();
    void Shoot();
}

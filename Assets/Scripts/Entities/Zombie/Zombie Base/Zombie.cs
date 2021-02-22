using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  Base zombie class for all concrete zombie types to inherit from 
 */
public interface Zombie
{
    void TakeDamage(float dmg);
    void Attack();

    GameObject GetGameObject();
}

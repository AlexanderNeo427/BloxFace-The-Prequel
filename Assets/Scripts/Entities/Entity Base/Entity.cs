using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Base entity interface for all 
 *  entities to inherit from
 */
public interface Entity 
{
    float GetMaxHP();
    float GetCurrentHP();
}

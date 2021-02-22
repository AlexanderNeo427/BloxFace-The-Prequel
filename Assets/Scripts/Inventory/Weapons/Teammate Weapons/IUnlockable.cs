using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnlockable
{
    bool IsUnlocked();
    void UnlockWeapon();
}

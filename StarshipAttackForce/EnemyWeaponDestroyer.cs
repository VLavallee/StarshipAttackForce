using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponDestroyer : MonoBehaviour
{
    public void DestroyThisWeapon()
    {
        Destroy(gameObject);
    }
}

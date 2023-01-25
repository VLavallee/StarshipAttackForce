using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverSaucer : MonoBehaviour
{
    [SerializeField] GameObject[] specialSaucerProtectionProjectiles;

    public void ActivateSaucerProjectiles()
    {
        foreach (GameObject projectile in specialSaucerProtectionProjectiles)
        {
            projectile.SetActive(true);
        }
    }
}

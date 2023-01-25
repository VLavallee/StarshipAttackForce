using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSaucerProtectionProjectile : MonoBehaviour
{
    [SerializeField] GameObject saucerProjectileToDeactivate;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        saucerProjectileToDeactivate.SetActive(false);
    }
}

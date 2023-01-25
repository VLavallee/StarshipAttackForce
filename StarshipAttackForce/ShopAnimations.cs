using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAnimations : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void HopperDown()
    {
        animator.SetTrigger("HopperDown");
    }

    public void HopperUp()
    {
        animator.SetTrigger("HopperUp");
    }
}

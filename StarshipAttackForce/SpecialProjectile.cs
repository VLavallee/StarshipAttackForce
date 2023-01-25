using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialProjectile : MonoBehaviour
{
    [SerializeField] bool createOnHit, createPlayerObject, createEnemyObject;
    [SerializeField] bool destroysWeapons = false;
    [SerializeField] bool isTrigger = true;
    public CameraShake cameraShake;
    ObjectPooler objectPooler;
    [SerializeField] string createdObjectName;
    private void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        objectPooler = FindObjectOfType<ObjectPooler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isTrigger)
        {
            if (destroysWeapons)
            {
                if (collision.gameObject.CompareTag("Enemy Weapon"))
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    private void OnDisable()
    {
        if(createOnHit)
        {
            if(createEnemyObject)
            {
                EnableEnemyObject();
            }
            if(createPlayerObject)
            {
                EnablePlayerObject();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isTrigger) { return; }
        if (destroysWeapons)
        {
            if (collision.gameObject.CompareTag("Enemy Weapon"))
            {
                Destroy(collision.gameObject);
            }
        }
        
    }
    public void EnablePlayerObject()
    {
        //var newCreateOnHitObject = Instantiate(createOnHitObject, transform.position, Quaternion.identity);
        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(createdObjectName,transform.position, Quaternion.identity);
        projectile.SetActive(true);
    }
    public void EnableEnemyObject()
    {
        GameObject projectile = objectPooler.SpawnFromPoolEnemyEffect(createdObjectName, transform.position, Quaternion.identity);
        projectile.SetActive(true);
    }
}

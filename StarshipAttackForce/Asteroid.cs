using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] bool playerHasDestroyedAsteroid = false;
    [SerializeField] int pointsPerKill = 1000;
    [SerializeField] float health = 100;
    [SerializeField] GameObject destructionFX;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] AudioClip destructionSFX;
    [SerializeField] [Range(0, 1)] float destructionSoundVolume = 0.7f;
    [SerializeField] PowerupDispenser powerupDispenser;
    
    [SerializeField] bool includeWarning = false;
    [SerializeField] GameObject warningObject;
    [SerializeField] Transform warningTransformPosition;

    private void Start()
    {
        powerupDispenser = GetComponent<PowerupDispenser>();
        if(includeWarning)
        {
            GameObject warning = Instantiate(warningObject, transform.position, Quaternion.identity);
            warning.transform.position = new Vector2(transform.position.x, warningTransformPosition.position.y);
            Destroy(warning, 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player Laser") || collision.gameObject.CompareTag("Enemy Weapon"))
        {
            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            HandleHit(damageDealer);
        }
    }

    public void HandleHit(DamageDealer damageDealer)
    {
        HitEffect();
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject explosion = Instantiate(destructionFX, transform.position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
        if (FindObjectOfType<SoundCenter>().CheckEnemyDamageSoundCooldown())
        {
            AudioSource.PlayClipAtPoint(destructionSFX, Camera.main.transform.position, destructionSoundVolume);
        }
        
        playerHasDestroyedAsteroid = true;
        powerupDispenser.hasBeenDestroyedByPlayer = true;
        Destroy(gameObject);
    }

    private void Idle()
    {
        GetComponent<Animator>().SetTrigger("Idle");
    }

    private void HitEffect()
    {
        if (GetComponent<Animator>() == null) { return; }

        GetComponent<Animator>().SetTrigger("HitEffect");
    }

    private void OnDestroy()
    {
        if (playerHasDestroyedAsteroid)
        {
            FindObjectOfType<GameSession>().AddToScore(pointsPerKill);
            FindObjectOfType<GameSession>().KillsPlusOne();
        }
    }
}

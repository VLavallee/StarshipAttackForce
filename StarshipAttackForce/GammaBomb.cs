using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaBomb : MonoBehaviour
{
    [SerializeField] GameObject gammaBombExplosion;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    public void Explode()
    {
        GameObject explosion = Instantiate(gammaBombExplosion, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Explode();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitEffect : MonoBehaviour
{
    [SerializeField] GameObject bulletHitEffectPrefab;
    [SerializeField] SpriteRenderer theRenderer;
    [SerializeField] List<Color> colors;
    [SerializeField] float minScale, maxScale;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            GameObject hitEffect = Instantiate(bulletHitEffectPrefab, transform.position, Quaternion.identity);
            var colorRoll = Random.Range(0, colors.Count - 1);
            hitEffect.GetComponent<SpriteRenderer>().color = colors[colorRoll];
            var scaleRoll = Random.Range(minScale, maxScale);
            hitEffect.GetComponent<Transform>().localScale = new Vector2(scaleRoll, scaleRoll);
            Destroy(hitEffect, .15f);
        }
    }
}

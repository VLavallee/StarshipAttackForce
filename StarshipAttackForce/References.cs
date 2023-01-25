using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour
{
    // REFERENCE SCRIPTS

    
    // RAYCASTING WITH LINE RENDERER IN FOREACH LOOP OF COROUTINE
    // 1
    [SerializeField] Transform[] laserCannonPositions;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] AudioClip shootSound;
    [SerializeField] float shootSoundVolume;
    bool canShoot;
    int laserDamage;

    // COROUTINE SYNTAX 
    // 2
    bool looping;
    float delayTime;


    // RAYCASTING WITH LINE RENDERER IN FOREACH LOOP OF COROUTINE
    // 1
    IEnumerator FireLaserCannons()
    {
        if (canShoot)
        {
            foreach (Transform cannons in laserCannonPositions)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(cannons.position, -cannons.transform.up);
                if (hitInfo)
                {
                    Debug.Log(hitInfo.transform.name);
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        // call damage method here
                    }

                    lineRenderer.SetPosition(0, cannons.position);
                    lineRenderer.SetPosition(1, hitInfo.point);
                }
                else
                {
                    lineRenderer.SetPosition(0, cannons.position);
                    lineRenderer.SetPosition(1, cannons.position + cannons.forward * 100);
                }
                lineRenderer.enabled = true;
                yield return new WaitForSeconds(1f);
                lineRenderer.enabled = false;
            }
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
        }
    }

    // COROUTINE SYNTAX
    // 2
    IEnumerator ExampleCoroutineNameHere()
    {
        //option to do something here
        yield return new WaitForSeconds(1);
        //option to do something here
    }

    IEnumerator ExampleCoroutineNameAlsoHere()
    {
        //option to do something here
        yield return StartCoroutine(ExampleCoroutineNameHere());
        //option to do something here
    }

    IEnumerator DoWhileCoroutine()
    {
        do
        {
            yield return StartCoroutine(ExampleCoroutineNameHere());
            if (looping)
            {
                yield return new WaitForSeconds(delayTime);
                // do something here
            }
        }
        while (looping);

    }

}

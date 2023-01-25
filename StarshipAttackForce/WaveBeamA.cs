using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBeamA : MonoBehaviour
{
    [SerializeField] GameObject waveBeamB;
    [SerializeField] float timeSinceInstantiation;
    [SerializeField] float timeLimit;
    

    private void Start()
    {
        timeLimit = FindObjectOfType<EnemyBoss>().GetWaveBeamATimeLimit();
        timeSinceInstantiation = 0;
    }

    void Update()
    {
        timeSinceInstantiation += Time.deltaTime;
        if(timeSinceInstantiation >= timeLimit)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject beamB = Instantiate(waveBeamB, transform.position, Quaternion.identity);
        if(FindObjectOfType<EnemyBoss>() == null)
        {
            Destroy(beamB);
            return;
        }
        beamB.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -FindObjectOfType<EnemyBoss>().waveBeamBVelocity);
    }
}

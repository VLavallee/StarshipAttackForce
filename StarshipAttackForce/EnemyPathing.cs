using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    [SerializeField] List<GameObject> autoTurrets;
    int waypointIndex = 0;
    [SerializeField] bool isDestroyingAtPathEnd = false;
    [SerializeField] bool isStoppingAtPathEnd = false;
    [SerializeField] Vector3 direction;
    private Rigidbody2D rb;
    [SerializeField] float calculatedAngle;
    [SerializeField] bool isFlexible;
    [SerializeField] public bool canMove = true;
    [SerializeField] float currentMoveSpeed;
    [SerializeField] bool isEnemy, isBoss;
    BossAndEnemyLevelTracker bossAndEnemyLevelTracker;

    private void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
        rb = GetComponent<Rigidbody2D>();
        bossAndEnemyLevelTracker = FindObjectOfType<BossAndEnemyLevelTracker>();
    }

    private void Update()
    {
        if(canMove)
        {
            Move();
        }
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            direction = transform.position - waypoints[waypointIndex].position;

            // these 3 lines of code are what allows the enemy ships to curve when following the path
            // first and second line convert the direction Vector into an angle. The - 90 deg is used because the math function assumes the object is facing right
            // which it is not, so using - 90 deg corrects this
            // the 3rd line rotates the rigidbody to the calculated angle. 
            // NOTE ** in order for MoveRotation to work smoothly, the interpolation setting on the rigidbody must be set to "interpolate" or the rotation will happen instantly,
            // showing no change from using the function rb.rotate(calculatedAngle) 
            if(isFlexible)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                calculatedAngle = angle - 90f;
                rb.MoveRotation(calculatedAngle);
            }
            if(isEnemy)
            {
                var movementThisFrame = bossAndEnemyLevelTracker.GetEnemySpeed() * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
                
            }
            if (isBoss)
            {
                var movementThisFrame = bossAndEnemyLevelTracker.GetBossSpeed() * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            }

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            // set this bool on if you want the wave config to send the next wave of enemy once the enemy has reached the end of the path
            if(isDestroyingAtPathEnd)
            {
                FindObjectOfType<EnemyTracker>().SubtractEnemyFromScene();
                Destroy(gameObject);
            }

            if(isStoppingAtPathEnd)
            {
                canMove = false;
                if(isBoss)
                {
                    if(FindObjectOfType<EnemyBossHealth>())
                    {
                        FindObjectOfType<EnemyBossHealth>().bossIsInvulnerable = false;
                        
                    }
                    if(FindObjectOfType<AutoMovement>())
                    {
                        FindObjectOfType<AutoMovement>().isMovable = true;
                    }
                    if(FindObjectOfType<BattleCruiser>())
                    {
                        FindObjectOfType<BattleCruiser>().canShoot = true;
                    }
                    if(FindObjectOfType<Dragonfly>())
                    {
                        FindObjectOfType<Dragonfly>().canShoot = true;
                    }
                    if (GetComponentInChildren<CirculonFiring>())
                    {
                        GetComponentInChildren<CirculonFiring>().TurnShootingOn();
                        GetComponentInChildren<CirculonDrone>().DroneCanShoot();
                        GetComponentInChildren<CirculonDrone>().RemoveInvulnerability();
                    }
                    if(GetComponent<SpinnorShooting>())
                    {
                        GetComponent<SpinnorShooting>().canShoot = true;
                    }
                    if(GetComponent<EnemyStealthBomber>())
                    {
                        GetComponent<EnemyStealthBomber>().canShoot = true;
                    }
                    if(GetComponent<AdditionalShooting>())
                    {
                        GetComponent<AdditionalShooting>().TurnShootingOn();
                    }
                    if(GetComponent<Supercruiser>())
                    {
                        GetComponent<Supercruiser>().canShoot = true;
                    }
                }
            }
            
            if(GetComponent<EnemyAutoTurret>() != null)
            {
                GetComponent<EnemyAutoTurret>().canShoot = true;
            }
            
            if(autoTurrets != null)
            {
                foreach(GameObject turret in autoTurrets)
                {
                    if(turret.GetComponent<EnemyAutoTurret>())
                    {
                        turret.GetComponent<EnemyAutoTurret>().canShoot = true;
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossHealthBar : MonoBehaviour
{
    [SerializeField] SpriteRenderer healthBarRenderer;
    [SerializeField] private Animator anim;
    [SerializeField] private EnemyBossHealth enemyBossHealth;
    [SerializeField] float bossHealth;
    [SerializeField] float healthBarValue;
    [SerializeField] float maxHealth;
    RandomPathEnemySpawner randomPathEnemySpawner;
    private float ninetyFive, ninety, eightyFive, eighty, seventyFive, seventy, sixtyFive, sixty, fiftyFive, fifty, fourtyFive, fourty, thirtyFive, thirty, twentyFive, twenty, fifteen, ten, five, zero;
    void Start()
    {
        anim = GetComponent<Animator>();
        enemyBossHealth = GetComponentInParent<EnemyBossHealth>();
        randomPathEnemySpawner = FindObjectOfType<RandomPathEnemySpawner>();
        bossHealth = enemyBossHealth.bossHealth;
        maxHealth = bossHealth;
        ConfigureBasePercentageNumbers();
        healthBarRenderer.enabled = false;
    }
    private void Update()
    {
        bossHealth = enemyBossHealth.bossHealth;
        CalculateHealthBarValue();
        ShowEnemyHealth();
    }

    void CalculateHealthBarValue()
    {
        if (bossHealth / maxHealth >= .95f && bossHealth != maxHealth)
        {
            healthBarValue = 10;
        }
        if (bossHealth / maxHealth >= .9f && bossHealth / maxHealth < .95f)
        {
            healthBarValue = 9.5f;
        }
        if (bossHealth / maxHealth >= .85f && bossHealth / maxHealth < .9f)
        {
            healthBarValue = 9;
        }
        if (bossHealth / maxHealth >= .8f && bossHealth / maxHealth < .85f)
        {
            healthBarValue = 8.5f;
        }
        if (bossHealth / maxHealth >= .75f && bossHealth / maxHealth < .8f)
        {
            healthBarValue = 8;
        }
        if (bossHealth / maxHealth >= .7f && bossHealth / maxHealth < .75f)
        {
            healthBarValue = 7.5f;
        }
        if (bossHealth / maxHealth >= .65f && bossHealth / maxHealth < .7f)
        {
            healthBarValue = 7;
        }
        if (bossHealth / maxHealth >= .60f && bossHealth / maxHealth < .65f)
        {
            healthBarValue = 6.5f;
        }
        if (bossHealth / maxHealth >= .55f && bossHealth / maxHealth < .6f)
        {
            healthBarValue = 6;
        }
        if (bossHealth / maxHealth >= .50f && bossHealth / maxHealth < .55f)
        {
            healthBarValue = 5.5f;
        }
        if (bossHealth / maxHealth >= .45f && bossHealth / maxHealth < .5f)
        {
            healthBarValue = 5;
        }
        if (bossHealth / maxHealth >= .40f && bossHealth / maxHealth < .45f)
        {
            healthBarValue = 4.5f;
        }
        if (bossHealth / maxHealth >= .35f && bossHealth / maxHealth < .40f)
        {
            healthBarValue = 4f;
        }
        if (bossHealth / maxHealth >= .30f && bossHealth / maxHealth < .35f)
        {
            healthBarValue = 3.5f;
        }
        if (bossHealth / maxHealth >= .25f && bossHealth / maxHealth < .30f)
        {
            healthBarValue = 3f;
        }
        if (bossHealth / maxHealth >= .2f && bossHealth / maxHealth < .25f)
        {
            healthBarValue = 2.5f;
        }
        if (bossHealth / maxHealth >= .15f && bossHealth / maxHealth < .2f)
        {
            healthBarValue = 2f;
        }
        if (bossHealth / maxHealth >= .1f && bossHealth / maxHealth < .15f)
        {
            healthBarValue = 1.5f;
        }
        if (bossHealth / maxHealth >= .05f && bossHealth / maxHealth < .1f)
        {
            healthBarValue = 1f;
        }
        if (bossHealth / maxHealth >= 0 && bossHealth / maxHealth < .05f)
        {
            healthBarValue = .5f;
        }
        if(bossHealth / maxHealth <= 0)
        {
            healthBarValue = 0;
            healthBarRenderer.enabled = false;
        }
    }

    void ConfigureBasePercentageNumbers()
    {
        ninetyFive = .95f;
        ninety = .9f;
        eightyFive = .85f;
        eighty = .8f;
        seventyFive = .75f;
        seventy = .7f;
        sixtyFive = .65f;
        sixty = .6f;
        fiftyFive = .55f;
        fifty = .5f;
        fourtyFive = .45f;
        fourty = .4f;
        thirtyFive = .35f;
        thirty = .3f;
        twentyFive = .25f;
        twenty = .2f;
        fifteen = .15f;
        ten = .1f;
        five = .05f;
        zero = 0f;
    }

    void ShowEnemyHealth()
    {
        if (bossHealth == maxHealth)
        {
            healthBarRenderer.enabled = false;
        }
        if (bossHealth > 0 && bossHealth != maxHealth)
        {
            healthBarRenderer.enabled = true;
        }
        switch (healthBarValue)
        {
            case 10:
                anim.SetTrigger("HB100");
                break;
            case 9.5f:
                anim.SetTrigger("HB95");
                break;
            case 9:
                anim.SetTrigger("HB90");
                break;
            case 8.5f:
                anim.SetTrigger("HB85");
                break;
            case 8:
                anim.SetTrigger("HB80");
                break;
            case 7.5f:
                anim.SetTrigger("HB75");
                break;
            case 7:
                anim.SetTrigger("HB70");
                break;
            case 6.5f:
                anim.SetTrigger("HB65");
                break;
            case 6:
                anim.SetTrigger("HB60");
                break;
            case 5.5f:
                anim.SetTrigger("HB55");
                break;
            case 5:
                anim.SetTrigger("HB50");
                break;
            case 4.5f:
                anim.SetTrigger("HB45");
                break;
            case 4:
                anim.SetTrigger("HB40");
                break;
            case 3.5f:
                anim.SetTrigger("HB35");
                break;
            case 3:
                anim.SetTrigger("HB30");
                break;
            case 2.5f:
                anim.SetTrigger("HB25");
                break;
            case 2:
                anim.SetTrigger("HB20");
                break;
            case 1.5f:
                anim.SetTrigger("HB15");
                break;
            case 1:
                anim.SetTrigger("HB10");
                break;
            case 0.5f:
                anim.SetTrigger("HB5");
                break;
            case 0:
                anim.SetTrigger("HB0");
                break;
            default:
                return;

        }
    }
    //private void OnDestroy()
    //{
    //    randomPathEnemySpawner.bossHasBeenDestroyed = true;
    //}
}

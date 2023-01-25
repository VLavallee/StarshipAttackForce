using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSlider : MonoBehaviour
{
    public Player player;
    public Slider healthSlider;
    public float maxSliderValue;
    public Image sliderFillColor;
    [SerializeField] Color maxHealthColor, highHealthColor, midHealthColor, lowHealthColor;
    [SerializeField] float seventyFive, fifty, twentyFive, ten;
    [SerializeField] float maxHealth;
    [SerializeField] GameObject gameOverTitleObj;

    [SerializeField] bool playerFound = false;

    private void Start()
    {
        healthSlider = GetComponent<Slider>();
    }
    void Update()
    {
        if(!playerFound)
        {
            FindActivePlayerInScene();
        }
        if (FindObjectOfType<Player>() == null) 
        {
            healthSlider.value = 0;
            return; 
        }
        healthSlider.value = player.playerHealth;
        UpdateColor();
        if(FindObjectOfType<Player>())
        {
            if(FindObjectOfType<Player>().isAlive == false)
            {
                gameOverTitleObj.SetActive(true);
            }
        }
    }

    private void UpdateColor()
    {
        //health is at max
        

        if (healthSlider.value >= seventyFive)
        {
            sliderFillColor.color = maxHealthColor;
        }

        if (healthSlider.value < seventyFive && healthSlider.value >= fifty)
        {
            sliderFillColor.color = highHealthColor;
        }

        if (healthSlider.value < fifty && healthSlider.value >= twentyFive)
        {
            sliderFillColor.color = midHealthColor;
        }

        if (healthSlider.value < twentyFive && healthSlider.value >= ten)
        {
            sliderFillColor.color = lowHealthColor;
        }
    }

    private void ConfigureValuesForHealthSliderColors()
    {
        maxHealth = player.GetHealthAmount();
        maxSliderValue = maxHealth;
        healthSlider.maxValue = maxHealth;


        seventyFive = maxHealth * 0.75f;
        fifty = maxHealth * 0.50f;
        twentyFive = maxHealth * 0.25f;
        ten = maxHealth * 0.1f;
    }

    private void FindActivePlayerInScene()
    {
        if (!FindObjectOfType<Player>())
        {
            return;
        }
        else if (FindObjectOfType<Player>())
        {
            player = FindObjectOfType<Player>();
            ConfigureValuesForHealthSliderColors();
            playerFound = true;

        }
    }

    // other scripts can call this method to check if the player script has been found
    public bool PlayerFoundStatus()
    {
        return playerFound;
    }
}

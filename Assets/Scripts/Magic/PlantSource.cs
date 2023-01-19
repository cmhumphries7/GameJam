using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlantSource: MonoBehaviour
{
    [SerializeField] public float drainRate = 5f;
    [SerializeField] public float cloudDrainRate = 10f;
    [SerializeField] public float drainRadius = 5f;
    [SerializeField] public float plantHealth = 50f;
    [SerializeField] public Light2D glowLight;
    public GameObject[] drainables;
    public PlayerLife playerLife;
    private LifeMagic lifeMagic;
    [SerializeField] private GameObject tutorialPrompt;
    [SerializeField] public AudioClip drainClip;


    public void Start()
    {
       drainables = GameObject.FindGameObjectsWithTag("Drainable");
       playerLife = FindObjectOfType<PlayerLife>();
    }

    public void Update()
    {
        if (lifeMagic != null)
        {
            CheckForPlant();
        }

        if (glowLight != null)
        {
            glowLight.intensity = (plantHealth * 2f) / 100f;
        }
        
    }

    public GameObject[] getDrainables()
    {
        return drainables;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        lifeMagic = collision.gameObject.GetComponent<LifeMagic>();
        lifeMagic.drainAudio = drainClip;
        if (plantHealth <= 0)
        {
            lifeMagic.canDrain = false;
        }
        else
        {
            lifeMagic.canDrain = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        lifeMagic.drainAudio = null;
        lifeMagic = null;
        
    }

    public void CheckForPlant()
    {
        if (lifeMagic.isRequestingLife && plantHealth > 0)
        {
            if (tutorialPrompt != null)
            {
                tutorialPrompt.SetActive(false);
            }
            float timedDrainRate = drainRate * Time.deltaTime;
            plantHealth = plantHealth - timedDrainRate;
            playerLife.lifeForce = Mathf.Clamp(playerLife.lifeForce + timedDrainRate, 0, playerLife.maxlifeforce);
            if (plantHealth < 0)
            {
                plantHealth = 0;
            }
        }
    }
}

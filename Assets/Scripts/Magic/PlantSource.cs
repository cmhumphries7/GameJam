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
    [SerializeField] public float cloudHealth = 150f;
    [SerializeField] public Light2D glowLight;
    public GameObject[] drainables;
    public PlayerLife playerLife;
    private LifeMagic lifeMagic;
    [SerializeField] float phaseTimer = 0f;
    float timeToNextPhase = 5f;
    bool nextPhase = false;




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
            CheckForCloud();
        }

        glowLight.intensity = (plantHealth * 2f) / 100f;
    }

    public GameObject[] getDrainables()
    {
        return drainables;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        lifeMagic = collision.gameObject.GetComponent<LifeMagic>();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        lifeMagic = null;
    }

    public void CheckForPlant()
    {
        if (lifeMagic.isRequestingLife && plantHealth > 0)
        {
            float timedDrainRate = drainRate * Time.deltaTime;
            plantHealth = plantHealth - timedDrainRate;
            playerLife.lifeForce = Mathf.Clamp(playerLife.lifeForce + timedDrainRate, 0, 100f);
            if (plantHealth < 0)
            {
                plantHealth = 0;
            }
        }
    }

    public void CheckForCloud()
    {
        PhaseOne();
        PhaseTwo();
    }

    public void PhaseOne()
    {
        if (lifeMagic.isRequestingLife && cloudHealth > 100)
        {
            float timedDrainRate = cloudDrainRate * Time.deltaTime;
            cloudHealth = cloudHealth - timedDrainRate;
            playerLife.lifeForce = playerLife.lifeForce + timedDrainRate;          
        }
        if (cloudHealth <= 100)
        {
            phaseTimer += Time.deltaTime;
            if (phaseTimer < timeToNextPhase && !nextPhase)
            {
                lifeMagic.LockMagic(true);
            }
            else
            {
                ResetTimer();
            }
            
        }
    }

    public void PhaseTwo()
    {
        if (lifeMagic.isRequestingLife && cloudHealth > 50 && nextPhase)
        {
            //Debug.Log("This is a cloud");
            float timedDrainRate = cloudDrainRate * Time.deltaTime;
            cloudHealth = cloudHealth - timedDrainRate;
            playerLife.lifeForce = playerLife.lifeForce + timedDrainRate;
            if (cloudHealth < 50)
            {
                cloudHealth = 50;
            }
        }
    }

    public void ResetTimer()
    {
        phaseTimer = 0f;
        Debug.Log("phase timer is " + phaseTimer);
        lifeMagic.LockMagic(false);
        nextPhase = true;
    }




}

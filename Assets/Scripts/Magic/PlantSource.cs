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
    bool phaseOneComplete = false;
    bool phaseTwoComplete = false;
    bool phaseThreeComplete = false;
    bool phaseComplete;




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
            playerLife.lifeForce = Mathf.Clamp(playerLife.lifeForce + timedDrainRate, 0, playerLife.maxlifeforce);
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
        PhaseThree();
    }

    #region Cloud Drain Phases
    public void PhaseOne()
    {
        if (lifeMagic.isRequestingLife && cloudHealth > 100 && !phaseOneComplete)
        {            
            float timedDrainRate = cloudDrainRate * Time.deltaTime;
            cloudHealth = cloudHealth - timedDrainRate;
            playerLife.lifeForce = playerLife.lifeForce + timedDrainRate;
            phaseComplete = false;
        }

        if (cloudHealth <= 100 && !phaseComplete && !phaseOneComplete)
        {
            Debug.Log("Entering first timer");
            phaseTimer += Time.deltaTime;
            if (phaseTimer < timeToNextPhase)
            {
                lifeMagic.LockMagic(true);
            }
        }

        if (phaseTimer > timeToNextPhase && !phaseOneComplete)
        {
            phaseComplete = true;
            ResetTimer();
            phaseOneComplete = true;
        }
    }

    public void PhaseTwo()
    {
        if (lifeMagic.isRequestingLife && cloudHealth < 101 && phaseOneComplete)
        {
            Debug.Log("Entering phase 2");
            float timedDrainRate = cloudDrainRate * Time.deltaTime;
            cloudHealth = cloudHealth - timedDrainRate;
            playerLife.lifeForce = playerLife.lifeForce + timedDrainRate;
            phaseComplete = false;
        }

        if (cloudHealth <= 50 && !phaseComplete && !phaseTwoComplete)
        {
            Debug.Log("Entering second timer");
            phaseTimer += Time.deltaTime;
            if (phaseTimer < timeToNextPhase)
            {
                lifeMagic.LockMagic(true);
            }
        }

        if (phaseTimer > timeToNextPhase && !phaseTwoComplete)
        {
            phaseComplete = true;
            ResetTimer();
            phaseTwoComplete = true;
        }

    }

    public void PhaseThree()
    {
        if (lifeMagic.isRequestingLife && cloudHealth < 51 && phaseTwoComplete)
        {
            Debug.Log("Entering phase 3");
            float timedDrainRate = cloudDrainRate * Time.deltaTime;
            cloudHealth = cloudHealth - timedDrainRate;
            playerLife.lifeForce = playerLife.lifeForce + timedDrainRate;       
        }
        if (cloudHealth <= 0 )
        {
            phaseThreeComplete = true;
            cloudHealth = 0;
        }
    }

    #endregion

    public void ResetTimer()
    {
        phaseTimer = 0f;
        lifeMagic.LockMagic(false);
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CloudShrink : MonoBehaviour
{
    [SerializeField] public float cloudHealth = 150f;
    [SerializeField] public float cloudDrainRate = 10f;
    [SerializeField] public Light2D glowLight;
    [SerializeField] float phaseTimer = 0f;
    float timeToNextPhase = 5f;
    bool phaseOneComplete = false;
    bool phaseTwoComplete = false;
    bool phaseThreeComplete = false;
    bool phaseComplete;
    private Vector3 scaleChange, positionChange;
    private LifeMagic lifeMagic;
    public PlayerLife playerLife;

    private void Awake()
    {
        scaleChange = new Vector3(-1.5f, -1.5f, -1.5f);
        positionChange = new Vector3(-1f, -1f, -1f);
    }

    public void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
    }

    public void Update()
    {
        if (lifeMagic != null)
        {
            PhaseOne();
            PhaseTwo();
            PhaseThree();
        }
        glowLight.intensity = (cloudHealth * 2f) / 100f;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        lifeMagic = collision.gameObject.GetComponent<LifeMagic>();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        lifeMagic = null;
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
            ShrinkCloud();
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
            ShrinkCloud();
            transform.position += positionChange;
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
        if (cloudHealth <= 0)
        {
            phaseThreeComplete = true;
            cloudHealth = 0;
            Destroy(gameObject);
        }
    }

#endregion

    public void ResetTimer()
    {
        phaseTimer = 0f;
        lifeMagic.LockMagic(false);
    }

    public void ShrinkCloud()
    {
        transform.localScale += scaleChange;
    }










}

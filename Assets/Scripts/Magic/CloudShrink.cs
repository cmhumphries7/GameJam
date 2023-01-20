using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CloudShrink : MonoBehaviour
{
    [SerializeField] public float cloudHealth = 150f;
    [SerializeField] public float cloudDrainRate = 10f;
    [SerializeField] public Light2D glowLight;
    [SerializeField] public GameObject darknessCircle;
    [SerializeField] float phaseTimer = 0f;
    [SerializeField] AudioClip drainAudio;
    [SerializeField] DialogueUI dialogue;
    [SerializeField] DialogueObject[] dialogueObjects;
    [SerializeField] AudioSource cloudSounds;
    [SerializeField] AudioClip thunder;
    float timeToNextPhase = 3f;
    bool phaseOneComplete = false;
    bool phaseTwoComplete = false;
    bool phaseThreeComplete = false;
    bool phaseComplete;
    private Vector3 scaleChange, positionChange;
    private LifeMagic lifeMagic;
    private PlayerMovement movement;
    public PlayerLife playerLife;
    private Vector3 startingSize;
    private Vector3 darknessCircleSize;

    private void Awake()
    {
        scaleChange = new Vector3(-1.5f, -1.5f, -1.5f);
        positionChange = new Vector3(-1f, -1f, -1f);
    }

    public void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
        startingSize = transform.localScale;
        darknessCircleSize = darknessCircle.transform.localScale;
    }

    public void Update()
    {
        if (lifeMagic != null)
        {
            PhaseOne();
            PhaseTwo();
            PhaseThree();
        }
        //glowLight.intensity = (cloudHealth * 2f) / 100f;
        darknessCircle.transform.localScale = darknessCircleSize;
        transform.localScale = new Vector3(startingSize.x * cloudHealth / 150f + 2, startingSize.y * cloudHealth / 150f + 2, transform.localScale.z);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        lifeMagic = collision.gameObject.GetComponent<LifeMagic>();
        movement = collision.gameObject.GetComponent<PlayerMovement>();
        lifeMagic.drainAudio = drainAudio;
        if (cloudHealth <= 0)
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


    #region Cloud Drain Phases
    public void PhaseOne()
    {
        if (lifeMagic.isRequestingLife && cloudHealth > 130 && !phaseOneComplete)
        {
            float timedDrainRate = cloudDrainRate * Time.deltaTime;
            cloudHealth = cloudHealth - timedDrainRate;
            darknessCircleSize = new Vector3(darknessCircleSize.x + timedDrainRate * 3.3f, darknessCircleSize.y + timedDrainRate * 3.3f, 1);
            playerLife.lifeForce = playerLife.lifeForce + timedDrainRate;
            phaseComplete = false;
        }

        if (cloudHealth <= 130 && !phaseComplete && !phaseOneComplete)
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
            movement.LockMovement(true);
            lifeMagic.LockMagic(true);
            cloudSounds.PlayOneShot(thunder);
            dialogue.ShowDialogue(dialogueObjects[0]);
            ResetTimer();
            phaseOneComplete = true;
            ShrinkCloud(100);
        }
    }

    public void PhaseTwo()
    {
        if (lifeMagic.isRequestingLife && cloudHealth < 101 && phaseOneComplete)
        {
            Debug.Log("Entering phase 2");
            float timedDrainRate = cloudDrainRate * Time.deltaTime;
            cloudHealth = cloudHealth - timedDrainRate;
            darknessCircleSize = new Vector3(darknessCircleSize.x + timedDrainRate * 3.3f, darknessCircleSize.y + timedDrainRate * 3.3f, 1);
            playerLife.lifeForce = playerLife.lifeForce + timedDrainRate;
            phaseComplete = false;
        }

        if (cloudHealth <= 80 && !phaseComplete && !phaseTwoComplete)
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
            movement.LockMovement(true);
            lifeMagic.LockMagic(true);
            cloudSounds.PlayOneShot(thunder);
            dialogue.ShowDialogue(dialogueObjects[1]);
            ResetTimer();
            phaseTwoComplete = true;
            ShrinkCloud(50);
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
            darknessCircleSize = new Vector3(darknessCircleSize.x + timedDrainRate * 3.3f, darknessCircleSize.y + timedDrainRate * 3.3f, 1);
            playerLife.lifeForce = playerLife.lifeForce + timedDrainRate;
        }
        if (cloudHealth <= 30)
        {
            cloudSounds.PlayOneShot(thunder);
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

    public void ShrinkCloud(float health)
    {
       // transform.localScale += scaleChange;
       cloudHealth = health;
    }










}

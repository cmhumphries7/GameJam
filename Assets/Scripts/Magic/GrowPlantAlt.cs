using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class GrowPlantAlt : MonoBehaviour
{
    [SerializeField] private float growPlantHealth = 0f;
    [SerializeField] public float growCost = 20f;
    [SerializeField] public Light2D glowLight;
    [SerializeField] public float drainRate = 5f;
    [SerializeField] public float timeToGrow = 5f;
    [SerializeField] public AudioClip growClip;
    [SerializeField] public AudioClip drainClip;
    public PlayerLife playerLife;
    private LifeMagic lifeMagic;
    //lerp
    bool isScaling = false;
    public Transform vine;
    public Vector3 toScale = new Vector3(430.01f, .3f, 1);
    private Vector3 startScalesize;
    private Coroutine growRoutine;
    [SerializeField] private GameObject tutorialPrompt;

    public AudioSource audioSource;
    void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
        if (vine != null) startScalesize = vine.localScale;
        audioSource = GetComponent<AudioSource>();
    }


    public void Update()
    {
        if (lifeMagic != null && growRoutine == null)
        {
            //Debug.Log("Checking for life magic");

            if (playerLife.lifeForce >= growCost)
            {

                if (lifeMagic.isGrowingLife && growPlantHealth < growCost)
                {
   
                    float timedDrainRate = drainRate * Time.deltaTime;
                    growPlantHealth = Mathf.Clamp(growPlantHealth + timedDrainRate, 0f, growCost);
                    playerLife.lifeForce = Mathf.Clamp(playerLife.lifeForce - timedDrainRate, 0, 100f);
                    if (audioSource != null)
                    {
                        audioSource.Play();
                    }
                    
                    if (tutorialPrompt != null)
                    {
                        tutorialPrompt.SetActive(false);
                    }
                }

                if (lifeMagic.isRequestingLife && growPlantHealth > 0)
                {
                    float timedDrainRate = drainRate * Time.deltaTime;
                    growPlantHealth = growPlantHealth - timedDrainRate;
                    playerLife.lifeForce = Mathf.Clamp(playerLife.lifeForce + timedDrainRate, 0, 100f);
                    if (growPlantHealth < 0)
                    {
                        growPlantHealth = 0;
                    }
                    
                }

                float multiplierX = toScale.x / (drainRate * timeToGrow * growCost);
                float xRate = drainRate * timeToGrow * growPlantHealth * multiplierX;
                float multiplierY = toScale.y / (drainRate * timeToGrow * growCost);
                float yRate = drainRate * timeToGrow * growPlantHealth * multiplierY;
                if (vine != null)
                {
                    vine.localScale = new Vector3(Mathf.Clamp(startScalesize.x + xRate, 0, toScale.x), Mathf.Clamp(startScalesize.y + yRate, 0, toScale.y), vine.localScale.z);
                }
                if (glowLight != null)
                {
                    glowLight.intensity = growPlantHealth / 20f;
                }
            }
        }

        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            lifeMagic = collision.gameObject.GetComponent<LifeMagic>();
            lifeMagic.growAudio = growClip;
            lifeMagic.drainAudio = drainClip;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            lifeMagic.growAudio = null;
            lifeMagic.drainAudio = null;
            lifeMagic = null;
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            if (growPlantHealth >= 0)
            {
                lifeMagic.canDrain = true;
            }
            else
            {
                lifeMagic.canDrain = false;
            }
        }
    }

    IEnumerator scaleOverTime(Transform vine, float duration)
    {
        if (isScaling)
        {
            yield break;
        }
        isScaling = true;
        float counter = 0;
        Vector3 startScalesize = vine.localScale;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            vine.localScale = Vector3.Lerp(startScalesize, toScale, counter / duration);
            yield return null;
        }

        isScaling = false;

    }

}

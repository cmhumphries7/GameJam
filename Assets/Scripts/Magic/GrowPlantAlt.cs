using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GrowPlantAlt : MonoBehaviour
{
    [SerializeField] private float growPlantHealth = 0f;
    [SerializeField] public float growCost = 20f;
    [SerializeField] public Light2D glowLight;
    [SerializeField] public float drainRate = 5f;
    public PlayerLife playerLife;
    private LifeMagic lifeMagic;
    public GameObject[] growables;
    //lerp
    bool isScaling = false;
    public Transform vine;
    public Vector3 toScale = new Vector3(430.01f, .3f, 1);
    private Vector3 startScalesize;
    private Coroutine growRoutine;
    [SerializeField] private GameObject tutorialPrompt;

    void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
        growables = GameObject.FindGameObjectsWithTag("Growable");
        startScalesize = vine.localScale;
    }

    public GameObject[] getGrowables()
    {
        return growables;
    }


    public void Update()
    {
        if (lifeMagic != null && growRoutine == null)
        {
            //Debug.Log("Checking for life magic");
            if (playerLife.lifeForce >= 20f)
            {


                if (lifeMagic.isGrowingLife)
                {
                    float timedDrainRate = drainRate * Time.deltaTime;
                    growPlantHealth = Mathf.Clamp(growPlantHealth + timedDrainRate, 0f, 20f);
                    playerLife.lifeForce = Mathf.Clamp(playerLife.lifeForce - timedDrainRate, 0, 100f);
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


            }
        }

        vine.localScale = new Vector3(startScalesize.x + growPlantHealth * toScale.x / 5f, vine.localScale.y, vine.localScale.z);
        glowLight.intensity = growPlantHealth / 20f;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        lifeMagic = collision.gameObject.GetComponent<LifeMagic>();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        lifeMagic = null;

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

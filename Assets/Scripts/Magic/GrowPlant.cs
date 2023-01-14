using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlant : MonoBehaviour
{
    [SerializeField] private float growPlantHealth = 0f;
    [SerializeField] public float growCost = 20f;
    public PlayerLife playerLife;
    private LifeMagic lifeMagic;
    public GameObject[] growables;
    //lerp
    bool isScaling = false;
    public Transform vine;
    public Vector3 toScale = new Vector3(430.01f, .3f, 1);
    private Coroutine growRoutine;
    //public Vector3 plantGrowth = new Vector3 (2,0,0);

    // Start is called before the first frame update
    void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
        growables = GameObject.FindGameObjectsWithTag("Growable");
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
                if (lifeMagic.isGrowingLife && growPlantHealth == 0f)
                {
                    growPlantHealth = growCost;
                    playerLife.lifeForce = playerLife.lifeForce - growCost;
                    Debug.Log("Growing health: " + growPlantHealth);                   
                    growRoutine = StartCoroutine(scaleOverTime(vine.transform, 10f));
                    //transform.localScale = transform.localScale + plantGrowth;
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision )
    {
        lifeMagic = collision.gameObject.GetComponent<LifeMagic>();
    }
    
    public void OnTriggerExit2D(Collider2D collision )
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

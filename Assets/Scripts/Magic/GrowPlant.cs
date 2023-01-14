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

    // Start is called before the first frame update
    void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
        lifeMagic = FindObjectOfType<LifeMagic>();
        growables = GameObject.FindGameObjectsWithTag("Growable");
    }

    public GameObject[] getGrowables()
    {
        return growables;
    }

    // Update is called once per frame
    public void Update()
    {
        if (lifeMagic != null)
        {
            //Debug.Log("Checking for life magic");
            if (playerLife.lifeForce >= 20f)
            {
                if (lifeMagic.isGrowingLife && growPlantHealth == 0f)
                {
                    growPlantHealth = growCost;
                    playerLife.lifeForce = playerLife.lifeForce - growCost;
                    Debug.Log("Growing health: " + growPlantHealth);
                    transform.localScale = transform.localScale + new Vector3(2,0,0);
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
}

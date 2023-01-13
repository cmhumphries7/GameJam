using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSource: MonoBehaviour
{
    [SerializeField] public float drainRate = 5f;
    [SerializeField] public float drainRadius = 5f;
    [SerializeField] public float plantHealth = 50f;
    public GameObject[] drainables;
    public PlayerLife playerLife;
    private LifeMagic lifeMagic;

    public void Start()
    {
       drainables = GameObject.FindGameObjectsWithTag("Drainable");
       Debug.Log("These are the plant source " + drainables.Length);
       playerLife = FindObjectOfType<PlayerLife>();
    }

    public void Update()
    {
        if (lifeMagic != null)
        {
            Debug.Log("Checking for life magic");
            if (lifeMagic.isRequestingLife && plantHealth > 0)
            {
                float timedDrainRate = drainRate * Time.deltaTime;
                plantHealth = plantHealth - timedDrainRate;
                playerLife.lifeForce = playerLife.lifeForce + timedDrainRate;
            }
        }
    }

    public GameObject[] getDrainables()
    {
        return drainables;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Inside on trigger stay");

        lifeMagic = collision.gameObject.GetComponent<LifeMagic>();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        lifeMagic = null;
    }



}

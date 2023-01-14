using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] public float lifeForce = 100f;
    [SerializeField] public float decayRate = 5f;
    [SerializeField] public Light2D glowLight;
    private GameObject player;
    public PlantSource plantSource;
    public GrowPlant growPlant;
    public GameObject[] drainables;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       
        plantSource = FindObjectOfType<PlantSource>();

        growPlant = FindObjectOfType<GrowPlant>();

        drainables = plantSource.getDrainables();

        StartCoroutine(LifeDecay(player));
        Debug.Log("These are the player " + (plantSource.drainables).Length);
    }

    void Update()
    {
        glowLight.intensity = lifeForce / 100;

    }

    private IEnumerator LifeDecay(GameObject player)
    {
        while (lifeForce > 0f)
        {
            lifeForce = lifeForce - decayRate;
            Debug.Log("Player's remaining lifeForce: " + lifeForce);
            yield return new WaitForSeconds(10);
        }
        yield return null;
        //Debug.Log("Finished.");

    }

    #region findDrainable
    /*    public GameObject FindClosestDrainable()
        {
            GameObject closest = null;
            //Debug.Log("Entered the find closest loop");
            for (int i = 0; i < drainables.Length; i++)
            {
                //Debug.Log("Entered the for loop");
                float drainRadius = plantSource.drainRadius;
                Vector2 diff = player.transform.position - drainables[i].transform.position;
                float distanceToDrainable = diff.magnitude;
                if (distanceToDrainable <= drainRadius)
                {
                    closest = drainables[i];
                    //Debug.Log("I've found the closest");
                }
            }
            return closest;       
        }*/
    #endregion

}



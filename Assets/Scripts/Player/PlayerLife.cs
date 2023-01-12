using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] public float lifeForce = 100f;
    [SerializeField] public float decayRate = 5f;
    [SerializeField] public float drainRate = 5f;
    [SerializeField] public float drainRadius = 5f;
    private GameObject player;
    private PlantSource[] drainables;
    private PlantSource plantSource;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        drainables = PlantSource.FindGameObjectWithTag("Drainable");
        plantSource = FindObjectOfType<PlantSource>();

        StartCoroutine(LifeDecay(player));
    }

     void Update()
    {
        PlantSource foundPlant = FindClosestDrainable();
        if ((Input.GetKey(KeyCode.E) && FindClosestDrainable() != null))
        {
            Debug.Log("Entered Loop");
            StartCoroutine(DrainLife(foundPlant));
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            StopCoroutine(DrainLife(foundPlant));
        }
    }

    private IEnumerator LifeDecay(GameObject player)
    {
        while (lifeForce > 0f)
        {
            lifeForce = lifeForce - decayRate;
            Debug.Log(lifeForce);
            yield return new WaitForSeconds(10);
            
        }
        yield return null;
        //Debug.Log("Finished.");

    }

    private IEnumerator DrainLife(PlantSource foundPlant)
    {
        while (foundPlant.plantHealth > 0f)
        {
            foundPlant.plantHealth -=- drainRate;
            Debug.Log("Plant health is" + foundPlant.plantHealth);
            yield return new WaitForSeconds(10);
        }
        //yield return null;
        Debug.Log("Finished.");
    }

    public PlantSource FindClosestDrainable()
    {
        PlantSource closest = null;
        foreach (PlantSource drainable in drainables)
        {
            Vector2 diff = player.transform.position - drainable.transform.position;
            float distanceToDrainable = diff.magnitude;
            if (distanceToDrainable <= drainRadius)
            {
                closest = drainable;
            }
        }
        return closest;
    }

}

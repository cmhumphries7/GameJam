using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] public float lifeForce = 100f;
    [SerializeField] public float decayRate = 5f;
    private GameObject player;
    public PlantSource plantSource;
    public GameObject[] drainables;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       
        plantSource = FindObjectOfType<PlantSource>();

        drainables = plantSource.getDrainables();

        StartCoroutine(LifeDecay(player));
        Debug.Log("These are the player " + (plantSource.drainables).Length);
    }

    void Update()
    {
        GameObject foundPlant = FindClosestDrainable();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Entered Loop");
            StartCoroutine(plantSource.DrainLife(foundPlant));
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            StopCoroutine(plantSource.DrainLife(foundPlant));
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

    public GameObject FindClosestDrainable()
    {
        GameObject closest = null;
        foreach (GameObject drainable in drainables)
        {
            Vector2 diff = player.transform.position - drainable.transform.position;
            float distanceToDrainable = diff.magnitude;
            if (distanceToDrainable <= plantSource.drainRadius)
            {
                closest = drainable;
            }
        }
        return closest;
    }
}



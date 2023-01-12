using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] public float lifeForce = 100f;
    [SerializeField] public float decayRate = 5f;
    [SerializeField] public float drainTime = 5f;
    [SerializeField] public float drainRadius = 5f;
    private GameObject player;
    private GameObject[] drainables;
    private PlantSource plantSource;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        drainables = GameObject.FindGameObjectsWithTag("Drainable");
        plantSource = FindObjectOfType<PlantSource>();
        StartCoroutine(LifeDecay(player));
    }

     void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) && FindClosestDrainable() != null))
        {
            FindClosestDrainable();
            StartCoroutine(DrainLife(closest));
        }     
        if (Input.GetKeyUp(KeyCode.E)) StopCoroutine(DrainLife(closest));
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
        Debug.Log("Finished.");

    }

    private IEnumerator DrainLife(GameObject closest)
    {
        while (closest.plantHealth > 0f)
        {

        }
        yield return null;
        Debug.Log("Finished.");
    }

    public GameObject FindClosestDrainable()
    {
        GameObject closest = null;
        foreach (GameObject drainable in drainables)
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

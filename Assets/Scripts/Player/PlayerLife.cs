using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] public float lifeForce = 200f;
    [SerializeField] public float decayRate = 1f;
    public float timedDecayRate;
    [SerializeField] public Light2D glowLight;
    private GameObject player;
    public PlantSource plantSource;
    public GrowPlant growPlant;
    public GameObject[] drainables;
    public bool isIntroLevel = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       
        plantSource = FindObjectOfType<PlantSource>();

        growPlant = FindObjectOfType<GrowPlant>();

        drainables = plantSource.getDrainables();

        if (!isIntroLevel)
        {
            StartCoroutine(LifeDecay(player));
        }
        //Debug.Log("These are the player " + (plantSource.drainables).Length);

}

void Update()
    {
        glowLight.pointLightOuterRadius = lifeForce / 25;
        timedDecayRate = decayRate * Time.deltaTime;

    }

    private IEnumerator LifeDecay(GameObject player)
    {
        while (lifeForce > 0f)
        {
            lifeForce = lifeForce - timedDecayRate ;
            //Debug.Log("Player's remaining lifeForce: " + lifeForce);
            Die();
            yield return null;
        }
        yield return null;
        //Debug.Log("Finished.");

    }

    void Die()
    {
        //Debug.Log("life force is zero");
        if (lifeForce <= 0 )
        {
            SceneManager.LoadScene("DeathScene");
        }
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



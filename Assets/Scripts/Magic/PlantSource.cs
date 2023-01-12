using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSource: MonoBehaviour
{
    [SerializeField] public float drainRate = 5f;
    [SerializeField] public float drainRadius = 5f;
    [SerializeField] public float plantHealth = 50f;
    public GameObject[] drainables;

    public void Start()
    {
       drainables = GameObject.FindGameObjectsWithTag("Drainable");
       Debug.Log("These are the plant source " + drainables.Length);
    }

    public GameObject[] getDrainables()
    {
        return drainables;

    }

    public IEnumerator DrainLife(GameObject foundPlant)
    {
        while (plantHealth > 0f && Input.GetKey(KeyCode.E))
        {
            plantHealth = plantHealth - drainRate;
            Debug.Log("Plant health is" + plantHealth);
            if (Input.GetKeyUp(KeyCode.E) )
            {
                StopCoroutine(DrainLife(foundPlant));
                break;
            }
            yield return new WaitForSeconds(1);
        }
        yield return null;
        Debug.Log("Finished.");
    }



}

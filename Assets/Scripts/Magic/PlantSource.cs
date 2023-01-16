using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlantSource: MonoBehaviour
{
    [SerializeField] public float drainRate = 50f;
    [SerializeField] public float drainRadius = 5f;
    [SerializeField] public float plantHealth = 50f;
    [SerializeField] public Light2D glowLight;
    public GameObject[] drainables;
    public PlayerLife playerLife;
    private LifeMagic lifeMagic;
    int LayerPlantMask, LayerCloudMask;


    public void Start()
    {
       drainables = GameObject.FindGameObjectsWithTag("Drainable");
       playerLife = FindObjectOfType<PlayerLife>();
    }

    public void Awake()
    {
        int LayerPlantMask = LayerMask.NameToLayer("PlantMask");
        int LayerCloudMask = LayerMask.NameToLayer("CloudMask");
    }

    public void Update()
    {
        if (lifeMagic != null)
        {
            //Debug.Log("Checking for life magic");
            if (lifeMagic.isRequestingLife && plantHealth > 0)
            {
                float timedDrainRate = drainRate * Time.deltaTime;
                plantHealth = plantHealth - timedDrainRate;
                playerLife.lifeForce = playerLife.lifeForce + timedDrainRate;
                if (plantHealth < 0)
                {
                    plantHealth = 0;
                }
            }
        }

        glowLight.intensity = (plantHealth * 2f) / 100f;
    }

    public GameObject[] getDrainables()
    {
        return drainables;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        lifeMagic = collision.gameObject.GetComponent<LifeMagic>();
        var layerMask = collision.gameObject.layer;
        if (layerMask == LayerPlantMask)
        {
            Debug.Log("This is a plant");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        lifeMagic = null;
    }

}

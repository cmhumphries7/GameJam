using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImbuePlants : MonoBehaviour
{
    [SerializeField] public float imbueCost = 20f;
    public PlayerLife playerLife;
    private float ImbueablesHealth = 0f;
    private LifeMagic lifeMagic;

    // Start is called before the first frame update
    void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();

    }

    // Update is called once per frame
    void Update()
    {
        if (lifeMagic.isImbuingLife != false)
        {
            if (playerLife.lifeForce >= 20f){
                    if (lifeMagic.isImbuingLife && ImbueablesHealth == 0f){
                        ImbueablesHealth = ImbueablesHealth + imbueCost;
                        playerLife.lifeForce = playerLife.lifeForce - imbueCost;
                        Debug.Log("Imbuables health: " + ImbueablesHealth);
                    }
                }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision )
    {
        lifeMagic = collision.gameObject.GetComponent<LifeMagic>();

    }
    // public void OnTriggerExit2D(Collider2D collision )
    // {
    //     lifeMagic = null;

    // }
}

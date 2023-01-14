using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImbuePlants : MonoBehaviour
{
    [SerializeField] private float imbueablesHealth = 0f;
    [SerializeField] public float imbueCost = 20f;
    public PlayerLife playerLife;
    private LifeMagic lifeMagic;

    // Start is called before the first frame update
    void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();

    }

    // Update is called once per frame
    public void Update()
    {
        if (lifeMagic != null)
        {
            if (playerLife.lifeForce >= 20f)
            {
                if (lifeMagic.isImbuingLife && imbueablesHealth == 0f)
                {
                    imbueablesHealth = imbueablesHealth + imbueCost;
                    playerLife.lifeForce = playerLife.lifeForce - imbueCost;
                    Debug.Log("Imbuables health: " + imbueablesHealth);

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

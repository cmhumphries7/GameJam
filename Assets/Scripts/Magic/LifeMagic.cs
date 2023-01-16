using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMagic : MonoBehaviour
{
    public bool isRequestingLife = false;
    public bool isGrowingLife = false;
    private Animator anim;
    private bool drainAnimPlaying = false;
    private bool powerAnimPlaying = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            isRequestingLife = true;
            anim.SetBool("isDraining", true);
            if (!drainAnimPlaying)
            {
                anim.SetTrigger("drainTrigger");
                drainAnimPlaying = true;
            }
        }
        else
        {
            isRequestingLife = false;
            anim.SetBool("isDraining", false);
            drainAnimPlaying = false;
        }


        if (Input.GetKey(KeyCode.F))
        {
            isGrowingLife = true;
            anim.SetBool("isPowering", true);
            if (!powerAnimPlaying)
            {
                anim.SetTrigger("powerTrigger");
                powerAnimPlaying = true;
            }

        }
        else
        {
            isGrowingLife = false;
            anim.SetBool("isPowering", false);
            powerAnimPlaying = false;
        }
    }
}

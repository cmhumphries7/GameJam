using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMagic : MonoBehaviour
{
    public bool isRequestingLife = false;
    public bool isGrowingLife = false;
    private Animator anim;

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
        }
        else
        {
            isRequestingLife = false;
            anim.SetBool("isDraining", false);
        }
        if (Input.GetKey(KeyCode.F))
        {
            isGrowingLife = true;
            anim.SetBool("isPowering", true);

        }
        else
        {
            isGrowingLife = false;
            anim.SetBool("isPowering", false);
        }
    }
}

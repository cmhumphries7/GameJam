using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMagic : MonoBehaviour
{
    public bool isRequestingLife = false;
    public bool isGrowingLife = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            isRequestingLife = true;
        }
        else
        {
            isRequestingLife = false;
        }
        if (Input.GetKey(KeyCode.F))
        {
            isGrowingLife = true;
            
        }
        else
        {
            isGrowingLife = false;
        }
    }
}

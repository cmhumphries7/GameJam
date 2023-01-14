using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMagic : MonoBehaviour
{
    public bool isRequestingLife = false;
    public bool isImbuingLife = false;

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            isImbuingLife = true;
        }
        else
        {
            isImbuingLife = false;
        }
    }
}

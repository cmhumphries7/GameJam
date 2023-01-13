using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMagic : MonoBehaviour
{
    public bool isRequestingLife = false;
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
    }
}

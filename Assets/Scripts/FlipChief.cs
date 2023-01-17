using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipChief : MonoBehaviour
{
    public void Flip()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
    }
}

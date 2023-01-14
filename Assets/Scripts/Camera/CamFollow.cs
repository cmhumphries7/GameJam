using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    public GameObject target;

    public float speed;
    public float maxDistance;

    // Update is called once per frame
    void Update()
    {
        Vector2 camPos = new Vector2(0, 0);
        camPos.x = transform.position.x;
        camPos.y = transform.position.y;

        Vector2 targetPos = new Vector2(0, 0);
        targetPos.x = target.transform.position.x;
        targetPos.y = target.transform.position.y;

        float dist = Vector2.Distance(camPos, targetPos);
        if (dist > maxDistance) {
            //move the camera
            camPos = Vector2.Lerp(camPos, targetPos, speed * Time.deltaTime);
            transform.position = new Vector3(camPos.x, camPos.y, transform.position.z);
        }
    }
}

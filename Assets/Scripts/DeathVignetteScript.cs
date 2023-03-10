using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVignetteScript : MonoBehaviour
{

    private Transform centerTransform;
    private Transform leftTransform;
    private Transform rightTransform;
    private Transform bottomTransform;
    private Transform topTransform;


    private Vector3 circlePosition;
    private Vector3 circleSize;
    private Vector3 targetCircleSize;

    private PlayerLife playerLife;

    private GameObject player;
    public float startingCircleSize;

        private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLife = FindObjectOfType<PlayerLife>();

        centerTransform = transform.Find("void center");
        leftTransform = transform.Find("left void");
        rightTransform = transform.Find("right void");
        bottomTransform = transform.Find("bottom void");
        topTransform = transform.Find("top void");

        setCircleSize(circlePosition,new Vector3(5, 5));

        targetCircleSize = new Vector3(1, 1);
    }

    private void Update()
    {
        startingCircleSize = playerLife.lifeForce / 4;
        //the 4 stands for magic
        circlePosition = player.transform.Find("HEAD").position;
        Vector3 sizeChangeVector = (targetCircleSize - circleSize).normalized;
        Vector3 newCircleSize = (circleSize + sizeChangeVector * startingCircleSize);
        setCircleSize(circlePosition, newCircleSize);
    }

    private void setCircleSize(Vector3 position, Vector3 size)
    {
        centerTransform.localScale = size;
        transform.position = position;

        topTransform.localScale = new Vector3(200, 200);
        topTransform.localPosition = new Vector3(0, topTransform.localScale.y * .5f + size.y * .5f);


        bottomTransform.localScale = new Vector3(200, 200);
        bottomTransform.localPosition = new Vector3(0,-topTransform.localScale.y * .5f - size.y * .5f);


        leftTransform.localScale = new Vector3(200, size.y);
        leftTransform.localPosition = new Vector3(-leftTransform.localScale.x * .5f - size.x * .5f, 0f);


        rightTransform.localScale = new Vector3(200, size.y);
        rightTransform.localPosition = new Vector3(+leftTransform.localScale.x * .5f + size.x * .5f, 0f);
    }
}

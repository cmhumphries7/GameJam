using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed = 0.5f;
    [SerializeField] public float jumpForce = 6f;


    private float movePlayerVector;
    private Rigidbody2D playerRigidBody2D;
    private bool facingRight;

    void Awake()
    {
        playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
    }

    // Update is called once per frame
    void Update()
    {
        movePlayerVector = Input.GetAxis("Horizontal");

        playerRigidBody2D.velocity = new Vector2(movePlayerVector * speed, playerRigidBody2D.velocity.y);

        if (Input.GetKeyDown("space"))
        {
            playerRigidBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (movePlayerVector > 0 && !facingRight)
        {
            Flip();
        }

        else if (movePlayerVector < 0 && facingRight)
        {
            Flip();
        }



        #region Old Movement Code
     /*   if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }

        if (Input.GetKeyDown("space"))
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }*/
        #endregion
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

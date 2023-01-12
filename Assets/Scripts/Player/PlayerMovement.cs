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
    private DialogueUI dialogueUI;

    void Awake()
    {
        playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
        dialogueUI = GetComponent<PlayerDialogue>().DialogueUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueUI != null && dialogueUI.IsOpen) return; //locks movement when dialogueUI is open

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

        void Flip()
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

    }

}


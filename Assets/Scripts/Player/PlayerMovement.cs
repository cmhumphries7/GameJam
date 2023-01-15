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
    CapsuleCollider2D capsuleColliderPlayer;
    private bool facingRight;
    private DialogueUI dialogueUI;
    public bool movementLocked = false;
    int layerMaskGround;
    float heightTestPlayer;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        layerMaskGround = LayerMask.GetMask("Ground");
        capsuleColliderPlayer = GetComponent<CapsuleCollider2D>();
        heightTestPlayer = capsuleColliderPlayer.bounds.extents.y + .05f;
    }
    void Awake()
    {
        playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
        dialogueUI = GetComponent<PlayerDialogue>().DialogueUI;
    }


    void Update()
    {
        if (dialogueUI != null && dialogueUI.IsOpen) return; //locks movement when dialogueUI is open

        if (!movementLocked)
        {
            movePlayerVector = Input.GetAxis("Horizontal");

            playerRigidBody2D.velocity = new Vector2(movePlayerVector * speed, playerRigidBody2D.velocity.y);

            if (Input.GetKeyDown("space") && isGrounded())
            {
                anim.SetTrigger("takeOff");
                playerRigidBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                anim.SetBool("isJumping", true);
            }
        }
        else
        {
            playerRigidBody2D.velocity = new Vector2(0, playerRigidBody2D.velocity.y);
        }

        if (isGrounded())
        {
            anim.SetBool("isJumping", false);
        }

        if (movePlayerVector == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if (movePlayerVector < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        else if (movePlayerVector > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

    }

        public void LockMovement(bool nlock)
        {
            if (nlock)
            {
                movementLocked = true;
            }
            else
            {
                movementLocked = false;
            }
        }

        bool isGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(capsuleColliderPlayer.bounds.center, Vector2.down, heightTestPlayer, layerMaskGround);
            bool isGrounded = hit.collider != null;
            return isGrounded;
        }

}



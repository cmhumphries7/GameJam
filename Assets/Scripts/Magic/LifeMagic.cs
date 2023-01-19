using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMagic : MonoBehaviour
{
    public bool isRequestingLife = false;
    public bool isGrowingLife = false;
    private Animator anim;
    private bool drainAnimPlaying = false;
    private bool powerAnimPlaying = false;
    private PlayerMovement playerMovement;
    public bool magicLocked = false;
    [SerializeField] public AudioSource growDrainAudio;
    public AudioClip growAudio;
    public AudioClip drainAudio;
    public bool canDrain = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (!magicLocked)
        {
            if (Input.GetKey(KeyCode.E))
            {
                isRequestingLife = true;
                anim.SetBool("isDraining", true);
                if (!drainAnimPlaying)
                {
                    anim.SetTrigger("drainTrigger");
                    if (canDrain && drainAudio != null)
                    {
                        growDrainAudio.PlayOneShot(drainAudio);
                    }
                    drainAnimPlaying = true;
                }
            }
            else
            {
                isRequestingLife = false;
                anim.SetBool("isDraining", false);
                if (drainAnimPlaying)
                {
                    growDrainAudio.Stop();
                }
                drainAnimPlaying = false;
            }


            if (Input.GetKey(KeyCode.F))
            {
                isGrowingLife = true;
                anim.SetBool("isPowering", true);

                if (!powerAnimPlaying)
                {
                    anim.SetTrigger("powerTrigger");
                    if (growAudio != null)
                    {
                        growDrainAudio.PlayOneShot(growAudio);
                    }
                    powerAnimPlaying = true;
                }

            }
            else
            {
                isGrowingLife = false;
                anim.SetBool("isPowering", false);
                if (powerAnimPlaying)
                {
                    growDrainAudio.Stop();
                }
                powerAnimPlaying = false;
            }
        }
    }

    public void LockMagic(bool nlock)
    {
        magicLocked = nlock;
        if (nlock == true)
        {
            isRequestingLife = false;
            anim.SetBool("isDraining", false);
            drainAnimPlaying = false;
            isGrowingLife = false;
            anim.SetBool("isPowering", false);
            powerAnimPlaying = false;
            growDrainAudio.Stop();
        }

    }
//used for final cutscene
    public void HoldDrainingAnim(bool hold)
    {
        if (hold == true)
        {
            anim.SetBool("isDraining", true);
            if (!drainAnimPlaying)
            {
                anim.SetTrigger("drainTrigger");
                drainAnimPlaying = true;
            }
        }
        else
        {
            isRequestingLife = false;
            anim.SetBool("isDraining", false);
            drainAnimPlaying = false;
        }
        
    }

        
}

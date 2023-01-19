using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathRegion : MonoBehaviour
{

    [SerializeField] Animator FadeInOut;
    public AudioSource audioSource;
    [SerializeField] public AudioClip deathClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            audioSource.PlayOneShot(deathClip);
            FadeInOut.SetTrigger("FadeOut");
            StartCoroutine(WaitFadeComplete());
        }
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene("DeathScene");
    }

    IEnumerator WaitFadeComplete()
    {
        yield return new WaitForSeconds(1);
        OnFadeComplete();
    }
}

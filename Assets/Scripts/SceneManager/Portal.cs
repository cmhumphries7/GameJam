using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    [SerializeField] int sceneToLoad = 1;
    [SerializeField] Animator FadeInOut;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Portal triggered");
        if(collision.gameObject.tag == "Player")
        {
            print("loading scene");
            FadeInOut.SetTrigger("FadeOut");
            StartCoroutine(WaitFadeComplete());
        }
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator WaitFadeComplete()
    {
        yield return new WaitForSeconds(1);
        OnFadeComplete();
    }

    public void ManualTrigger(int scene)
    {
        print("loading scene");
        SceneManager.LoadScene(scene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    [SerializeField] int sceneToLoad = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Portal triggered");
        if(collision.gameObject.tag == "Player")
        {
            print("loading scene");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

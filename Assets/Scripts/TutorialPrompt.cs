using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPrompt : MonoBehaviour
{
    private bool alreadyTriggered = false;
    [SerializeField] GameObject prompt;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !alreadyTriggered)
        {
            prompt.SetActive(true);
            alreadyTriggered = true;
        }
    }
}

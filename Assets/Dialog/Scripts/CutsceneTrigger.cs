using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector timeline;
    private bool alreadyTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerDialogue player) && !alreadyTriggered)
        {
            player.GetComponent<PlayerMovement>().LockMovement(true);

            timeline.Play();
            
            alreadyTriggered = true;
        }
    }
}

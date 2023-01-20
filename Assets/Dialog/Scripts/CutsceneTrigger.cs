using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector timeline;
    [SerializeField] private AudioClip talkingAudio;
    [SerializeField] private float audioTime;
    private bool alreadyTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerDialogue player) && !alreadyTriggered)
        {
            player.GetComponent<PlayerMovement>().LockMovement(true);
            player.GetComponent<LifeMagic>().LockMagic(true);

            timeline.Play();
            
            alreadyTriggered = true;

            if (talkingAudio != null)
            {
                player.DialogueUI.talkingAudio = talkingAudio;
                player.DialogueUI.audioTime = audioTime;
            }
        }
    }
}

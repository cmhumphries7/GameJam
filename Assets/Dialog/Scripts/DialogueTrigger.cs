using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private AudioClip talkingAudio;
    [SerializeField] private float audioTime;
    private bool alreadyTriggered = false;
    

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerDialogue player) && !alreadyTriggered)
        {
            player.DialogueUI.talkingAudio = null;
            player.DialogueUI.audioTime = 0;
            player.GetComponent<PlayerMovement>().LockMovement(true);
            player.GetComponent<LifeMagic>().LockMagic(true);
            foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
            {
                if (responseEvents.DialogueObject == dialogueObject)
                {
                    player.DialogueUI.AddResponseEvents(responseEvents.Events);
                    break;
                }
            }
            if (talkingAudio != null)
            {
                player.DialogueUI.talkingAudio = talkingAudio;
                player.DialogueUI.audioTime = audioTime;
            }

            player.DialogueUI.ShowDialogue(dialogueObject);
            alreadyTriggered = true;
        }
    }
}

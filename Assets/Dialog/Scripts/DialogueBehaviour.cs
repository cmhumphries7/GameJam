using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class DialogueBehaviour : PlayableBehaviour
{
    [SerializeField] private DialogueObject dialogueObject;
    private DialogueUI dialogueUI;

    private bool clipPlayed = false;
    private bool pauseScheduled = false;
    private PlayableDirector director;

    public override void OnPlayableCreate(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);
        dialogueUI = GameObject.FindObjectOfType<DialogueUI>();
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!clipPlayed
            && info.weight > 0f)
        {
            dialogueUI.currentDirector = director;
            dialogueUI.ShowDialogue(dialogueObject);

            if (Application.isPlaying)
            {
               /* if (hasToPause)
                {
                    pauseScheduled = true;
                }*/
                PauseTimeline(director);
            }

            clipPlayed = true;
        }
    }


    public void PauseTimeline(PlayableDirector activeDirector)
    {
        activeDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
    }
}

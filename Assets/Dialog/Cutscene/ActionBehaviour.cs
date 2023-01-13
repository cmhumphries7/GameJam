using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class ActionBehavior : PlayableBehaviour
{

    private PlayableDirector director;
    private bool clipPlayed = false;

    public override void OnPlayableCreate(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);

    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!clipPlayed
            && info.weight > 0f)
        {

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            director.playableGraph.GetRootPlayable(0).SetSpeed(1d);
        }
    }


    public void PauseTimeline(PlayableDirector activeDirector)
    {
        activeDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
    }
}

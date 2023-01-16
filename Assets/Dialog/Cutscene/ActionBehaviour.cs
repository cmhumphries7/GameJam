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
    private float count = 0;

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

        if (Input.GetKey(KeyCode.F))
        {
            count += Time.deltaTime;
            if (count >= 3f)
            {
                director.playableGraph.GetRootPlayable(0).SetSpeed(1d);
                count = 0;
            }
        }
    }


    public void PauseTimeline(PlayableDirector activeDirector)
    {
        activeDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
    }
}

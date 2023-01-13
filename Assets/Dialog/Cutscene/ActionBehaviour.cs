using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class ActionBehavior : PlayableBehaviour
{

    private PlayableDirector director;

    public override void OnPlayableCreate(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);

    }


    public void PauseTimeline(PlayableDirector activeDirector)
    {
        activeDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
    }
}

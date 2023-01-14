using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class ActionClip : PlayableAsset, ITimelineClipAsset
{
    public ActionBehavior template = new ActionBehavior();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<ActionBehavior>.Create(graph, template);

        return playable;
    }
}

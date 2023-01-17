using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Rendering.Universal;

[Serializable]
public class ActionBehavior : PlayableBehaviour
{

    private PlayableDirector director;
    private bool clipPlayed = false;
    private float count = 0;
    public int scenario = 0;
    [SerializeField] public Light2D glowLight;
    [SerializeField] public Color color;
     private GameObject player;
    

    public override void OnPlayableCreate(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);
        player = GameObject.FindGameObjectWithTag("Player");

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

        if (Input.GetKey(KeyCode.F) && scenario == 0)
        {
            count += Time.deltaTime;
            if (count >= 3f)
            {
                director.playableGraph.GetRootPlayable(0).SetSpeed(1d);
                count = 0;
            }
        }

        if (Input.GetKey(KeyCode.E) && scenario == 1)
        {
            player.GetComponent<PlayerLife>().maxlifeforce = 150;
            count += Time.deltaTime;
            if (count >= 3f)
            {
                director.playableGraph.GetRootPlayable(0).SetSpeed(1d);
                count = 0;
                player.GetComponent<PlayerLife>().glowLight.intensity = 1.5f;
                player.GetComponent<PlayerLife>().lifeForce = 150;
            }
            
            
        }
    }


    public void PauseTimeline(PlayableDirector activeDirector)
    {
        activeDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
    }
}

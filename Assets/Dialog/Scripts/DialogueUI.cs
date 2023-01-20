using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    public PlayableDirector currentDirector = null;

    public bool IsOpen { get; private set;}

    private ResponseHandler responseHandler;
    private TypewriterEffect typerwriterEffect;
    [SerializeField] PlayerMovement player;
    [SerializeField] LifeMagic playerMagic;
    [SerializeField] AudioSource dialogueAudio;
    [SerializeField] AudioClip dialogueClip;
    public AudioClip talkingAudio;
    public float audioTime;


    private void Start()
    {
        typerwriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        player = FindObjectOfType<PlayerMovement>();
        playerMagic = FindObjectOfType<LifeMagic>();
        currentDirector = null;
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        dialogueAudio.PlayOneShot(dialogueClip);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }


    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            if (talkingAudio != null)
            {
                dialogueAudio.time = audioTime;
                dialogueAudio.clip = talkingAudio;
                dialogueAudio.Play();
            }

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return null; //waits one frame
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typerwriterEffect.Run(dialogue, textLabel);

        while (typerwriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                typerwriterEffect.Stop();
            }
        }
        //dialogueAudio.time = 0;
        //dialogueAudio.clip = null;
        dialogueAudio.Stop();

    } 

    public void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        if(currentDirector != null)
        {
            currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(1d);
            currentDirector = null;
        }
        else
        {
            player.LockMovement(false); //unlocks movement at end of dialogue only if it is not part of a cutscene
            playerMagic.LockMagic(false);
            
        }
    }


}


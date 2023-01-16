using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    public PlayableDirector currentDirector;

    public bool IsOpen { get; private set;}

    private ResponseHandler responseHandler;
    private TypewriterEffect typerwriterEffect;
    [SerializeField] PlayerMovement player;

    private void Start()
    {
        typerwriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        player = FindObjectOfType<PlayerMovement>();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
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
        }
    }


}


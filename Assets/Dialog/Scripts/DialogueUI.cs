using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;

    public bool IsOpen { get; private set;}

    private ResponseHandler responseHandler;
    private TypewriterEffect typerwriterEffect;

    private void Start()
    {
        typerwriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
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

    private void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;

    }


}


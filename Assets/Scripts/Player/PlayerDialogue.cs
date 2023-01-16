using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;
    private PlayerMovement playerMove;

    public IInteractable Interactable { get; set;}
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactable?.Interact(this);
            if (Interactable != null)
            {
                Interactable.Interact(this);
                playerMove.LockMovement(true);
            }
        }
    }
}

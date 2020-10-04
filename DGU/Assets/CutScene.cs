using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;

    public Dialogue dialogue;
    private bool isDialogueStarted = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();

    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isDialogueStarted)
        {
            TriggerDialogue();
            isDialogueStarted = true;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueController>().StartDialogue(dialogue);
    }
}

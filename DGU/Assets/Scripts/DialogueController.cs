using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public GameObject gameOjbect;
    public Text dialgoueText;
    private bool isPlayerTalking = false;
    public Image enemyImage;
    public Image playerImage;
    private Queue<string> sentences;
    private bool isDialogueStarted;
    public CutScene cutScene;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        ChangeTalker();
        isPlayerTalking = !isPlayerTalking;
        sentences.Clear();
        isDialogueStarted = true;
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && isDialogueStarted)
        {
            ChangeTalker();
            DisplayNextSentence();
            isPlayerTalking = !isPlayerTalking;
        }
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count ==0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialgoueText.text=sentence;
    }

    void ChangeTalker()
    {
        if(!isPlayerTalking)
        {
            enemyImage.color = Color.white;
            playerImage.color = Color.gray;
        }
        else if (isPlayerTalking)
        {
            enemyImage.color = Color.gray;
            playerImage.color = Color.white;
        }
    }
    void EndDialogue()
    {
        cutScene.playerAttack.enabled = true;
        cutScene.playerMovement.enabled=true;
        cutScene.playerAttack.Attack();
        cutScene.enabled = false;
        
        Destroy(gameOjbect);
        this.enabled = false;
    }
}

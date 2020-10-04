using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerAttack.enabled = true;
            playerMovement.enabled = true;
        }
    }
}

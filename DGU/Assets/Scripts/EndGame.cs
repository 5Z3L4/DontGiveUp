using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject EndPanel;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EndPanel.SetActive(true);
    }
}

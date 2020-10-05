using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour
{
    SceneLoader sceneLoader;
    private void Awake()
    {
        sceneLoader =GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneLoader>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sceneLoader.NextScene("Level2");
        }
    }
}

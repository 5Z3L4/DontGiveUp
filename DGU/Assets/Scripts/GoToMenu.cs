using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMenu : MonoBehaviour
{
    SceneLoader sceneLoader;
    private void Awake()
    {
        sceneLoader = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneLoader>();
    }

    public void LoadMenu()
    {
        sceneLoader.NextScene("Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("CM vcam1"));
        Destroy(GameObject.Find("Main Camera (1)"));
        Destroy(GameObject.Find("Spawn Point"));
        Destroy(GameObject.Find("Skeleton Idle_0"));
        Destroy(GameObject.Find("Canvas"));
        Destroy(GameObject.Find("EventSystem"));
        Destroy(GameObject.Find("HealthManagement"));
        Destroy(GameObject.Find("PauseMenu"));
        Destroy(GameObject.Find("SceneManagement"));
        Destroy(GameObject.Find("Cursor"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

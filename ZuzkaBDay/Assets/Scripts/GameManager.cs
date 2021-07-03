using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject platformPrefab;

    public int platformCount = 300;

    private void Start()
    {
        Vector3 spawnPosition = new Vector3();

        for (int i = 0; i < 9999; i++)
        {
            spawnPosition.y += Random.Range(2f, 4f);
            spawnPosition.x = Random.Range(-8f, 8f);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

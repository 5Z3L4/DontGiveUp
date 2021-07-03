using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] alco;
    public GameObject bad;
    public int height = 11;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRandomGameObject());
    }

    IEnumerator SpawnRandomGameObject()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));
        int randomAlco = Random.Range(0, alco.Length);
        if(Random.value <= .7f)
        {
            Instantiate(alco[randomAlco], new Vector2(Random.Range(-7, 8), height), Quaternion.identity);
        }
        else
        {
            Instantiate(bad, new Vector2(Random.Range(-8, 9), height), Quaternion.identity);
        }
        StartCoroutine(SpawnRandomGameObject());
    }
}

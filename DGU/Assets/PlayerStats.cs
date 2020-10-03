using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int healthPoints = 3;

    private void Update()
    {
        if (healthPoints <=0)
        {
            Destroy(gameObject);
        }
    }
}

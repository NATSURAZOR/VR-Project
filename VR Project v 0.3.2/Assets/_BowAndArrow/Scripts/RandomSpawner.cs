using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject enemy;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-30, 10), 15, Random.Range(-30, 10));
            Instantiate(enemy, randomSpawnPosition, Quaternion.identity);
        }
    }
}

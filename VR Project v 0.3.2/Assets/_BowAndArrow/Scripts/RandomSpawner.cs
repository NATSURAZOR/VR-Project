using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    private float rebootingTime;
    public float spawnTime = 5.0f;
    public GameObject gameMenu;
    public List<int> minusPosOfSpawnX;
    public List<int> plusPosOfSpawnlX;
    public List<int> minusPosOfSpawnZ;
    public List<int> plusPosOfSpawnlZ;


    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("gameMenu").Length != 0)
        {
            return;
        }

        rebootingTime += Time.deltaTime;

        if (rebootingTime >= spawnTime)
        {
            spawnEnemy();
            rebootingTime = 0;
        }
        
    }

    private Vector3 getSpawnPostion()
    {
        float x, y, z;
         
        y = 4.0f;

        float[] xRange = new float[] { Random.Range(minusPosOfSpawnX[0], minusPosOfSpawnX[1]), Random.Range(plusPosOfSpawnlX[0], plusPosOfSpawnlX[1]) };
        x = xRange[Random.Range(0, xRange.Length)];

        float[] zRange = new float[] { Random.Range(minusPosOfSpawnZ[0], minusPosOfSpawnZ[1]), Random.Range(plusPosOfSpawnlZ[0], plusPosOfSpawnlZ[1]) };
        z = zRange[Random.Range(0, 2)];

        return new Vector3(x, y, z);
    }

    private float enemyScale()
    {
       return Random.Range(0.9f, 1.1f);
    }
    private void spawnEnemy()
    {
        GameObject enemy =  enemies[Random.Range(0, 2)];
        enemy.transform.localScale *= enemyScale();
        Vector3 randomSpawnPosition = getSpawnPostion();
        Instantiate(enemy, randomSpawnPosition, Quaternion.identity);
    }
}

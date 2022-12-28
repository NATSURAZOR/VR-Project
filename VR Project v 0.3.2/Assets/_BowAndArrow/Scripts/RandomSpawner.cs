using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    private float rebootingTime;
    public float spawnTime = 5.0f;
    public GameObject gameMenu;

 

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

        float[] xRange = new float[] { Random.Range(-40f, -10f), Random.Range(10f, 40f) };
        x = xRange[Random.Range(0, xRange.Length)];

        float[] zRange = new float[] { Random.Range(-40f, -10f), Random.Range(10f, 40f) };
        z = zRange[Random.Range(0, 2)];


        return new Vector3(x, y, z);
    }

    //private float enemyScale()
    //{
        //return Random.Range(0.7f, 1.5f);
    //}
    private void spawnEnemy()
    {

        //enemy.transform.localScale *= enemyScale();
        Vector3 randomSpawnPosition = getSpawnPostion();
        Instantiate(enemies[Random.Range(0, 2)], randomSpawnPosition, Quaternion.identity);
    }
}

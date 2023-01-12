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
    public float minEnemySize;
    public int killsToWin;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("MainCamera")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("gameMenu").Length != 0)
        {
            return;
        }

        rebootingTime += Time.deltaTime;
        KillCounter counter = player.GetComponent<KillCounter>();
        if (counter.killCount >= killsToWin)
        {
            return;
        }
        
        if (rebootingTime >= spawnTime)
        {
       
            spawnEnemy();
            rebootingTime = 0;
        }
        
    }

    private Vector3 getSpawnPostion()
    {
        float x, y, z;
         
        y = 6.0f;

        float[] xRange = new float[] { Random.Range(minusPosOfSpawnX[0], minusPosOfSpawnX[1]), Random.Range(plusPosOfSpawnlX[0], plusPosOfSpawnlX[1]) };
        x = xRange[Random.Range(0, xRange.Length)];

        float[] zRange = new float[] { Random.Range(minusPosOfSpawnZ[0], minusPosOfSpawnZ[1]), Random.Range(plusPosOfSpawnlZ[0], plusPosOfSpawnlZ[1]) };
        z = zRange[Random.Range(0, 2)];

        return new Vector3(x, y, z);
    }

    private float enemyScale()
    {
       return Random.Range(minEnemySize, minEnemySize + 0.2f);
    }
    private void spawnEnemy()
    {
        GameObject enemy =  enemies[Random.Range(0, 2)];
        Vector3 randomSpawnPosition = getSpawnPostion();
        var test = Instantiate(enemy, randomSpawnPosition, Quaternion.identity);
        test.transform.localScale *= enemyScale();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TrainigRoomSpawner : MonoBehaviour
{
    public GameObject turtle;
    public GameObject slime;

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length < 2)
        {
            spawnEnemy(enemies);
        }
    }

    private void spawnEnemy(GameObject[] enemies)
    {
        GameObject enemy = getEnemyForSpawn(enemies);
        Vector3 enemyPos = getEnemyPos(enemy);
        Quaternion rotation = new Quaternion(0, -90, 0, 1);
        Instantiate(enemy, enemyPos, rotation);
    }

    private Vector3 getEnemyPos(GameObject enemy)
    {
        if (enemy == turtle)
        {
            return new Vector3(7.65f, 0.0f, 0.0f);
        }
        return new Vector3(7.37f, 0.0f, -2.38f);
    }

    private GameObject getEnemyForSpawn(GameObject[] enemies)
    {
        if (enemies[0] != turtle)
        {
            return turtle;
        }
        return slime;
    }
}

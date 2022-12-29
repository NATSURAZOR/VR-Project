using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrainigRoomSpawner : MonoBehaviour
{
    public GameObject turtle;
    public GameObject slime;
    private float time = 0.9f;
    public float rebootingTime = 1.0f;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= rebootingTime) {
            time = 0;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
       
            if (enemies.Length < 2 && enemies.Length > 0)
            {
                Debug.Log(enemies[0].name);
                spawnEnemy(enemies);
                return;
            }

            if (enemies.Length == 0)
            {
                spawnEnemy(turtle);
                spawnEnemy(slime);
            }
        }
    }
    
    private void spawnEnemy(GameObject enemy)
    {
        Vector3 enemyPos = getEnemyPos(enemy);
        Quaternion rotation = Quaternion.Euler(0, -90, 0);
        Instantiate(enemy, enemyPos, rotation);
    }

    private void spawnEnemy(GameObject[] enemies)
    {
        GameObject enemy = getEnemyForSpawn(enemies);
        Vector3 enemyPos = getEnemyPos(enemy);
        Quaternion rotation = Quaternion.Euler(0, -90, 0);
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
        if (enemies[0].name == "TurtleShellTrainRoom(Clone)")
        {
            return slime;
        }
       
        return turtle;
        
       
        
    }
}

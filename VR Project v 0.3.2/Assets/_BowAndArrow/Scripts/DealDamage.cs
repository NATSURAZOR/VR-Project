using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class DealDamage : MonoBehaviour
{
    private GameObject player;
    public int enemyDamage = 50;

    private float rebootingTime;
    private float damageTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("MainCamera")[0];
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        rebootingTime += Time.deltaTime;

        if (distance <= 5f)
        {
            if (rebootingTime >= damageTime)
            {
                Damageable d = player.transform.gameObject.GetComponent<Damageable>();
                if (d)
                {
                    d.DealDamage(enemyDamage);
                    rebootingTime = 0;
                    Debug.Log("damage player");
                }
            }

          
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class DealDamage : MonoBehaviour
{
    private GameObject player;
    public int enemyDamage = 50;

    public Animator myAnim;

    private float rebootingTime;
    private float damageTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("gameMenu").Length != 0)
        {
            myAnim.enabled = false;
            return;
        }

        if (myAnim.enabled == false)
        {
            myAnim.enabled = true;
        }
        
        float distance = Vector3.Distance(transform.position, player.transform.position);
        rebootingTime += Time.deltaTime;

        Damageable d = player.transform.gameObject.GetComponent<Damageable>();
        if (d.Health <= 0)
        {
            myAnim.Play("Victory");
        }
        else
        {
            if (distance <= 2.0f)
            {
                transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                if (d && rebootingTime >= damageTime)
                {
                    myAnim.Play("Attack01");
                    d.DealDamage(enemyDamage);
                    rebootingTime = 0;
                    Debug.Log("damage player");
                }

            }


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class EnemyDie : MonoBehaviour
{
    private float endTimer = 2.0f;
    private float time = 0;
    public Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Damageable d = transform.gameObject.GetComponent<Damageable>();
      
        if (d.Health <= 0)
        {
            GameObject player = GameObject.FindGameObjectsWithTag("MainCamera")[0];
            KillCounter counter = player.GetComponent<KillCounter>();
            
            time += Time.deltaTime;
            myAnim.Play("Die");
            if (time >= endTimer)
            {
                if (counter)
                {
                    counter.killCount += 1;
                }
                d.DestroyThis();
            }
        }
    }
}

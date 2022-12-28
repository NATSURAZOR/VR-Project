using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 10f;
    private GameObject player;

    private Vector3 heading;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        transform.LookAt(player.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameObject.FindGameObjectsWithTag("gameMenu").Length != 0)
        {
            return;
        }

       
        KillCounter counter = player.GetComponent<KillCounter>();
        if (counter.killCount >= 20)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -1 * speed * Time.deltaTime);
            return;
        }
        

        heading = transform.position - player.transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        // transform.position -= direction * Time.deltaTime * speed;
        if (distance <= 2.0f)
        {
            transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        }
        else {

         
            transform.LookAt(player.transform.position);
            var lookPos = player.transform.position - transform.position;
            lookPos.y = player.transform.rotation.y;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 0.125f);
            

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position,  speed * Time.deltaTime);
        }
        
        
            
    }


}

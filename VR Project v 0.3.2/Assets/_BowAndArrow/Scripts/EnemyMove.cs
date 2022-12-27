using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 10f;
    private GameObject player;
    public GameObject gameMenu;
    private Vector3 heading;
    private float time;
    private float checkRotationTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        transform.LookAt(player.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameMenu.activeSelf) {
            heading = transform.position - player.transform.position;
            time += Time.deltaTime;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;

            // transform.position -= direction * Time.deltaTime * speed;
            if (distance <= 2.0f)
            {
                transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            }
            else {
                if (time >= checkRotationTime)
                {
                    transform.LookAt(player.transform.position);
                }
      
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
        
            
    }


}

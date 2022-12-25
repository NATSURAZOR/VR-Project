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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        heading = transform.position - player.transform.position;

        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        transform.position -= direction * Time.deltaTime * speed;

        transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y + 1.3f, player.transform.position.z ));
    }


}

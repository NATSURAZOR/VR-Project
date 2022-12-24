using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 10f;
    public GameObject player;

    private Vector3 position;
    private Vector3 heading;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        heading = transform.position - player.transform.position;

        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        transform.position -= direction * Time.deltaTime * speed;
    }
}

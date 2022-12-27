using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerHP : MonoBehaviour
{
    private GameObject player;
    public TextMeshProUGUI text;
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        Damageable d = player.transform.gameObject.GetComponent<Damageable>();

        text.text = d.Health + " HP";
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = target.position + target.rotation * locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset);
        Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
        transform.rotation = smoothedrotation;

        Damageable d = player.transform.gameObject.GetComponent<Damageable>();
        if (d)
        {
            text.text = d.Health + " HP";
            if(d.Health <= 150 && d.Health > 50)
            {
                text.color = new Color32(255, 150, 0, 255);
            }
            if(d.Health <= 50)
            {
                text.color = new Color32(255, 0, 22, 255);
            }
        }
    }
}

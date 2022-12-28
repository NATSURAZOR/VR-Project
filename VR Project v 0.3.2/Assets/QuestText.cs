using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestText : MonoBehaviour
{
    private GameObject player;
    public TextMeshProUGUI text;
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;
    private float endTimer = 4.0f;
    private float time = 0;
    private float secondTime = 0;

    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        canvas = GetComponent<Canvas>();
        text.text = "Advanturer first steps.\nKill 20 slimes or Turtle:\n0 / 15";
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time <= endTimer)
        {
            return;
        }

        KillCounter counter = player.GetComponent<KillCounter>();
        if (counter.killCount >= 20 && secondTime <= endTimer)
        {
            secondTime += Time.deltaTime;
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            text.text = "Quest is done!!!";
            text.fontSize = 36;
            return;
        }

        if (secondTime >= endTimer)
        {
            text.text = "Enemies run away!";
            return;
        }


        text.text = counter.killCount + " / 20";
        
        canvas.renderMode = RenderMode.WorldSpace;
   
        
        text.fontSize = 16;
        text.alignment = TextAlignmentOptions.Center;
        

        Vector3 desiredPosition = target.position + target.rotation * locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset);
        Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
        transform.rotation = smoothedrotation;
    }
}

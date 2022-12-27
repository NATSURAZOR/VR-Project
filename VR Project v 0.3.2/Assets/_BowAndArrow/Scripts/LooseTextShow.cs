using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
public class LooseTextShow : MonoBehaviour
{
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
   
      
    }

    // Update is called once per frame
    void Update()
    {
        Damageable d = transform.gameObject.GetComponent<Damageable>();
        if (d.Health <= 0)
        {
            text.transform.gameObject.SetActive(true);
        }
    }
}

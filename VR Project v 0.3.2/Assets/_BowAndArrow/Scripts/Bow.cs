using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bow : XRGrabInteractable
{
   
    private void Update()
    {
        if (isSelected)
        {
           
            foreach (Collider c in GetComponents<Collider>())
            {
                c.enabled = false;
               
            }
        }
        else
        {
            foreach (Collider c in GetComponents<Collider>())
            {
                c.enabled = true;

            }
        }
    }

  
}

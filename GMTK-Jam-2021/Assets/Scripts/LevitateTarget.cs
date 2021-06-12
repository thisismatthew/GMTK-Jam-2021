using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateTarget : MonoBehaviour
{
    public LevitatorController controller;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Liftable")
        {
            controller.LevitationObject = collision.gameObject; 
        }
    }

}

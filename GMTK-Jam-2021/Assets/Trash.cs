using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public GameObject[] trash;
    public TrashCounter counter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(GameObject t in trash)
        {
            if (t == collision.gameObject)
            {
                t.SetActive(false);
                counter.counter--;
            }
        }
    }
}

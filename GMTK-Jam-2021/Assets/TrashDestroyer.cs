using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashDestroyer : MonoBehaviour
{

    public GameObject[] trash;
    public TrashCounter counter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Core")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        foreach (GameObject t in trash)
        {
            if (t == collision.gameObject)
            {
                t.SetActive(false);
                counter.counter--;
            }
        }
          
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChompDestroyer : MonoBehaviour
{
    public GameObject[] trash;
    private Animator animator;
    public Sprite sprite;
    public TrashCounter counter;



    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "Core" && counter.counter > 0)
        {
            animator.Play("ChompAnim");

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        foreach (GameObject t in trash)
        {
            if (t == collision.gameObject)
            {
                animator.Play("ChompAnim");
                t.SetActive(false);
                counter.counter--;
                
            }
        }

        if (counter.counter == 0)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
        }

    }
}

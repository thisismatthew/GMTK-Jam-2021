using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : MonoBehaviour
{
    public Sprite[] faces;
    public int counter;
    private SpriteRenderer renderer;
    public Animator doorAnimator;

    private void Start()
    {
        counter = faces.Length -1;
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter <= 0)
        {
            doorAnimator.Play("DoorAnim");
            counter = 0;
        }
        renderer.sprite = faces[counter];
    }
}

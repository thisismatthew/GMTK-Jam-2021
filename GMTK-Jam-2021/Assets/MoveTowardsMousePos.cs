using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsMousePos : MonoBehaviour
{
    public Camera cam;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {

        rb.MovePosition(Vector2.Lerp(transform.position, cam.ScreenToWorldPoint(Input.mousePosition), 0.01f));
    }
}

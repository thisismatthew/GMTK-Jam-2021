using UnityEngine;

public class Attatcher : MonoBehaviour
{
    public Controller LocalController;
    private FixedJoint2D joint;

    private void Start()
    {
        joint = GetComponent<FixedJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Core")
        {
            Debug.Log("Attaching");
            PlayerInput input = collision.gameObject.GetComponent<PlayerInput>();
            input.CurrentController = LocalController;
            joint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }
}

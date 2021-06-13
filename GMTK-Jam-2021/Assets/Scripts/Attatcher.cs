using UnityEngine;

public class Attatcher : MonoBehaviour
{
    public bool CoreConected;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CoreConected)
        {
            if (collision.gameObject.tag == "Core")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                collision.gameObject.GetComponent<CoreController>().ConnectToHost(this.transform.parent.gameObject, this.gameObject);
                CoreConected = true;
            }
           
        }
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterRotation : MonoBehaviour
{
    private Transform _parent;
    void Start()
    {
        _parent = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.identity;
    }
}

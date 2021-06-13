using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitatorController : Controller
{
    public GameObject Lazerbeam;
    private GameObject _lazerinstance;
    public Transform _target;
    private Vector2 _newTargetPos;
    public GameObject LevitationObject;
    public LayerMask BarrierLayer;

    private void Awake()
    {
        _newTargetPos = _target.position;
    }

    private void FixedUpdate()
    {
        //Target controlls
        if (_horizontalInput > 0.01f || _horizontalInput < -0.01f)
        {
            
            _newTargetPos.x = _target.transform.position.x + _horizontalInput;
        }
        if (_verticalInput > 0.01f || _verticalInput < -0.01f)
        {
            _newTargetPos.y = _target.transform.position.y + _verticalInput;
        }
        _target.position = Vector2.Lerp(_target.position, _newTargetPos, 0.05f);

        if (LevitationObject != null)
        {
            if (_jumpCharging)
            {
                if (_lazerinstance == null)
                {
                    Debug.Log("lazerCreated");
                    _lazerinstance = Instantiate(Lazerbeam);
                }
                
                RaycastHit2D barrierHit = Physics2D.Raycast(GroundingRoot.position, _target.position - GroundingRoot.position, BarrierLayer);
                _lazerinstance.GetComponent<LineRenderer>().SetPosition(0, GroundingRoot.position);
                _lazerinstance.GetComponent<LineRenderer>().SetPosition(1, barrierHit.point);
                Debug.DrawLine(GroundingRoot.position, barrierHit.point, Color.green);


                if (barrierHit.collider.gameObject.tag != "Environment")
                {
                    LevitationObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    LevitationObject.transform.position = Vector2.Lerp(_target.position, LevitationObject.transform.position, 0.05f);
                }
                
            }
        }

        if (!_jumpCharging && _lazerinstance != null)
        {
            Debug.Log("Destroyed");
            Destroy(_lazerinstance);
            _lazerinstance = null;
        }

    }

}

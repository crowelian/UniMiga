using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSwitcher : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform endPoint;
    private float _current, _target;
    private float _speed = 4f;

    [SerializeField] AnimationCurve _curve;

    // Start is called before the first frame update
    void Start()
    {
        if (startPoint == null || endPoint == null)
        {
            Destroy(this);
        }
    }

    public void SwitchPosition()
    {
        if (_target == 1)
        {
            _target = 0;
        }
        else
        {
            _target = 1;
        }
    }

    public bool GetIsInPosition()
    {
        if (_target >= 1)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {

        Move();
    }

    void Move()
    {
        _current = Mathf.MoveTowards(_current, _target, _speed * Time.deltaTime);
        float time = _curve.Evaluate(_current);

        if (time > 0) // check that animation done 
        {
            transform.position = Vector3.Lerp(startPoint.transform.position, endPoint.transform.position, time);

            transform.rotation = Quaternion.Lerp(Quaternion.Euler(startPoint.transform.rotation.eulerAngles), Quaternion.Euler(endPoint.transform.rotation.eulerAngles), _curve.Evaluate(_current));

        }
    }
}

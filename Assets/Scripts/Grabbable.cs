using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Transform grabPointTransform;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Grab(Transform grabPointTransform)
    {
        this.grabPointTransform = grabPointTransform;
        rigidbody.useGravity = false;
    }

    public void Drop()
    {
        this.grabPointTransform = null;
        rigidbody.useGravity = true;
    }

    private void FixedUpdate()
    {
        if (grabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, grabPointTransform.position, Time.deltaTime * lerpSpeed);
            rigidbody.MovePosition(newPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, grabPointTransform.rotation, Time.deltaTime * lerpSpeed);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubbleMovement : MonoBehaviour
{
    public float startRotationZ;
    public float rotationSpeed = 1f;
    public bool rotateRight = true;
    public float maxRotation = 0.05f;

    void Start()
    {
        startRotationZ = transform.rotation.z;
    }

    void Update()
    {
        if (rotateRight)
        {
            transform.Rotate( 0, 0, 1 * Time.deltaTime * rotationSpeed);
            if(transform.rotation.z >= maxRotation) rotateRight = false;
        }
        else
        {
            transform.Rotate(0, 0, -1 * Time.deltaTime * rotationSpeed);
            if (transform.rotation.z <= -maxRotation) rotateRight = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappingEars : MonoBehaviour
{
    public GameObject leftEar;
    public GameObject rightEar;
    public GameObject leftEarFlap;
    public GameObject rightEarFlap;
    public float speed = 100;

    private bool rotationUp = true;

    void Update()
    {
        if (rotationUp)
        {
            leftEarFlap.transform.Rotate(0, 0, 1 * Time.deltaTime * speed);
            rightEarFlap.transform.Rotate(0, 0, -1 * Time.deltaTime * speed);
            leftEar.transform.Rotate(0, 0, -0.3f * Time.deltaTime * speed);
            rightEar.transform.Rotate(0, 0, 0.3f * Time.deltaTime * speed);

            if (leftEarFlap.transform.localRotation.z >= 0.2)
            {
                rotationUp = false;
            }
        }

        if (!rotationUp)
        {
            leftEarFlap.transform.Rotate(0, 0, -1 * Time.deltaTime * speed);
            rightEarFlap.transform.Rotate(0, 0, 1 * Time.deltaTime * speed);
            leftEar.transform.Rotate(0, 0, 0.3f * Time.deltaTime * speed);
            rightEar.transform.Rotate(0, 0, -0.3f * Time.deltaTime * speed);

            if (leftEarFlap.transform.localRotation.z <= -0.14)
            {
                rotationUp = true;
            }
        }
    }
}

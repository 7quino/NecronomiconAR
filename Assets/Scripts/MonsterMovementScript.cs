using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementScript : MonoBehaviour
{
    public GameObject eyeL;
    public GameObject eyeR;
    public float speedBody = 100;
    public float speedEyes = 100;

    private bool movingUp = true;

    // Update is called once per frame
    void Update()
    {
        if (movingUp)
        {
            eyeL.transform.Rotate(0, 0, -1 * Time.deltaTime * speedEyes);
            eyeR.transform.Rotate(0, 0, 1 * Time.deltaTime * speedEyes);
            eyeL.transform.localScale -= new Vector3(0, 0.001f, 0);
            eyeR.transform.localScale -= new Vector3(0, 0.001f, 0);
            transform.Translate(0, (1f * Time.deltaTime * speedBody), 0);

            if (transform.localPosition.y >= 0.2)
            {
                movingUp = false;
            }
        }

        if (!movingUp)
        {
            eyeL.transform.Rotate(0, 0, 1 * Time.deltaTime * speedEyes);
            eyeR.transform.Rotate(0, 0, -1 * Time.deltaTime * speedEyes);
            eyeL.transform.localScale += new Vector3(0, 0.001f, 0);
            eyeR.transform.localScale += new Vector3(0, 0.001f, 0);
            transform.Translate(0, (-1f * Time.deltaTime * speedBody), 0);

            if (transform.localPosition.y <= -0.2)
            {
                movingUp = true;
            }
        }
    }
}

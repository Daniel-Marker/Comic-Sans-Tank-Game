using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFaceCamera : MonoBehaviour
{
    //todo, set position of text so that it always looks like it's above the head

    void Update()
    {
        transform.LookAt(Camera.main.transform);

        Vector3 currRot = transform.rotation.eulerAngles;
        currRot.y += 180;
        transform.rotation = Quaternion.Euler(currRot);
    }
}

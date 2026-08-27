#nullable disable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateScript : MonoBehaviour
{
    public GameObject objectToRotate;
    public float rotateSpeed = 5f;
    void FixedUpdate()
    {
        objectToRotate.transform.Rotate(new Vector3(0, rotateSpeed, 0));
    }
}

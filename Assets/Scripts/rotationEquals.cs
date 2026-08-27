#nullable disable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationEquals : MonoBehaviour
{
    public Transform cloneFrom;
    public Transform cloneTo;
    void LateUpdate()
    {
        cloneTo.rotation = cloneFrom.rotation;
    }
}

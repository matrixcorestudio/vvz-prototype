using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    public void Rotate(GameObject toRotate)
    {
        toRotate.transform.Rotate(new Vector3(0, 0, -90));
    }
}

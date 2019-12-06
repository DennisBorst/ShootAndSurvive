using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate( new Vector3(0, rotationSpeed, 0), Space.Self);
    }
}

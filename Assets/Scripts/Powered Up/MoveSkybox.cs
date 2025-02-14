using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class MoveSkybox : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.back * speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatSkybox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -65)
        {
            transform.position = Vector3.zero;
        }
    }
}

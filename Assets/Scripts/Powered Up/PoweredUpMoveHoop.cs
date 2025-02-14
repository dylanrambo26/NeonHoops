using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredUpMoveHoop : MonoBehaviour
{
    public float speed;

    private bool movingLeft;

    private bool movingRight;

    private PoweredUpManager poweredUpManagerScript;

    private Rigidbody hoopRB;
    private Vector3 hoopStartPos;

    //private bool isClassicMode;
    // Start is called before the first frame update
    void Start()
    {
        movingLeft = true;
        movingRight = false;
        poweredUpManagerScript = GameObject.Find("GameManager").GetComponent<PoweredUpManager>();
        hoopRB = GetComponent<Rigidbody>();
        hoopStartPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!poweredUpManagerScript.freezeHoopActivated)
        {
            translateHoop();
        }
        else
        {
            hoopRB.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void translateHoop()
    {
        if (movingLeft && poweredUpManagerScript.isGameActive)
        {
            transform.Translate(Vector3.left * speed);
        }
        else if (movingRight && poweredUpManagerScript.isGameActive)
        {
            transform.Translate(Vector3.right * speed);
        }
        else
        {
            transform.position = hoopStartPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Left Bound"))
        {
            movingLeft = false;
            movingRight = true;
        }
        else if(other.gameObject.CompareTag("Right Bound"))
        {
            movingLeft = true;
            movingRight = false;
        }
    }
}

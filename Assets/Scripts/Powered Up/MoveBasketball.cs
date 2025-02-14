using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class MoveBasketball : MonoBehaviour
{
    //public Vector3 cursorLocation;
    public GameObject cameraObject;
    private Camera mainCamera;
    private float leftBound = -10;

    private Rigidbody ballRb;
    //private float rightBound = -5.4f;

    public float cursorXAxis;
    private Quaternion resetRotation;
    private bool ballReady;
    //private bool multishotActivated;
    public float ballForce;

    public Vector3 ballDirection;
    public Vector3 ballRotation;

    private Vector3 resetPos;

    private BallSpawner ballSpawnerScript;

    private PoweredUpManager poweredUpManagerScript;
    [SerializeField] private Material moneyBallMaterial;
    public Renderer ballRenderer;

    private GameObject box;

    private Renderer[] sidesOfBox;

    public Material boxOriginalMat;

    private Color redBoxColor;
    private Color greenBoxColor;

    private Color moneyBallBoxColor;
    private Color pinkBoxColor;
    private Color cyanBoxColor;
    private Color freezeBoxColor;

    private float boxColorSeconds;
   // public GameObject[] balls;
    // Start is called before the first frame update
    void Start()
    {
        ballReady = true;
        cameraObject = GameObject.FindWithTag("MainCamera");
        mainCamera = cameraObject.GetComponent<Camera>();
        ballRb = GetComponent<Rigidbody>();
        ballRb.constraints = RigidbodyConstraints.FreezePositionY;
        resetRotation = transform.rotation;
        ballSpawnerScript = GameObject.Find("BallState").GetComponent<BallSpawner>();
        poweredUpManagerScript = GameObject.Find("GameManager").GetComponent<PoweredUpManager>();
        box = GameObject.Find("Box");
        sidesOfBox = box.GetComponentsInChildren<Renderer>();
        ballRenderer = GetComponent<Renderer>();
        greenBoxColor = new Color(0,1,0,.4f);
        redBoxColor = new Color(1,0,0,.4f);
        moneyBallBoxColor = new Color(1, .5f, 0);
        pinkBoxColor = new Color(1, 0, .5f, 0.4f);
        cyanBoxColor = Color.cyan;
        freezeBoxColor = new Color(0, 0, 1, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        cursorXAxis = mainCamera.ScreenToViewportPoint(Input.mousePosition).x;
        PositionBall();
        resetPos = transform.position;
        ShootBall();
    }

    void PositionBall()
    {
        if (ballReady)
        {
            transform.position = new Vector3(leftBound + 4.6f * cursorXAxis, 12 , -10.75f);
        }
        
    }
    void ShootBall()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ballReady && poweredUpManagerScript.isGameActive)
        {
            ballReady = false;
            ballRb.constraints = RigidbodyConstraints.None;
            ballRb.AddForce(ballDirection * ballForce, ForceMode.Impulse);
            ballRb.AddTorque(ballRotation);
        }
    }
    void ResetBall()
    {
        if (ballReady)
        {
            transform.position = resetPos;
            transform.rotation = resetRotation;
            ballRb.constraints = RigidbodyConstraints.FreezeAll;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            ballReady = true;
            ResetBall();
        }
        else if (other.gameObject.CompareTag("Power Up"))
        {
            if (other.gameObject.name.Equals("Freeze Hoop(Clone)"))
            {
                Debug.Log("hit freeze");
                boxColorSeconds = poweredUpManagerScript.freezeHoopRate;
                foreach (Renderer sideRenderer in sidesOfBox)
                {
                    StartCoroutine(boxColorChange(sideRenderer, freezeBoxColor, boxColorSeconds));
                }
                poweredUpManagerScript.freezeHoopActivated = true;
                StartCoroutine(poweredUpManagerScript.FreezeHoopCountdown());
            }
            else if (other.gameObject.name.Equals("Force Field Powerup(Clone)"))
            {
                Debug.Log("hit force");
                boxColorSeconds = poweredUpManagerScript.forceFieldRate;
                foreach (Renderer sideRenderer in sidesOfBox)
                {
                    StartCoroutine(boxColorChange(sideRenderer, cyanBoxColor, boxColorSeconds));
                }
                poweredUpManagerScript.forceFieldActivated = true;
                StartCoroutine(poweredUpManagerScript.ForceFieldCountdown());
            }
            else if (other.gameObject.name.Equals("Money Ball Powerup(Clone)"))
            {
                Debug.Log("hit money");
                boxColorSeconds = poweredUpManagerScript.moneyBallRate;
                foreach (Renderer sideRenderer in sidesOfBox)
                {
                    StartCoroutine(boxColorChange(sideRenderer, moneyBallBoxColor, boxColorSeconds));
                }
                ballRenderer.material = moneyBallMaterial;
                poweredUpManagerScript.moneyBallActivated = true;
                StartCoroutine(poweredUpManagerScript.MoneyBallCountdown());
            }
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            boxColorSeconds = 0.5f;
            foreach (Renderer sideRenderer in sidesOfBox)
            {
                StartCoroutine(boxColorChange(sideRenderer, redBoxColor, boxColorSeconds));
            }
            Destroy(other.gameObject);
            poweredUpManagerScript.SubtractLife();
        }
        else if (other.gameObject.CompareTag("Plus One"))
        {
            boxColorSeconds = 0.5f;
            foreach (Renderer sideRenderer in sidesOfBox)
            {
                StartCoroutine(boxColorChange(sideRenderer, pinkBoxColor, boxColorSeconds));
            }
            Destroy(other.gameObject);
            poweredUpManagerScript.AddLife();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sensor"))
        {
            boxColorSeconds = 0.5f;
            foreach (Renderer sideRenderer in sidesOfBox)
            {
                StartCoroutine(boxColorChange(sideRenderer, greenBoxColor, boxColorSeconds));
            }
            poweredUpManagerScript.AddScore();
        }
    }

    IEnumerator boxColorChange(Renderer renderer, Color newColor, float seconds)
    {
        renderer.material.color = newColor;
        yield return new WaitForSeconds(seconds);
        renderer.material.color = boxOriginalMat.color;
    }
}

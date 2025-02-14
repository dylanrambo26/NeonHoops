using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color startColor;

    public float rValue;

    public float gValue;

    public float bValue;
    //public float transparency;

    public Renderer objectRenderer;

    private float seconds;
    // Start is called before the first frame update
    void Start()
    {
        startColor = Color.red;
        rValue = startColor.r;
        gValue = startColor.g;
        bValue = startColor.b;
        seconds = 0.1f;
        objectRenderer = GetComponent<Renderer>();
        //transparency = objectRenderer.material.color.a;
        StartCoroutine("ChangeColorsOverTime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSingleColor(Color newColor)
    {
        objectRenderer.material.color = newColor;
    }

    IEnumerator ChangeColorsOverTime()
    {
        while (bValue < 1)
        {
            bValue += 0.1f;
            objectRenderer.material.color = new Color(rValue, gValue, bValue);
            yield return new WaitForSeconds(seconds);
        }
        
        while (rValue >= 0.1f)
        {
            rValue -= 0.1f;
            objectRenderer.material.color = new Color(rValue, gValue, bValue);
            yield return new WaitForSeconds(seconds);
        }
        
        rValue = 0;

        while (gValue < 1)
        {
            gValue += 0.1f;
            objectRenderer.material.color = new Color(rValue, gValue, bValue);
            yield return new WaitForSeconds(seconds);
        }
        
        while (bValue >= 0.1f)
        {
            bValue -= 0.1f;
            objectRenderer.material.color = new Color(rValue, gValue, bValue);
            yield return new WaitForSeconds(seconds);
        }

        bValue = 0;
        
        while (rValue < 1)
        {
            rValue += 0.1f;
            objectRenderer.material.color = new Color(rValue, gValue, bValue);
            yield return new WaitForSeconds(seconds);
        }
        
        while (gValue >= 0.1f)
        {
            gValue -= 0.1f;
            objectRenderer.material.color = new Color(rValue, gValue, bValue);
            yield return new WaitForSeconds(seconds);
        }

        gValue = 0;
        StartCoroutine("ChangeColorsOverTime");
    }
}

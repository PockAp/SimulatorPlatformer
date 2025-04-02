using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundScript : MonoBehaviour
{
    public GameObject cameraa;
    public float paralax = 0.7f;
    private float dlina;
    private float XstartposX;
    

    void Start()
    {
        XstartposX = transform.position.x;
        dlina = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = cameraa.transform.position.x * (1 - paralax);
        float distance = cameraa.transform.position.x * paralax;
        transform.position = new Vector3(XstartposX + distance, transform.position.y, transform.position.z);
        if(temp > XstartposX + dlina)
        {
            XstartposX += dlina;
        }
        else if(temp < XstartposX - dlina)
        {
            XstartposX -= dlina;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
            touchStart.z = 76;
            touchStart =  Camera.main.ScreenToWorldPoint(touchStart);
            print(touchStart);
        }
        if(Input.GetMouseButton(0)){
            Vector3 tmpMousePos = Input.mousePosition;
            tmpMousePos.z = 76;
            tmpMousePos =  Camera.main.ScreenToWorldPoint(tmpMousePos); 
            Vector3 direction = touchStart - tmpMousePos;
            this.transform.position += direction;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFruit : MonoBehaviour
{
    Target target;
    Vector3 startHitPos;

    private void Start()
    {
        target = GetComponent<Target>();
    }
    private void OnMouseEnter()
    {
        //print($"mouse entered target");
        if (Input.GetMouseButton(0))
        {
            //print($"mouse enter");
            startHitPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseOver()
    {
        //print($"on mouse over");
        if (Input.GetMouseButtonDown(0))
        {
            startHitPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            //print($"mouseover");
            Vector3 mouseToWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //print($"{(mouseToWorldPos - startHitPos).magnitude}");
            if ((mouseToWorldPos - startHitPos).magnitude > 0.2)
            {
                //print($"targetHit");
                target.isHit = true;
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Rigidbody rb;

    public Vector2 maxVelocity = new Vector3(4f, 13f);
    public float xRange = 9.5f;
    public float yRange = -0.6f;
    public float torque = 1f;
    public bool isHit = false;
    public bool isDestroyed = false;
    public bool isStart = false;
    public float rotationSpeed = 1f;

    Vector3 startHitPos;
    Vector3 rotationVector;
    


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (!isStart)
        {
            float yScale = Random.Range(0.7f, 1f);
            float xScale = Random.Range(0f, 1f);
            float dir = transform.position.x < 0 ? 1f : -1f;

            rb.velocity = new Vector3(maxVelocity.x * xScale * dir, maxVelocity.y * yScale, 0);
            rb.AddTorque(Random.Range(0f, 1f) * torque, Random.Range(0f, 1f) * torque, Random.Range(0f, 1f) * torque, ForceMode.Impulse);
        }
        else
        {
            rotationVector = Vector3.one * Random.Range(0.1f, 0.9f);
        }

    }

    void Update()
    {
        if (isStart)
        {
            transform.RotateAround(transform.position, rotationVector, rotationSpeed * Time.deltaTime);
        }
        if (transform.position.y < yRange)
        {
            isDestroyed = true;
            Destroy(gameObject, 1);
        }
    }

    //private void OnMouseEnter()
    //{
    //    print($"mouse entered target");
    //    if (Input.GetMouseButton(0))
    //    {
    //        //print($"mouse enter");
    //        startHitPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    }
    //}

    //private void OnMouseOver()
    //{
    //    //print($"mouse over target");
    //    if (Input.GetMouseButton(0))
    //    {
    //        //print($"mouseover");
    //        Vector3 mouseToWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        //print($"{(mouseToWorldPos - startHitPos).magnitude}");
    //        if ((mouseToWorldPos - startHitPos).magnitude > 0.2)
    //        {
    //            //print($"targetHit");
    //            isHit = true;
    //        }
    //    }
    //}
}

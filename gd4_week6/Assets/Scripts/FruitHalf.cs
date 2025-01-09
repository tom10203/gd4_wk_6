using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitHalf : MonoBehaviour
{
    public GameObject fruit;
    public bool isLeft;
    public float force;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        float left = Vector3.Dot(Vector3.left, fruit.transform.right);
        float up = Vector3.Dot(Vector3.up, fruit.transform.right);
        Vector3 forceToAdd;
        if (isLeft)
        {
            forceToAdd = Vector3.right * left + Vector3.down * up;
            
        }
        else
        {
            forceToAdd = Vector3.left * left + Vector3.up * up;
        }

        rb.AddForce(forceToAdd * force);
    }

}

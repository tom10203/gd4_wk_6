using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ParticleSystem ps;
    ParticleSystemRenderer psr;
    public GameObject lineGO;
    Line line;
    void Start()
    {
        line = lineGO.AddComponent<Line>();
        psr = ps.GetComponent<ParticleSystemRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Vector2 lineAngle = new Vector2(line.currentDirection.x, line.currentDirection.y);
            float angle = GetAngle(lineAngle, Vector2.up);

            if (angle < 0)
            {
                angle = 360 + angle;
            }

            angle = Mathf.Deg2Rad * angle + Mathf.Deg2Rad * 19.74f;

            if (Input.GetKeyDown(KeyCode.P))
            {
                var main = ps.main;

            }
        }
    }

    private float GetAngle(Vector2 v1, Vector2 v2)
    {
        var sign = Mathf.Sign(v1.x * v2.y - v1.y * v2.x);
        return Vector2.Angle(v1, v2) * sign;
    }
}

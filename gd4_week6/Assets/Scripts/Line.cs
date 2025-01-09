using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float segmentLength = 0.1f;
    public float maxNoOfSegments;
    float dequeTimer = 0f;

    Queue<Vector3> points = new Queue<Vector3>();
    bool drawLine = false;
    Vector3 lastPos;
    Vector3 mouseToWorldPos;

    public Vector3 currentDirection;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            points.Clear();
            drawLine = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            drawLine = true;
            lastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (drawLine)
        {
            mouseToWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            

            if (CheckDistance(lastPos, mouseToWorldPos))
            {

                currentDirection = mouseToWorldPos - lastPos; // used for particle effects

                //CheckCollision(lastPos, mouseToWorldPos, -2.4f); // Check for bomb collisions
                CheckCollision(lastPos, mouseToWorldPos, -3.9f); // Check for fruit collisions

                points.Enqueue(AdjustMouseToWorlPos(mouseToWorldPos));
                if (points.Count > maxNoOfSegments)
                {
                    points.Dequeue();
                }
                lastPos = mouseToWorldPos;
            }
            else if ((lastPos - mouseToWorldPos).magnitude <= 0.05)
            {
                dequeTimer -= Time.deltaTime;
                if (points.Count > 0 && dequeTimer <= 0)
                {
                    points.Dequeue();
                    dequeTimer = 0.01f;
                }
            }
        }

        DrawLine();

    }

    bool CheckDistance(Vector3 point1, Vector3 point2)
    {
        float dst = (point1 - point2).magnitude;
        if (dst >= segmentLength)
        {
            return true;
        }
        return false;
    }


    void DrawLine()
    {
        lineRenderer.positionCount = points.Count;
        int i = 0;
        foreach (Vector3 point in points)
        {
            lineRenderer.SetPosition(i, point);
            i++;
        }
    }

    Vector3 AdjustMouseToWorlPos(Vector3 pos)
    {
        return new Vector3(pos.x, pos.y, -3f);
    }

    void CheckCollision(Vector3 from, Vector3 to, float z)
    {

        from = new Vector3(from.x, from.y, z);
        to = new Vector3(to.x, to.y, z);

        Vector3 dir = to - from;
        Debug.DrawRay(to, dir, Color.yellow);

        RaycastHit[] hits;

        hits = Physics.RaycastAll(from, dir, dir.magnitude);

        for (int i = 0; i < hits.Length; i++)
        {
            Target target = hits[i].transform.GetComponent<Target>();
            target.isHit = true;
        }

        

    }


}

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

    float startZ = -1.28f;
    float inGameZ = -3.9f;
    float zz;

    public int melonsHitThisFrame = 0;

    bool setDirection = true;
    Vector3 startDir;

    float timer = 0f;
    [SerializeField] float reduceLineLengthTime = 0.1f;

    int combo = 0;

    [SerializeField] GameObject[] comboImages;
    [SerializeField] float minX, maxX, minY, maxY;

    UIManager uIManager;

    bool playSlash = true;
    [SerializeField] AudioSource slash;
    [SerializeField] AudioSource fruitSplat;

    public bool gameOver = false;

    private void Start()
    {
        uIManager = FindFirstObjectByType<UIManager>();
        lineRenderer = GetComponent<LineRenderer>();
        zz = startZ;
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            points.Clear();
            drawLine = false;
            ResetCombo();
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
                timer = Time.time + reduceLineLengthTime;

                if (!gameOver)
                {
                    combo += CheckCollision(lastPos, mouseToWorldPos, zz); // Check for fruit collisions
                }

                points.Enqueue(AdjustMouseToWorlPos(mouseToWorldPos));
                if (points.Count > maxNoOfSegments)
                {
                    points.Dequeue();
                }

                if (points.Count >= 10 && playSlash)
                {
                    slash.Play();
                    playSlash = false;
                }

                if (setDirection)
                {
                    //Debug.Log($"Setting Start Direction");
                    startDir = SetDirection(lastPos, mouseToWorldPos);
                    setDirection = false;
                }
                else
                {
                    currentDirection = (mouseToWorldPos - lastPos).normalized;

                    float dot = Vector3.Dot(startDir, currentDirection);
                    if (dot < 0)
                    {
                        ResetCombo();
                    }
                }

                
                lastPos = mouseToWorldPos;
            }
            else
            {
                
                if (Time.time > timer)
                {
                    ResetCombo();
                    dequeTimer -= Time.deltaTime;
                    if (points.Count > 0
                        
                        && dequeTimer <= 0)
                    {
                        points.Dequeue();
                        dequeTimer = 0.01f;
                    }
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

    int CheckCollision(Vector3 from, Vector3 to, float z)
    {
        int fruitHit = 0;
        from = new Vector3(from.x, from.y, z);
        to = new Vector3(to.x, to.y, z);

        Vector3 dir = to - from;
        Debug.DrawRay(to, dir, Color.yellow);

        RaycastHit[] hits;

        hits = Physics.RaycastAll(from, dir, dir.magnitude);
        if (hits.Length > 0)
        {
            zz = inGameZ;
            for (int i = 0; i < hits.Length; i++)
            {
                Transform hitTransform = hits[i].transform;
                Target target = hitTransform.GetComponent<Target>();
                target.isHit = true;

                if (hitTransform.gameObject.layer == 6)
                {
                    fruitHit += 1;
                }
            }
            fruitSplat.Play();

        }

        return fruitHit;
    }

    Vector3 SetDirection(Vector3 startPoint, Vector3 endPoint)
    {
        return (endPoint - startPoint).normalized;
    }

    bool CheckDirction(Vector3 currentPoint, Vector3 lastPoint)
    {
        //    Vector3 dir = (currentPoint - lastPoint).normalized;
        //    float dot = Vector3.Dot(dir, startDir);
        //    return dot > 0;
        return true;
    }

    void ResetCombo()
    {
        //Debug.Log($"Reset Combo");
        if (combo >= 3)
        {
            Vector3 spawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPoint.x = Mathf.Clamp(spawnPoint.x, minX, maxX);
            spawnPoint.y = Mathf.Clamp(spawnPoint.y, minY, maxY);
            spawnPoint.z = -2.5f;
            int comboImageIndex = combo - 3;
            comboImageIndex = Mathf.Clamp(comboImageIndex, 0, 2);
            Instantiate(comboImages[comboImageIndex], spawnPoint, Quaternion.identity);
            uIManager.UpdateScore(combo);
        }
        setDirection = true;
        combo = 0;
        playSlash = true;

    }
}

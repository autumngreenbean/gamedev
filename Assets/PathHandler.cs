using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHandler : MonoBehaviour
{
    public bool pathReady = false;
    public bool pathStarted = false;
    public Vector2[] path;
    public GameManager manager;
    private List<Vector2> tempPath = new List<Vector2>();

    public Vector2 exitPos;
    public GameObject exitObject;
    public bool useExitObject = false;
    public bool hasStartPos = false;
    public float maxStartDist = 1f;
    public Vector2 startPos;
    public GameObject startObject;
    public bool useStartObject = false;

    public float maxDist = 0.1f;
    public float snap = 0.2f;

    private Vector2 lastPos;

    LineRenderer rend;

    private void Start()
    {
        if (useExitObject)
        {
            exitPos = new Vector2(exitObject.transform.position.x, exitObject.transform.position.y);
        }
        if (hasStartPos)
        {
            if (useStartObject)
            {
                startPos = new Vector2(startObject.transform.position.x, startObject.transform.position.y);
            }
            tempPath.Add(startPos);
            manager.OnStartPosChosen(this, startPos);
        }
        lastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rend = gameObject.GetComponent<LineRenderer>();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !pathReady)
        {
            if (!hasStartPos || Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), startPos) < maxStartDist)
            {
                pathStarted = true;
            }
        }
        if (pathStarted && !pathReady)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(lastPos, pos) > maxDist)
            {
                tempPath.Add(pos);
                if (Vector2.Distance(pos, exitPos) < snap)
                {
                    pathReady = true;
                    path = tempPath.ToArray();
                    Vector3[] vecs = new Vector3[tempPath.Count];
                    int i = 0;
                    foreach (Vector2 vec in tempPath)
                    {
                        vecs[i] = new Vector3(vec.x, vec.y, -2);
                        i++;
                    }
                    rend.SetPositions(vecs);
                    manager.OnPathFinished(this);
                    pathStarted = false;
                    return;
                }
                if (tempPath.Count > 1)
                {
                    Debug.Log("renderig");
                    rend.enabled = true;
                    Vector3[] vecs = new Vector3[tempPath.Count];
                    int i = 0;
                    foreach (Vector2 vec in tempPath)
                    {
                        vecs[i] = vec;
                        i++;
                    }
                    rend.positionCount = tempPath.Count;
                    rend.SetPositions(vecs);
                }
                lastPos = pos;
            }
        }
    }
}

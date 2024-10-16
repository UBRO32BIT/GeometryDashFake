using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingObstacle : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public GameObject ways;
    public Transform[] wayPoints;
    int pointIndex;
    int pointCount;
    int direction = 1;
    Vector3 moveDirection;
    Vector3 targetPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        pointIndex = 1;
        pointCount = wayPoints.Length;
        targetPos = wayPoints[1].transform.position;
        DirectionCalculate();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
        if (transform.position == targetPos)
        {
            NextPoint();
        }
    }

    void NextPoint()
    {
        moveDirection = Vector3.zero;
        if (pointIndex == pointCount - 1)  //Arrived last pos
        {
            direction = -1;
        }
        if (pointIndex == 0) //Arrived first pos
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;

        //StartCoroutine(WaitNextPoint());
        DirectionCalculate();
    }

    void DirectionCalculate()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }
}

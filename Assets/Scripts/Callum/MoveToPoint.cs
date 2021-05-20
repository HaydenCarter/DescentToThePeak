using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour
{
    public Transform currentEndMarker;
    public Transform endMarker1 = null; // create an empty gameobject and assign in inspector
    public Transform endMarker2 = null;
    public Transform endMarker3 = null;
    public Transform endMarker4 = null;


    public float speed;
    public float newSpeed;
    private Vector3 position;
    public bool moving;
    public bool forceStop;
    public bool newMoveTrigger = false;


    private void Awake()
    {
        position = gameObject.transform.position;
        moving = false;
        currentEndMarker = endMarker1;
    }
    private void Update()
    {
        if (moving == true)
        {
            Debug.Log("Moving is true");
            transform.position = Vector3.MoveTowards(transform.position, currentEndMarker.position, speed * Time.deltaTime);
        }
        if ((Vector3.Distance(transform.position, currentEndMarker.position) == 0))
        {
            moving = false;
        }
        if (newMoveTrigger == true)
        {
            if ((Vector3.Distance(transform.position, currentEndMarker.position) == 0) && moving == false)
            {
                newMoveTrigger = false;
                MoveToNew();
            }
        }
        if (forceStop == true)
        {
            transform.position = transform.position;
        }
            
    }
    public void Move()
    {
        Debug.Log("Bruh.");
        moving = true;
        forceStop = false;
    }
    public void StopMoving()
    {
        moving = false;
        forceStop = true;
    }

    public void MoveToNew()
    {
        if (moving == false)
        {
            forceStop = false;
            if (currentEndMarker == endMarker1)
            {
                currentEndMarker = endMarker2;
                moving = true;
                newMoveTrigger = false;
            }
            else if (currentEndMarker == endMarker2)
            {
                currentEndMarker = endMarker3;
                moving = true;
                newMoveTrigger = false;
            }
            else if (currentEndMarker == endMarker3)
            {
                currentEndMarker = endMarker4;
                moving = true;
            }
            else if (currentEndMarker == endMarker4)
            {
                moving = false;
            }
        }
        else if (moving == true)
        {
            newMoveTrigger = true;
        }
    }
    public void ChangeSpeed()
    {
        speed = newSpeed;
    }
}

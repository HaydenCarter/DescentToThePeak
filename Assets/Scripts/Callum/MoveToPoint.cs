using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour
{
    public Transform endMarker = null; // create an empty gameobject and assign in inspector
    public float speed;
    private Vector2 position;
    public bool moving;

    private void Start()
    {
        position = gameObject.transform.position;
        moving = false;
    }
    private void Update()
    {
        if (moving == true) 
            {
                transform.position = Vector3.MoveTowards(transform.position, endMarker.position, speed * Time.deltaTime);
            }
    }
    public void CameraMove()
    {
        Debug.Log("Bruh.");
        moving = true;
    }
}

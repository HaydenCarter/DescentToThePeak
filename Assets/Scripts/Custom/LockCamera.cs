using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCamera : MonoBehaviour
{
    public float xclamp1 = 0f;
    public float xclamp2 = 1f;
    public float yclamp1 = 0f;
    public float yclamp2 = 1f;
    [SerializeField] private Transform targetToFollow;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, xclamp1, xclamp2),
            Mathf.Clamp(targetToFollow.position.y, yclamp1, yclamp2),
            transform.position.z);
    }
}

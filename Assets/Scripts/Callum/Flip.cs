using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    // Start is called before the first frame update
    public void FaceLeft()
    {
        transform.eulerAngles = new Vector3(0, 180, 0); // Normal
    }
    public void FaceRight()
    {
       transform.eulerAngles = new Vector3(0, 0, 0);
    }

}

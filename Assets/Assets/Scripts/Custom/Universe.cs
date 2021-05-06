using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe : MonoBehaviour
{
    private static Universe instance;
    public static Universe Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    // VARIABLES
    public bool IsOnWall = false;
    public int MaxStamina = 8;
    public int Stamina = 8;
}


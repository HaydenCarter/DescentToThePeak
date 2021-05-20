using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Allblacktrig : MonoBehaviour
{
    public Silhouette2[] _allBlack;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Silhouette2 sil in _allBlack)
        {
            sil.ColorChange();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

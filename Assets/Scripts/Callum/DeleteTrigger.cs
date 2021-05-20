using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTrigger : MonoBehaviour
{
    public GameObject currentObject;
    private void OnTriggerExit2D(Collider2D collidedObject)
    {
        if (collidedObject.CompareTag("Player"))
        {
            DeleteObject();
        }
    }
       void DeleteObject()
        {
            currentObject.SetActive(false);
        }
    }
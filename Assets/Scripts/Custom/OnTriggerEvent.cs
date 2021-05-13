using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OnTriggerEvent : MonoBehaviour
{
    [SerializeField] GameEvent _event;

    private void OnTriggerEnter2D(Collider2D collidedObject)
    {
        if (collidedObject.CompareTag("Player"))
        {
            _event?.Invoke();
        }
    }
}

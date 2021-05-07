using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    //private float climbUp = 32f;
    //private float climbDown = -32f;
    public float climbingSpeed = 32.0f;
    public float delayTime = 1f; // seconds
    public bool isClimbing = false;
    public Rigidbody2D _rb;
    public bool isSliding = false;
    [SerializeField] PlatformerWallStick _wallStickInstance;

    [SerializeField] SoFloat _wallCheckDistanceYoffset;
    [SerializeField] SoFloat _wallBackCheckDistance;
    [SerializeField] SoFloat _wallCheckDistance;

    [SerializeField] GameObject _invisibleWall;
    float direction;
    void Update()
    {
        if (!_playMovement)
        {
            if (_wallStickInstance._backCheck == true)
            {
                if (isSliding)
                {
                    _rb.gravityScale = 2;
                    _invisibleWall.SetActive(false);
                }

                if (Universe.Instance.Stamina > 0)
                {
                    isSliding = false;
                    if (Input.GetButtonDown("Vertical"))
                    {
                        _playMovement = true;
                        _invisibleWall.SetActive(true);
                        Universe.Instance.Stamina += -1;
                        direction = Input.GetAxisRaw("Vertical");
                    }
                }
                else
                {
                    isSliding = true;
                }
            }
        }
        else
        {
            StartCoroutine("stopClimb");
            //Invoke("stopClimb", delayTime);
        }
    }

    private void FixedUpdate()
    {
        if (_playMovement)
        {
            //var destination = new Vector2(transform.position.x, (transform.position.y + direction) * climbingSpeed);
            //var starting = new Vector2(transform.position.x, transform.position.y);
            //transform.position = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), destination, delayTime);
            _rb.AddRelativeForce((Vector2.up * direction) * climbingSpeed * Time.deltaTime);                    
        }
    }

    bool _playMovement = false;

    IEnumerator stopClimb()
    {
        yield return new WaitForSeconds(delayTime);
        if (_playMovement)
        {
            Debug.Log("DEST REACHED");
            _playMovement = false;
        }
        StopCoroutine("stopClimb");
    }
}

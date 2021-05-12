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
    public Animator _animator;
    bool hasSlid = false;
    [SerializeField] PlatformerWallStick _wallStickInstance;

    [SerializeField] SoFloat _gravity;
    [SerializeField] SoFloat _fallMultiplier;
    [SerializeField] SoFloat _wallCheckDistanceYoffset;
    [SerializeField] SoFloat _wallBackCheckDistance;
    [SerializeField] SoFloat _jumpHorForce;

    [SerializeField] GameObject _invisibleWall;

    public GameEvent _OnSlide;

    void JumpHorizontally()
    {
        Universe.Instance.Stamina += 3;
        _rb.AddForce(transform.right * _jumpHorForce.Value, ForceMode2D.Impulse);    
        isSliding = false;
        _wallStickInstance.enabled = true;
    }

    float direction;
    void Update()
    {
        _backCheck = Physics2D.Raycast(
          new Vector2(transform.localPosition.x, transform.localPosition.y + _wallCheckDistanceYoffset.Value),
          new Vector2(transform.localRotation.y == 0 ? -_wallBackCheckDistance.Value : _wallBackCheckDistance.Value, 0),
          Mathf.Abs(_wallBackCheckDistance.Value),
          _wallLayer);

        if (!_backCheck)
        {
            isSliding = false;
            _rb.gravityScale = _gravity.Value * _fallMultiplier.Value;
        }

        if (_backCheck)
        {
            if (!_playMovement)
            {
                if (_wallStickInstance._backCheck)
                {
                    if (isSliding)
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            JumpHorizontally();
                            return;
                        }

                        _wallStickInstance.enabled = false;


                        _rb.gravityScale = 5;

                        _invisibleWall.SetActive(false);
                    }

                    if (Universe.Instance.Stamina > 0)
                    {
                        isSliding = false;
                        hasSlid = false;
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
                        _rb.gravityScale = 5;

                        if (!hasSlid)
                        {
                            isSliding = true;
                            _OnSlide?.Invoke();
                            hasSlid = true;
                        }
                    }
                }
            }
            else
            {
                StartCoroutine("stopClimb");
            }
        }
            
    }

    public void CancelSlide()
    {
        _wallStickInstance.enabled = true;
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
    private bool _backCheck;
    public LayerMask _wallLayer;

    IEnumerator stopClimb()
    {
        yield return new WaitForSeconds(delayTime);
        if (_playMovement)
        {
            Debug.Log("DEST REACHED");
            _rb.velocity = Vector2.zero;
            _playMovement = false;
        }
        StopCoroutine("stopClimb");
    }

    #region GIZMOS
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(new Vector2(transform.localPosition.x, transform.localPosition.y + _wallCheckDistanceYoffset.Value), new Vector2(transform.localRotation.y == 0 ? -_wallBackCheckDistance.Value : _wallBackCheckDistance.Value, 0));
    }
    #endregion
}

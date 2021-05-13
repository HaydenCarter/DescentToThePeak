using System;
using UnityEngine;

public class PlatformerJump : MonoBehaviour
{
    #region VARS
    [Header("---LOCAL---", order = 0)] //Component variables
    [SerializeField] string _moveAxis;
    [SerializeField] string _jumpAxis;
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] ParticleSystem _gooVfx;
    [SerializeField] ParticleSystem _jumpExplosionGooVfx;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _groundCheckOffset;

    [Header("---SHARED---", order = 1)] //Scriptable Object Floats
    [SerializeField] SoFloat _jumpForce;
    [SerializeField] SoFloat _jumpHold;
    [SerializeField] SoFloat _gravity;
    [SerializeField] SoFloat _fallMultiplier;
    [SerializeField] SoFloat _cyoteTime;
    [SerializeField] SoFloat _groundCheckDistance;

    [Header("---EVENTS---", order = 2)] //EVENTS
    [SerializeField] GameEvent _OnGrounded;
    [SerializeField] GameEvent _OnJump;
    public GameObject staminaWheel;
    #endregion

    bool _wasJumping = false;
    bool _jumping = false;
    void InputCheck()
    {
        if (Input.GetButtonDown(_jumpAxis) && _grounded || Input.GetButtonDown(_jumpAxis) && _isOnWall)
        {
            _jumping = true;
        }
        if (Input.GetButtonUp(_jumpAxis))
        {
            _jumping = false;
        }
    }

    bool _grounded = true, _groundCheckLeft = false, _groundCheckRight = false;
    void GroundCheck()
    {
        _groundCheckLeft = SingleGroundCheck(transform.localPosition.x - _groundCheckOffset);
        _groundCheckRight = SingleGroundCheck(transform.localPosition.x + _groundCheckOffset);
        if (_groundCheckLeft || _groundCheckRight)
        {
            if(_wasJumping && _groundCheckDistance.Value > -1)
            {
                _jumpExplosionGooVfx.Play();
                _wasJumping = false;
            }

            SetGrounded(true);
            SetOnWall(false);
            Universe.Instance.Stamina = Universe.Instance.MaxStamina;
            staminaWheel.SetActive(false);
            _OnGrounded.Invoke();
        }
        if (!_groundCheckLeft && !_groundCheckRight)
        {
            Invoke("CyoteTime", _cyoteTime.Value);
        }
    }

    bool _isOnWall = false;
    public void SetOnWall(bool isOnWall)
    {
        _isOnWall = isOnWall;
        _animator.SetBool("WallStick", _isOnWall); // land anim
    }

    public void SetGrounded(bool isGrounded)
    {       
        if (_isOnWall)
        {
            _animator.SetBool("Grounded", false);
            _grounded = false;
        }
        else
        {
            _animator.SetBool("Grounded", _grounded); // land anim
            _grounded = isGrounded;
        }

        if (isGrounded)
            _wasSliding = false;
    }

    bool SingleGroundCheck(float xPos)
    {
        return Physics2D.Raycast(
          new Vector2(xPos, transform.localPosition.y),
          new Vector2(0, _groundCheckDistance.Value),
          Mathf.Abs(_groundCheckDistance.Value),
          _groundLayer);
    }

    bool _wasSliding = false;

    public void WasSliding(bool wasSliding)
    {
        _wasSliding = wasSliding;
    }

    public Climbing _climbing;
    void ChangeGravity()
    {
        if (_groundCheckLeft || _groundCheckRight)
            _rb.gravityScale = 0;
        else if (!_groundCheckLeft && !_groundCheckRight && !_isOnWall)
            _rb.gravityScale = _gravity.Value * _fallMultiplier.Value;
    }

    #region UNITY
    void FixedUpdate()
    {
        if (_grounded)
        {
            _gooVfx.Play();
        }
        else
        {
            _gooVfx.Pause();
        }

        if (!_jumping) return;
        _gooVfx.Pause();
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        Jump();
        Invoke("JumpHeightController", _jumpHold.Value);
    }

    void Update()
    {
        InputCheck();
        GroundCheck();
        ChangeGravity();
        FallingCheck();
    }
    #endregion

    #region HELPERS
    public void Jump() { _rb.AddForce(Vector2.up * _jumpForce.Value, ForceMode2D.Impulse); SetGrounded(false); _rb.gravityScale = _gravity.Value * _fallMultiplier.Value; _OnJump.Invoke(); }
    void JumpHeightController() { if (_jumping) _jumping = false; }
    void FallingCheck() => _animator.SetFloat("VelocityY", _rb.velocity.y);
    void CyoteTime() { _grounded = false; _animator.SetBool("Grounded", false); _wasJumping = true; } //jump anim
    #endregion

    #region GIZMOS
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(new Vector2(transform.localPosition.x - _groundCheckOffset, transform.localPosition.y), new Vector2(0, _groundCheckDistance.Value));
        Gizmos.DrawRay(new Vector2(transform.localPosition.x + _groundCheckOffset, transform.localPosition.y), new Vector2(0, _groundCheckDistance.Value));
    }
    #endregion
}
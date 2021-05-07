using System;
using UnityEngine;

public class PlatformerWallStick : MonoBehaviour
{
    #region VARS
    [Header("---LOCAL---", order = 0)] //Component variables
    [SerializeField] string _moveAxis;
    [SerializeField] string _jumpAxis;
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] LayerMask _wallLayer;
    [SerializeField] ParticleSystem _gooVfx;
    [SerializeField] ParticleSystem _wallSplatVfx;
    [SerializeField] ParticleSystem _wallSplatExplosionVfx;
    [SerializeField] float _originalGroundCheckDistance; 
    [SerializeField] float _groundCheckDistanceOffset;

    [Header("---SHARED---", order = 1)] //Scriptable Object Floats
    [SerializeField] SoFloat _gravity;
    [SerializeField] SoFloat _jumpHorForce;
    [SerializeField] SoFloat _wallCheckDistanceYoffset;
    [SerializeField] SoFloat _wallCheckDistance;
    [SerializeField] SoFloat _wallBackCheckDistance;
    [SerializeField] SoFloat _groundCheckDistance;

    [Header("---EVENTS---", order = 2)] //EVENTS
    [SerializeField] GameEvent _OnWall;
    [SerializeField] GameEvent _OnOffWall;
    #endregion

    RaycastHit2D _wallHit;
    public bool _wallCheck = false;
    public bool _backCheck = false;
    bool _jumping = false;
    void WallCheck()
    {
        _wallCheck = Physics2D.Raycast(
          new Vector2(transform.localPosition.x, transform.localPosition.y + _wallCheckDistanceYoffset.Value),
          new Vector2(transform.localRotation.y == 0 ? _wallCheckDistance.Value : -_wallCheckDistance.Value, 0),
          Mathf.Abs(_wallCheckDistance.Value),
          _wallLayer);

        _backCheck = Physics2D.Raycast(
          new Vector2(transform.localPosition.x, transform.localPosition.y + _wallCheckDistanceYoffset.Value),
          new Vector2(transform.localRotation.y == 0 ? -_wallBackCheckDistance.Value : _wallBackCheckDistance.Value, 0),
          Mathf.Abs(_wallBackCheckDistance.Value),
          _wallLayer);
    }

    void JumpHorizontally() => _rb.AddForce(transform.right * _jumpHorForce.Value, ForceMode2D.Impulse);

    #region UNITY
    private void Awake()
    {
        _climbing = GetComponent<Climbing>();
    }
    void Start() => _wallSplatVfx.Pause();

    bool _jumpHor = false;
    void FixedUpdate()
    {
        if(_jumpHor)
        {
            Universe.Instance.Stamina += 4;
            _OnOffWall?.Invoke();
            JumpHorizontally();
            _jumpHor = false;
        }
    }

    bool _grounded = false;
    public void IsGrounded(bool val) => _grounded = val;
    bool _wasOnWall = false;
    Climbing _climbing;
    [SerializeField] GameObject _invisibleWall;
    void Update()
    {
        if (_grounded) return;

        WallCheck();

        if (_wallCheck)
        {          
            _wallSplatExplosionVfx.Play();
            _gooVfx.Pause();
            _rb.gameObject.transform.eulerAngles = new Vector3(0, _rb.gameObject.transform.eulerAngles.y == 0 ? 180 : 0, 0);
            _OnWall.Invoke();
        }

        if (!_backCheck)
        {
            _wallSplatVfx.Pause();
        }
        else
        {
            _wallSplatVfx.Play();

            if (_climbing.isSliding) return;
            _rb.velocity = Vector2.zero;
            _rb.gravityScale = 0;
            
            _gooVfx.Pause();
            _OnWall.Invoke();

            if (Input.GetButtonDown(_jumpAxis))
            {
                _jumpHor = true;
            }
        }

        if (!_wallCheck && !_backCheck)
        {
            _invisibleWall.SetActive(false);
            _animator.SetBool("WallStick", false);            
            if (_rb.velocity.x < -0.1f)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else if (_rb.velocity.x > 0.1f)
                transform.rotation = Quaternion.Euler(0, 0, 0);

            if (_wasOnWall)
            {
                _OnOffWall?.Invoke();
                _wasOnWall = false;
            }
        }
        else
        {
            _wasOnWall = true;
            _animator.SetBool("WallStick", true);
        }
    }
    #endregion

    #region HELPERS
    #endregion

    #region GIZMOS
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(new Vector2(transform.localPosition.x, transform.localPosition.y + _wallCheckDistanceYoffset.Value), new Vector2(transform.localRotation.y == 0 ? _wallCheckDistance.Value : -_wallCheckDistance.Value, 0));
        Gizmos.DrawRay(new Vector2(transform.localPosition.x, transform.localPosition.y + _wallCheckDistanceYoffset.Value), new Vector2(transform.localRotation.y == 0 ? -_wallBackCheckDistance.Value : _wallBackCheckDistance.Value, 0));
    }
    #endregion
}
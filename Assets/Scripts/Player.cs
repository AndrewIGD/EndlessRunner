using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpHeight;
    [SerializeField] float jumpBoostHeight;
    [SerializeField] float sideJumpWidth, sideJumpHeight;
    [SerializeField] float gravity;
    [SerializeField] GameObject fireVfx;
    [SerializeField] AudioSource jump;
    [SerializeField] AudioSource fall;

    private PlayerControls controls;

    private const float AnimatorSpeedMultiplier = 1f;

    private Animator _animator;

    private Rigidbody _rb;

    private bool _isGrounded = true;

    private bool _canSideFlip = false;

    private bool _jumpBoost = false;

    private bool _moving = false;

    private bool IsRunning
    {
        get
        {
            return _isGrounded && _animator.GetCurrentAnimatorStateInfo(0).IsName("run") && _animator.GetBool("Jump") == false && _animator.GetBool("Left") == false && _animator.GetBool("Right") == false;
        }
    }

    private bool Jumping
    {
        get
        {
            return _animator.GetBool("Jump") || _animator.GetBool("SideFlip");
        }
    }

    public void Bump()
    {
        if (_moving == false)
            return;

        _moving = false;

        EndGame();

        fall.Play();
    }

    public void Burn()
    {
        Instantiate(fireVfx, transform.position, Quaternion.identity);

        EndGame();

        Destroy(gameObject);
    }

    public void StartMoving()
    {
        _moving = true;

        _animator.SetTrigger("StartRun");
    }

    public void Grounded()
    {
        _isGrounded = true;
    }

    public void Falling()
    {
        _isGrounded = false;
    }

    public void Sideflip()
    {
        _canSideFlip = true;
    }

    public void StopSideflip()
    {
        _canSideFlip = false;
    }

    public void JumpBoost()
    {
        _jumpBoost = true;
    }

    public void CancelJumpBoost()
    {
        _jumpBoost = false;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _rb = GetComponent<Rigidbody>();

        controls = new PlayerControls();

        controls.Movement.Enable();

        Physics.gravity = new Vector3(0, gravity, 0);
    }

    private void Update()
    {
        if (_moving == false)
            return;

        if (controls.Movement.Jump.ReadValue<float>() > 0)
        {
            Jump();
        }
        else
        {
            CancelJump();
        }

        SideMovement();

        CheckGround();

        ForwardMovement();
    }

    private void CheckGround()
    {
        _animator.SetBool("Falling", !_isGrounded);
    }

    private void ForwardMovement()
    {
        transform.position += transform.forward * Time.deltaTime * GameManager.instance.levelData.platformSpeed;
    }

    private void SideMovement()
    {
        _animator.SetBool("Left", controls.Movement.Right.ReadValue<float>() > 0);
        _animator.SetBool("Right", controls.Movement.Left.ReadValue<float>() > 0);
    }

    private void CancelJump()
    {
        _animator.SetBool("Jump", false);
        _animator.SetBool("SideFlip", false);
    }

    private void Jump()
    {
        if (!Jumping)
        {
            if (_canSideFlip == false)
                _animator.SetBool("Jump", true);
            else _animator.SetBool("SideFlip", true);
        }
    }

    private static void EndGame()
    {
        CameraFollow.instance.Stop();

        GameManager.instance.End();
    }

    //Animation Events
    private void AddJumpVelocity()
    {
        if (_isGrounded == false)
            return;

        _rb.velocity = new Vector3(0, _jumpBoost ? jumpBoostHeight : jumpHeight);

        _animator.SetBool("Jump", false);
        _animator.SetBool("SideFlip", false);

        jump.Play();
    }

    private void AddLeftVelocity()
    {
        if (_isGrounded == false)
            return;

        _rb.velocity = new Vector3(-sideJumpWidth, sideJumpHeight);

        _animator.SetBool("Right", false);

        jump.Play();
    }

    private void AddRightVelocity()
    {
        if (_isGrounded == false)
            return;

        _rb.velocity = new Vector3(sideJumpWidth, sideJumpHeight);

        _animator.SetBool("Left", false);

        jump.Play();
    }
}

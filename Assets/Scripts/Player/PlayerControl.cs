using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Attached Script")]
    private Rigidbody _rigidbody;
    private Animator _animator;
    private PlayerStats _playerStats;
    private PlayerItemList _playerItemList;

    [Header("Speed")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float spinSpeed = 1;

    [Header("Stamina")] 
    [SerializeField] private float maxStamina = 100;
    [SerializeField] private float staminaRegeneration = 0.1f;
    [SerializeField] private float staminaBoost = 10;
    [SerializeField] private float staminaRunCost = 2;
    [SerializeField] private float staminaJumpCost = 30;
    private float _currentStamina;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float gravityForce = 5;
    [SerializeField] private float distantToGround = 0.1f;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask ground ;

    [Header("Camera")]
    [SerializeField] private Camera followCamera;
    [SerializeField] private Vector3 maxOffsetCamera ;
    private Vector3 _cameraPos;

    [Header("Animation")] [SerializeField] private float idleAndRunningTransition;
    private bool _isRunning;
    private bool _isJumping;
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _playerStats = GetComponent<PlayerStats>();
        _playerItemList = GetComponent<PlayerItemList>();

        _currentStamina = maxStamina;
        _cameraPos = followCamera.transform.position - transform.position;
    }

    private void Update()
    {
        ChoosingItem();
        UsingItem();
    }

    void FixedUpdate()
    {
        Movement();
        StaminaRegen();
        AnimationUpdate();    
    }
    
    private void LateUpdate()
    {
        var position = _rigidbody.position;
        var positionCam = followCamera.transform.position;
        float offsetX =  Mathf.Max( Mathf.Min( positionCam.x - position.x, maxOffsetCamera.x), -maxOffsetCamera.x );
        float offsetY =  Mathf.Max( Mathf.Min( positionCam.y - position.y, maxOffsetCamera.y), -maxOffsetCamera.y );
        float offsetZ =  Mathf.Max( Mathf.Min( positionCam.z - position.z, maxOffsetCamera.z), -maxOffsetCamera.z );
        
        followCamera.transform.position = new Vector3(position.x + _cameraPos.x + offsetX , position.y + _cameraPos.y + offsetY, position.z + _cameraPos.z + offsetZ);
        
    }

    private void Movement()
    {
        Vector3 velocity = Vector3.zero;
        
         
        if (Input.GetKey(KeyCode.A))
        {
            velocity += Vector3.left;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            velocity += Vector3.right;
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            velocity += Vector3.forward;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            velocity += Vector3.back;
        }

        if (Input.GetKey(KeyCode.LeftShift) && _currentStamina >= staminaRunCost)
        {
            velocity *= staminaBoost;
            _currentStamina -= staminaRunCost;
        }
        
        var rigidbodyVelocity = _rigidbody.velocity;
        _rigidbody.velocity = new Vector3(velocity.x * movementSpeed,  rigidbodyVelocity.y - gravityForce * Time.fixedDeltaTime, velocity.z* movementSpeed);
        
        _isJumping = !IsGround();
        _isRunning = velocity.normalized.magnitude > idleAndRunningTransition;
        
        if (Input.GetKey(KeyCode.Space) && ! _isJumping && _currentStamina >= staminaJumpCost)
        {
            _rigidbody.velocity = new Vector3(rigidbodyVelocity.x , jumpForce, rigidbodyVelocity.z);
            _currentStamina -= staminaJumpCost;
        }

        // Rotation of the character
        if (_isRunning)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime * spinSpeed);

            _rigidbody.MoveRotation(targetRotation);
        }

    }

    void StaminaRegen()
    {
        if(!_isJumping && !_isRunning) _currentStamina = Mathf.Min(_currentStamina + staminaRegeneration, maxStamina);
        UIManager.Instance.UpdateStamina((int) Mathf.Floor(_currentStamina), maxStamina);
    }

    private void ChoosingItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _playerItemList.OnChoosingButton(0);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playerItemList.OnChoosingButton(1);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _playerItemList.OnChoosingButton(2);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _playerItemList.OnChoosingButton(3);
        }
        
    }

    private void UsingItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast (ray, out var hit)) 
            {
                //draw invisible ray cast/vector
                //Debug.DrawLine(ray.origin, hit.point);
                //log hit area to the console
                Debug.Log(hit.point.ToString() + " " + hit.transform.gameObject.name);
                //_playerItemList.OnUsingItem();
            }    
            
        }
    }
    
    private void AnimationUpdate()
    {
        _animator.SetBool(IsJumping, _isJumping);
        _animator.SetBool(IsRunning, _isRunning);
    }

    private bool IsGround()
    {
        return Physics.CheckSphere(transform.position, distantToGround, ground );
    }

    
}

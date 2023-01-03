using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody _rigidbody;
    private Animator _animator; 

    [SerializeField] private float movementSpeed;
    [SerializeField] private float spinSpeed = 1;
    
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
        
        _cameraPos = followCamera.transform.position - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
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

        var rigidbodyVelocity = _rigidbody.velocity;
        _rigidbody.velocity = new Vector3(velocity.x * movementSpeed,  rigidbodyVelocity.y - gravityForce * Time.fixedDeltaTime, velocity.z* movementSpeed);
        
        _isJumping = !IsGround();
        _isRunning = velocity.normalized.magnitude > idleAndRunningTransition;
        
        if (Input.GetKey(KeyCode.Space) && ! _isJumping)
        {
            _rigidbody.velocity = new Vector3(rigidbodyVelocity.x , jumpForce, rigidbodyVelocity.z);
        }

        // Rotation of the character
        if (_isRunning)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime * spinSpeed);

            _rigidbody.MoveRotation(targetRotation);
        }


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

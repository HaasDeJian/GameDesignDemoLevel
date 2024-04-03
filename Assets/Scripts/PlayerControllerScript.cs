using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private GameObject camHolder;

    private Vector2 _look;
    private Vector3 _playerVelocity;
    private float _movementZ, _movementX, _lookRotation;
    private bool _grounded;
    private const float GravityValue = -9.81f;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateIsGrounded();

        transform.Rotate(new Vector3(0, (_look.x * 100 )* rotationSpeed * Time.deltaTime, 0));
        MovePlayer(_movementZ,false);
    }
    
    private void LateUpdate()
    {
        Look();
    }
    
    private void MovePlayer(float moveDirection, bool jump)
    {
        if (_grounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }
        
        var movement = transform.TransformDirection(new Vector3(_movementX, 0f, moveDirection));
        
        characterController.Move(movement * (Time.deltaTime * playerSpeed));

        
        if (_grounded && jump)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * GravityValue);
        }

        _playerVelocity.y += GravityValue * Time.deltaTime;

        characterController.Move(_playerVelocity * Time.deltaTime);
    }
    
    private void Look()
    {
        transform.Rotate(Vector3.up * (_look.x * rotationSpeed));
        _lookRotation += (-_look.y * rotationSpeed);
        _lookRotation = Mathf.Clamp(_lookRotation, -90, 90);
        var eulerAngles = camHolder.transform.eulerAngles;
        eulerAngles = new Vector3(_lookRotation, eulerAngles.y, eulerAngles.z);
        camHolder.transform.eulerAngles = eulerAngles;
    }
    
    private void OnJump()
    {
        MovePlayer(transform.position.y,true);
    }
    
    private void UpdateIsGrounded()
    {
        _grounded = groundCheck.IsGrounded();    
    }

    
    private void OnMove(InputValue movementValue)
    {
        var movementVector = movementValue.Get<Vector2>();
        
        _movementZ = movementVector.y;
        _movementX = movementVector.x;
    }
    
    private void OnLook(InputValue lookValue)
    {
        _look = lookValue.Get<Vector2>();
    }
}

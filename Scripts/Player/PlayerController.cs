using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private PhysicalCheck physicalCheck;
    private Rigidbody2D rb;
    public Vector2 inputDirection;

    [Header("Basic Parameters")]
    public float velocity;
    public float jumpForce;
    public bool forceWalk;

    // When it's created, it will call Awake() first, then is OnEnable(), finally is Start()

    private void Awake()
    {
        physicalCheck = GetComponent<PhysicalCheck>();
        rb = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInputControl();
        inputControl.GamePlay.Jump.started += Jump;
        inputControl.GamePlay.ForceControl.started += ForceWalkStart;
        inputControl.GamePlay.ForceControl.canceled += ForceWalkEnd;
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>(); /*new Vector2();*/
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = new Vector2(forceWalk ? 2.4f : inputDirection.x * velocity * Time.deltaTime, rb.velocity.y);

        // Flip Player
        if (inputDirection.x == 0.0f)
            return;
        transform.localScale = new Vector3((inputDirection.x < 0.0f ? -1 : 1) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (physicalCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void ForceWalkStart(InputAction.CallbackContext obj)
    {
        forceWalk = true;
    }

    private void ForceWalkEnd(InputAction.CallbackContext context)
    {
        forceWalk = false;
    }
}

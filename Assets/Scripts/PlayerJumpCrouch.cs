using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpCrouch : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float crouchScale = 0.5f;
    public float normalScale = 1f;
    public float gravity = -9.81f;

    private Rigidbody rb;
    private bool isGrounded;
    private Transform playerTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = transform;
    }

    void Update()
    {
        // Movement
        // float moveX = Input.GetAxis("Horizontal");
        // float moveZ = Input.GetAxis("Vertical");

        // Vector3 move = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;
        // transform.Translate(move, Space.Self);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerTransform.localScale = new Vector3(1, crouchScale, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            playerTransform.localScale = new Vector3(1, normalScale, 1);
        }
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        // Apply gravity manually
        if (!isGrounded)
        {
            rb.AddForce(Vector3.up * gravity);
        }
    }
}

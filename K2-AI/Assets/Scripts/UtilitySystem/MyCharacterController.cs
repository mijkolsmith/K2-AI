using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCharacterController : MonoBehaviour
{
    private CharacterController player;

    [SerializeField] float speed = 5.0f;
	[SerializeField] float gravity = -20f;

	[SerializeField] Transform groundCheck;
	[SerializeField] float groundDistance = .5f;
	[SerializeField] LayerMask groundMask;
    private bool isGrounded;

	[SerializeField] float jumpHeight = 2.0f;
    
    Vector3 velocity;
    Vector3 move = Vector3.zero;

	[SerializeField] float health = 100f;

	void Start()
    {
        //hide the mouse
        Cursor.lockState = CursorLockMode.Locked;
        player = GetComponent<CharacterController>();
        groundCheck = transform.GetChild(1).GetComponent<Transform>();
    }

    void Update()
    {
        //ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //reset gravity if grounded & make player accelerate to a max when falling
        if (isGrounded && velocity.y < -9.81f )
        {
            velocity.y = -9.81f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButton("Fire2") == false)
        {
            //movement
            move = transform.right * x + transform.forward * z;
			move.Normalize();
            player.Move(move * speed * Time.deltaTime);

            //gravity
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            player.Move(velocity * Time.deltaTime);
        }
        else if (Input.GetButton("Fire2") == true && isGrounded)
        {
            Debug.Log("Crouching");
            //movement
            move = transform.right * x + transform.forward * z;

            player.Move(move * speed * Time.deltaTime);

            //gravity
            player.Move(velocity * Time.deltaTime);
        }

        //toggle the mouse hide with escape
        if (Input.GetKeyDown("escape"))
        {
            if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
            else if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
        }
    }

    public void CheckIsGrounded()
    {
        RaycastHit2D[] hits;

        //We raycast down 1 pixel from this position to check for a collider
        Vector2 positionToCheck = transform.position;
        hits = Physics2D.RaycastAll(positionToCheck, new Vector2(0, -1), 0.01f);

        //if a collider was hit, we are grounded
        if (hits.Length > 0)
        {
            isGrounded = true;
        }
    }
}

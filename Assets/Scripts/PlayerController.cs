using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float maxSpeed;
	[SerializeField]
	private float movePower;
	[SerializeField]
	private float jumpPower;

	[SerializeField]
	private LayerMask groundMask;

	private new Rigidbody2D rigidbody;
	private Animator animator;
	private new SpriteRenderer renderer;
	private Vector2 inputDir;
	private bool isGrounded;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		renderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		Move();
	}

	private void FixedUpdate()
	{
		GroundCheck();
	}

	private void OnMove(InputValue value)
	{
		inputDir = value.Get<Vector2>();
		animator.SetFloat("MoveDirX", Mathf.Abs(inputDir.x));
		if (inputDir.x > 0)
			renderer.flipX = false;
		else if (inputDir.x < 0)
			renderer.flipX = true;
	}

	private void OnJump(InputValue value)
	{
		if (value.isPressed && isGrounded)
			Jump();
	}

	private void Move()
	{
		if (inputDir.x < 0 && rigidbody.velocity.x > -maxSpeed)
			rigidbody.AddForce(Vector2.right * inputDir.x * movePower);
		else if (inputDir.x > 0 && rigidbody.velocity.x < maxSpeed)
			rigidbody.AddForce(Vector2.right * inputDir.x * movePower);
	}

	private void Jump()
	{
		rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
	}

	private void GroundCheck()
	{
		Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.red);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundMask);
		if (hit.collider != null)
		{
			isGrounded = true;
			animator.SetBool("IsGrounded", true);

			// Smooth landing
			if (rigidbody.velocity.y < -3)
			{
				rigidbody.velocity = new Vector2(rigidbody.velocity.x, -3);
			}
		}
		else
		{
			isGrounded = false;
			animator.SetBool("IsGrounded", false);
		}
	}
}

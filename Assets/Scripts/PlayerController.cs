using System.Collections;
using System.Collections.Generic;
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

	private new Rigidbody2D rigidbody;
	private Animator animator;
	private new SpriteRenderer renderer;
	private Vector2 inputDir;

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
		if (value.isPressed)
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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		animator.SetBool("IsGrounded", false);
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		animator.SetBool("IsGrounded", true);
	}
}

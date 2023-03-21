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
	private Vector2 inputDir;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		Move();
	}

	private void OnMove(InputValue value)
	{
		inputDir = value.Get<Vector2>();
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
}

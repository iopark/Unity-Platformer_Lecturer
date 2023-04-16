using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Mouse : MonoBehaviour
{
	private new Rigidbody2D rigidbody;
	private Animator animator;
	private new Collider2D collider;

	private bool isDie = false;

	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private Transform groundCheckPosition;
	[SerializeField]
	private LayerMask groundLayer;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		collider = GetComponent<Collider2D>();
	}

	private void Update()
	{
		if (!isDie)
		{
			Move();
			if (!IsGroundExist())
				Turn();
		}
	}

	private void Move()
	{
		animator.SetBool("Move", true);
		rigidbody.velocity = new Vector2(transform.right.x * -1 * moveSpeed, rigidbody.velocity.y);
	}

	private void Turn()
	{
		transform.Rotate(Vector3.up, 180);	
	}

	private void Die()
	{
		isDie = true;
		rigidbody.velocity = Vector2.up * 3;
		animator.SetBool("Die", true);
		collider.enabled = false;

		Destroy(gameObject, 3f);
	}

	private bool IsGroundExist()
	{
		Debug.DrawRay(groundCheckPosition.position, Vector2.down, Color.red);
		return Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 1f, groundLayer);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			// TODO : 임시로 구현
			Die();
		}
	}
}

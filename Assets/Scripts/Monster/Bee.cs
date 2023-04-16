using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Bee : Monster
{
	public enum State { Idle, Trace, Returning, Die}

	private State state = State.Idle;
	private Transform target;
	private Vector3 returnPosition;

	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float range;

	private void Start()
	{
		rigidbody.gravityScale = 0f;
		target = GameObject.FindGameObjectWithTag("Player").transform;
		returnPosition = transform.position;
	}

	private void Update()
	{
		switch (state)
		{
			case State.Idle:
				IdleUpdate();
				break;
			case State.Trace:
				TraceUpdate();
				break;
			case State.Returning:
				ReturningUpdate();
				break;
			case State.Die:
				DieUpdate();
				break;
		}
	}

	public void ChangeState(State state)
	{
		this.state = state;
	}

	private void IdleUpdate()
	{
		rigidbody.velocity = Vector3.zero;

		if (Vector2.Distance(target.position, transform.position) < range)
		{
			ChangeState(State.Trace);
		}
	}

	private void TraceUpdate()
	{
		Vector2 dir = (target.position - transform.position).normalized;
		rigidbody.velocity = dir * moveSpeed;
		renderer.flipX = rigidbody.velocity.x > 0 ? true : false;

		if (Vector2.Distance(target.position, transform.position) > range)
		{
			ChangeState(State.Returning);
		}
	}

	private void ReturningUpdate()
	{
		Vector2 dir = (returnPosition - transform.position).normalized;
		rigidbody.velocity = dir * moveSpeed;
		renderer.flipX = rigidbody.velocity.x > 0 ? true : false;

		if (Vector2.Distance(returnPosition, transform.position) <= 0.1f)
		{
			ChangeState(State.Idle);
		}
	}

	private void DieUpdate()
	{
		// Do nothing
	}

	protected override void Die()
	{
		base.Die();

		ChangeState(State.Die);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
	Animator anim;
    public float  speed=5f;
	bool isRun = false;
	bool isRight = true;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		Move();
		Turn();
	}

	#region Character Movement
	void Move()
	{
		if (Input.GetKey(KeyCode.D))
		{
			isRight = true;
			transform.Translate(Vector2.right*speed*Time.deltaTime);
			isRun = true;
        }
        else if (Input.GetKey(KeyCode.A))
		{
			isRight = false;
			transform.Translate(Vector2.right *speed* Time.deltaTime);
			isRun = true;
		}
		anim.SetBool("isRunning",isRun);
		isRun = false;
	}
	#endregion

	#region Character Turn
	void Turn()
	{
		if (isRight)
		{
			transform.rotation = Quaternion.Euler(Vector2.up * 0);
		}
		if (!isRight)
		{
			transform.rotation = Quaternion.Euler(Vector2.down * -180);
		}
	}
	#endregion
}

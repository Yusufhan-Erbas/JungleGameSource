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
	private void Start()
	{
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		Move();
	}
	#region Character Movement
	void Move()
	{
		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(Vector2.right*speed*Time.deltaTime);
			isRun = true;
        }
        else if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(Vector2.left *speed* Time.deltaTime);
			isRun = true;
		}
		anim.SetBool("isRunning",isRun);
		isRun = false;
	}
	#endregion

	#region Character Turn
	void Turn()
	{
		
	}
	#endregion
}

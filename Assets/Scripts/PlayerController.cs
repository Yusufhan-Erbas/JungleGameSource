using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	Rigidbody2D rb;
	Animator anim;
	public float speed = 5f;
	bool isRun = false;
	bool isRight = true;
	bool isDead = false;
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
			transform.Translate(Vector2.right * speed * Time.deltaTime);
			isRun = true;
		}
		else if (Input.GetKey(KeyCode.A))
		{
			isRight = false;
			transform.Translate(Vector2.right * speed * Time.deltaTime);
			isRun = true;
		}
		anim.SetBool("isRunning", isRun);
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

	#region Character Death
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Death(collision.gameObject);
	}

	void Death(GameObject player)
	{
		if (player.CompareTag("Death"))
		{
			Debug.Log(player);
			isDead = true;
			anim.SetBool("isDead", isDead);
			rb.velocity = new Vector2(0f,6f);
			Destroy(gameObject, 0.7f);
		}
		else if (player.CompareTag("EnemyBody"))
		{
			isDead = true;
			anim.SetBool("isDead", isDead);
			if (isRight)
			{
				rb.velocity = new Vector2(-4f, 0f);
			}
			else
			{
				rb.velocity = new Vector2(4f, 0f);
			}
			Destroy(gameObject, 0.7f);
		}
		else
		{
			isDead = false;
		}
	}
	#endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	[SerializeField]
	GameObject rockHead,enemyHead;
	Rigidbody2D rb;
	
	Animator anim;
	

	
	bool isEnemyDead = false;
	private void Awake()
	{
		rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
		anim = GetComponentInParent<Animator>();
	}

	
	#region Enemy Death
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Death(collision.gameObject);
	}

	void Death(GameObject enemy)
	{
		if (enemy.CompareTag("Player"))
		{
			rb.velocity = new Vector2(rb.velocity.x,5f);
			isEnemyDead = true;
			anim.SetBool("enemyDead", isEnemyDead);
			Destroy(rockHead,0.8f);
		}
		else
		{
			isEnemyDead = false;
		}
	}
	#endregion

	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField]
	GameObject startPoint, endPoint;
	Transform currentPoint;

	Rigidbody2D enemyRb;

	int walk = 3;

	private void Start()
	{
		enemyRb = GetComponent<Rigidbody2D>();
		currentPoint = endPoint.transform;
	}

	private void Update()
	{
		EnemyPatrol();
	}

	#region Enemy Patrolling
	void EnemyPatrol()
	{
		Vector2 distance = currentPoint.position - transform.position;
		if (currentPoint == endPoint.transform)
		{
			enemyRb.velocity = new Vector2(walk, 0);
		}
		else
		{
			enemyRb.velocity = new Vector2(-walk, 0);
		}

		if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == endPoint.transform)
		{
			currentPoint = startPoint.transform;
		}
		if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == startPoint.transform)
		{
			currentPoint = endPoint.transform;
		}
	}
	#endregion
}

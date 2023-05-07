using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGround : MonoBehaviour
{
	[SerializeField]
	LayerMask ground;
	Rigidbody2D rb;
	float jumpAmount = 6f;
	bool isGround=true;
	AudioSource jumpSound;
	private void Awake()
	{
		jumpSound = GetComponent<AudioSource>();
		rb = GetComponentInParent<Rigidbody2D>();
	}

	private void Update()
	{
		RaycastHit2D isWeGround = Physics2D.Raycast(transform.position, Vector2.down, 0.25f, ground);

		if (isWeGround.collider != null)
		{
			isGround = true;
		}
		else
		{
			isGround = false;
		}

		if(isGround==true && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))) 
		{
			jumpSound.Play();
			rb.velocity =new Vector2(rb.velocity.x,jumpAmount);
		}
	}
}

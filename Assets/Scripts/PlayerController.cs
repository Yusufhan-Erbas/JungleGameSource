using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	Rigidbody2D rb;
	Animator anim,flagAnim;
	[SerializeField]
	Text scoreText;
	[SerializeField]
	GameObject[] enemyHeads;
	GameObject enemyHead;

	AudioSource levelClear, gameManager;
	public float speed = 5f;
	bool isRun = false;
	bool isRight = true;
	bool isDead = false;
	
	int score = 0;

	private void Awake()
	{
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<AudioSource>();
		levelClear = GetComponent<AudioSource>();
		flagAnim = GameObject.FindWithTag("Flag").GetComponent<Animator>();
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}
	private void Start()
	{
		scoreText.text = "X "+score;
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

	#region Fruit Taken Process
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Fruit"))
		{
			score += 5;
			Destroy(collision.gameObject,0.6f);
			scoreText.text = "X " + score;
		}
		else if (collision.CompareTag("Flag"))
		{
			StartCoroutine(NewScene());
			if (levelClear.isPlaying)
			{
				return;
			}
			gameManager.mute = true;
			levelClear.Play();
			
			
		}

		for (int i = 0; i < enemyHeads.Length; i++)
		{
			enemyHead = enemyHeads[i];
			Debug.Log(enemyHead.tag);
			if (collision.CompareTag(enemyHead.tag.ToString()))
			{
				score += 10;
				scoreText.text = "X " + score;
			}
		}
		
		
	}

	IEnumerator NewScene()
	{
		flagAnim.SetTrigger("isEnd");
		yield return new WaitForSeconds(3f);
		flagAnim.SetBool("isFinish",true);
		yield return new WaitForSeconds(6f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

	}
	#endregion
}

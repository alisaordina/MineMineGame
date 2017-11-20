using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfController : MonoBehaviour 
{

	[SerializeField] private float force= 1f;

	[SerializeField] private Transform spawnPoint;

	[SerializeField] private float jumpForce = 30f;


	[SerializeField] GameObject fireBall; 



	public int directionDwarf = 1;
	private Animator _animator;

	private Rigidbody2D _rigidBody;

	private bool _dead = false;

	private GameObject ShoteOut = null;

	// Use this for initialization
	void Start () 
	{

		_rigidBody = gameObject.GetComponent<Rigidbody2D> ();

		_animator = gameObject.GetComponent<Animator> ();

		gameObject.transform.position = spawnPoint.position;

	}

	// Update is called once per frame
	void FixedUpdate () 
	{

		if (!_dead) 
		{

			//float jump = Input.GetAxis ("Jump");

			float crawl = Input.GetAxis ("Vertical");

			float fire = Input.GetAxis ("Fire1");

			Vector2 forceVector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

			//if (jump > 0 && isGrounded()) 
			if(Input.GetKeyDown("w") && isGrounded() )
			{
				
					_rigidBody.AddForce (Vector2.up * jumpForce);


					
			}


			if (crawl < 0) {
				_animator.SetBool ("Crawl", true);
			} else {
				_animator.SetBool ("Crawl", false);
			}

			if (fire >0) 
			{
				
				_animator.SetBool ("Fire", true);


				Vector2 positionOfDwarf = new Vector2 (this.gameObject.transform.position.x, this.gameObject.transform.position.y);
				//Instantiate (fireBall, positionOfDwarf, Quaternion.identity);
				if(ShoteOut == null)
				{
					ShoteOut = Instantiate (fireBall, positionOfDwarf, Quaternion.identity);

				//Instantiate (fire, positionOfDwarf, Quaternion.identity);
			} 
			}

			else
			{
				_animator.SetBool ("Fire", false);
			}
			

			_rigidBody.AddForce (forceVector * force);

			_animator.SetInteger ("Velocity", (int)(Mathf.Abs (_rigidBody.velocity.x) * 1000));

			_animator.SetInteger ("Velocity", (int)(_rigidBody.velocity.x * 1000));

			if (_rigidBody.velocity.x < 0) {

				gameObject.transform.localScale = new Vector3 (-1, 1, 1);
				directionDwarf = -1;
			} else {
				gameObject.transform.localScale = new Vector3 (1, 1, 1);
				directionDwarf = 1;
			}

			_animator.SetBool ("Grounded", isGrounded ());
		}



	}

	private bool isGrounded()
	{

		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();

		RaycastHit2D res = Physics2D.Linecast (
			new Vector2(gameObject.transform.position.x, 
				gameObject.transform.position.y-(sr.bounds.size.y/2+0.1f)), 

			new Vector2(gameObject.transform.position.x, 
				gameObject.transform.position.y-(sr.bounds.size.y/2+0.2f))); 



		Debug.DrawLine (new Vector2(gameObject.transform.position.x, 
			gameObject.transform.position.y-(sr.bounds.size.y/2+0.1f)), 
			new Vector2(gameObject.transform.position.x, gameObject.transform.position.y-(sr.bounds.size.y/2+0.2f)));
		
		if (res != null && res.collider!=null) 
		{
			Debug.Log (res.collider.gameObject.name);
		}
			return res.collider != null;
		
	}

	public void Death()
	{

		_dead = true;

		_animator.SetBool ("Dead", true);


	}

	public int DwarfDirection()
	{
		//_rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		/*if (_rigidBody.velocity.x < 0) {

			directionDwarf = -1;
		} else {
			
			directionDwarf = 1;
		}*/
		return directionDwarf;
	}


}


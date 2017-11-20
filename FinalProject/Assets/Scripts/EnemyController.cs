using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
	[SerializeField] private LayerMask enemyMask;

	[SerializeField] private float speed = 1;

	[SerializeField] GameObject enemyExplosion;

	private Rigidbody2D _rigidBody;

	private Transform _transform;

	private float _width;
	//0.60

	private float _height;
	//0.60
	//private Animator _animator;
	//[SerializeField] private float force= 1f;


	// Use this for initialization
	void Start () {

		_rigidBody = gameObject.GetComponent<Rigidbody2D> ();

		_transform = gameObject.GetComponent<Transform> ();

		//_animator = gameObject.GetComponent<Animator> ();
		//_rigidBody = gameObject.GetComponent<Rigidbody2D> ();

		SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer> ();

		_width = spriteR.bounds.extents.x;

		_height = spriteR.bounds.extents.y;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//Vector2 forceVector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		//_rigidBody.AddForce (forceVector * force);

		//_animator.SetBool ("Idle", true);

		Vector2 LineCastPos = (Vector2)gameObject.transform.position -
		                      (Vector2)gameObject.transform.right * _width - Vector2.up * _height;

		Debug.DrawLine (LineCastPos, LineCastPos + Vector2.down);

		bool isGrounded = Physics2D.Linecast (LineCastPos, LineCastPos + Vector2.down, enemyMask);


		/*RaycastHit2D res = Physics2D.Linecast (
			new Vector2(gameObject.transform.position.x, 
				gameObject.transform.position.y-(sr.bounds.size.y/2)), 

			new Vector2(gameObject.transform.position.x, 
				gameObject.transform.position.y-(sr.bounds.size.y/2+0.0001f))); 



		Debug.DrawLine (new Vector2(gameObject.transform.position.x, 
			gameObject.transform.position.y-(sr.bounds.size.y/2)), 
			new Vector2(gameObject.transform.position.x, gameObject.transform.position.y-(sr.bounds.size.y/2+0.0001f)));*/

		if (!isGrounded) 
		{
			Vector3 currentRotation = _transform.eulerAngles;

			currentRotation.y += 180;

			_transform.eulerAngles = currentRotation;
		}

		Vector2 vel = _rigidBody.velocity;
		vel.x = -_transform.right.x * speed;
		_rigidBody.velocity = vel;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag.Equals("fireBall"))
		{
			Instantiate(enemyExplosion).GetComponent<Transform>().position =
			other.gameObject.GetComponent<Transform> ().position;
			Destroy (other.gameObject);
			Destroy (this.gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The EnemyController.cs script
 * Alisa Ordina
 * id: 100967526
 * November 17, 2017
 * 
 * This following script component is attached to enemy game object in the scene.
 * This following code allows to access the enemy game object and its components in order
 * to apply an appropriate movement behaviour. The layer mask would be used not really
 * for the colision but more for the line casting similar that was coded in DwarfController script.
 * So, creating a mask based on the layer this could be used for line casting and applying
 * appropriate movements on platform. Becuase this enemy game object is assigned to its own
 * designated platform spot the line casting is applied in orderto detect when the platfrom's edge 
 * has reached. When the enemy game object detects the endge ofthe platfrom itis not grounded
 * then the enemy game object will be inversed flipped horizontalally to go to the oposite way, resetthe position
 * and travel in an oposite way until it reaches the otherend point of the platform edge.
 * 
 * 
*/

public class EnemyController : MonoBehaviour 
{
	
	//The enemy layeris set uo as everything except unselected enemy in orderto apply the flip on itwhen appropriate.
	//setting up the enemy layermask that would set its value
	[SerializeField] private LayerMask enemyMask;

	//Declaire public variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated horizontal x axis speed of the enemy's game object's movement.
	[SerializeField] private float speed = 1;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated game object that is called enemyExplosion
	[SerializeField] GameObject enemyExplosion;

	//Declaire private variable
	//This variable is assigned to a designated Rigidbody2D variable.
	//Which means this is assigned as a physics component of this 2D Dwarf sprite 
	//component. This is assigned in order to 
	//use the physics of this game object in the scene
	private Rigidbody2D _rigidBody;

	//This variable is from Unity the transform component 
	//The Trasform is defined in Unity as position, rotation and scale
	//Here it is going to be used for its coordinates, it is going to be used for the position.
	private Transform _transform;

	//set up the scale of enemy game object's width
	private float _width;
	//0.60

	//set up the scale of enemy game object's height
	private float _height;
	//0.60
	//private Animator _animator;
	//[SerializeField] private float force= 1f;
	private AudioSource[] _audioSources;

	private AudioSource _enemyExplosionSound;
	private AudioSource _enemySound;

	private bool isDead = false;

	// Use this for initialization
	void Start () 
	{
		//_enemyExplosionSound = gameObject.GetComponent<AudioSource> ();
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._enemyExplosionSound = this._audioSources [0];
		this._enemySound = this._audioSources [1];
		//Here the set up of the Rigidbody2D component. 
		//This Rigidbody2D Component is accessed from this specific game object
		//which the script is attached to the enemy game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Rigidbody2D that allows to access the physics part of this game object.
		//This is set up, so the specific methods could be applied to control 
		//this game object's physics and invoke such as velocity method when appropriate.
		//Now that the Rigidody is also glabbaly declaired, this means that Rigidbody 
		//could be used in update function and apply a designated force on to it and move the game object.
		_rigidBody = gameObject.GetComponent<Rigidbody2D> ();

		//Here the set up of the Tranform. 
		//The Transform is accessed from this specific game object
		//which the script is attached to which is enemy game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Transform which is scale, rotation and its position.
		//This is set up so the specific methods could be applied to control 
		//this game object's position in the scene.
		_transform = gameObject.GetComponent<Transform> ();

		//_animator = gameObject.GetComponent<Animator> ();
		//_rigidBody = gameObject.GetComponent<Rigidbody2D> ();

		//Accessing the enemy game object's sprite renderer
		//This is used later to access the sprite's size and access the sprite boundary size in order to 
		//draw the ray line appropriatly.
		SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer> ();

		//set up the enemy sprite width size
		//from the reference from the sprite rendeder component
		_width = spriteR.bounds.extents.x;

		//set up the enemy sprite height size
		//from the reference from the sprite rendeder component
		_height = spriteR.bounds.extents.y;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//Vector2 forceVector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		//_rigidBody.AddForce (forceVector * force);

		//_animator.SetBool ("Idle", true);

		//First to draw the line,start with the starting position of the enemy which
		//would be equals to the enemy game object position which is casted as Vector 2
		//and it is subtracted to move ot down and to the left position in front of the enemy game object.
		//This is to track if the enemy game object is grounded as it goes towards the edgeof theplatfrom this function 
		//would be triggered to reverse this enemy game object horizontally in order for it to go the other way 
		//anoposite way.
		Vector2 LineCastPos = (Vector2)gameObject.transform.position -
		                      (Vector2)gameObject.transform.right * _width - Vector2.up * _height;

		//the Debug would help to see how this line is drawn in orderto adjust it if needs to be.
		//This function takes the starting line which is the lineCastPos that is stated above
		//andthe end would be addingthe enemy Vector2 down value
		Debug.DrawLine (LineCastPos, LineCastPos + Vector2.down);

		//Now is Grounded will be set and calculate if this enemy game object is grounded. Whether 
		//this enemy game object is grounded the value for it is calculated by using the Physics2D
		//and the start of the linecast which is the LineCastPos variable and the end value which is 
		//LineCastPos + Vector2.down and the third [arameter is enemymask the reason why enemy mask is there
		//this way the line will not get the enmy game objecton to its account it will only solely 
		//detect the platform collider2D and if the enemy gameobject if it is grounded or not
		bool isGrounded = Physics2D.Linecast (LineCastPos, LineCastPos + Vector2.down, enemyMask);


		/*RaycastHit2D res = Physics2D.Linecast (
			new Vector2(gameObject.transform.position.x, 
				gameObject.transform.position.y-(sr.bounds.size.y/2)), 

			new Vector2(gameObject.transform.position.x, 
				gameObject.transform.position.y-(sr.bounds.size.y/2+0.0001f))); 



		Debug.DrawLine (new Vector2(gameObject.transform.position.x, 
			gameObject.transform.position.y-(sr.bounds.size.y/2)), 
			new Vector2(gameObject.transform.position.x, gameObject.transform.position.y-(sr.bounds.size.y/2+0.0001f)));*/

		//Check to see if the enemy game object is grounded
		if (!isGrounded) 
		{
			//if it is true and enemy game object is not grounded then this
			//function get invoked.
			//So, if this enemy game object is not grounded, this game object should be switched to go to the oposite way.
			//The rotate is sued because the line wouldbe rotated along with the enemy game object, if flip horizontally is applied
			//like previously -1, 1, 1 the line cast would stay the same so that is why the whole game object
			//sould be rotated it the eulerAngles in Rigid body is applied
			Vector3 currentRotation = _transform.eulerAngles;

			//then rotate the enemy game object along with the linecast and the whole thing will be applied
			//and rotate it around in the y axis in 180 degrees which mean turn it around
			currentRotation.y += 180;

			//update the position of enemy game object
			_transform.eulerAngles = currentRotation;
		}

		//setting up the velocity of this enemy game object from its Rigidbody component
		//In order to move this enemy game object the rigidbody velocity is used.
		//This is set up to know/to store the current velocity of this enemy game object from its rigidbody
		Vector2 vel = _rigidBody.velocity;

		//then apply the indicated speed to this enemy game object in horizontal axis
		//This is the setting up the velocity of its designated predifined speed in horizontal axis.
		vel.x = -_transform.right.x * speed;

		//update the enemy gameobject's speed
		//set it back to the enemy game object's body.
		_rigidBody.velocity = vel;

		if (isDead) {
			if (this._enemyExplosionSound.isPlaying) {

			} else {
				Destroy (this.gameObject);
			}
		}
	}

	//This is the function that would detect the collision with
	//this enemy game object. When Unity calculates that the enemy object
	//has intercepted with a different game object this function would be executed.
	//This is the fact that when the enemy object has intercepted with another
	//game object it triggered (intercepted) with each other.
	//This function is executed and the following conditions is invoked.
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals ("Player")) {

			if (this._enemySound!= null) {
				this._enemySound.Play ();
			}

		}
		//if the collisionhappenedwith the fire game object and its tag is equals to the fireball tag
		if(other.gameObject.tag.Equals ("fireBall"))
		{
			if (this._enemyExplosionSound!= null) {
				this._enemyExplosionSound.Play ();
			}
			//if this is true and the collision happened with the fireball tag
			//then instantiate/create the enemyexplosion game object on to the scene where 
			//on to the fireball game object's position thatis located in the scene


			Instantiate(enemyExplosion).GetComponent<Transform>().position =
			other.gameObject.GetComponent<Transform> ().position;


			//once the collision has happened and the creation of the enemy explosion was created
			//destroy/remove the fire ball game object from the scene
			//Destroy (other.gameObject);

			//once the collision has happened and the fire ball game object was removed fromthe scene then
			//destroy/remove the enemy game object from the scene
			//Destroy (this.gameObject);
			isDead= true;
		}
	}
}

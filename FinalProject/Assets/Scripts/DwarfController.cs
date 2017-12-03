using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The DwarfController.cs script
 * Alisa Ordina
 * id: 100967526
 * November 17, 2017
 * 
 * This script component is attached to Dwarf game object in the scene.
 * This following code does few things such as
 * applying force which changes the Dwarf's speed and its jump speed.
 * It allows the user to control Dwarf game object
 * in the scene by applying the specific input and the application of forces 
 * and physics in the hosrizontal and vertical direction in ebling the user to control their avatar.
 * So,using physics and forces would allow the user to control the Dwarf game object in the scene.
 * 
*/

public class DwarfController : MonoBehaviour 
{
	//Declaration of the public force variable that could be edited in Unity's Inspector 
	//in order to define the strength of the force which would be appropriate to apply for this Dwarf
	//game object in the scene.
	[SerializeField] private float force= 1f;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned as a Tranform to a designated game object that is called spawnPoint
	[SerializeField] private Transform spawnPoint;

	//Declaration of the public force variable that could be edited in Unity's Inspector 
	//in order to define the strength of the force for the 
	//Dwarf's jump, vector up force which would be appropriate to apply for this Dwarf
	//game object in the scene.
	[SerializeField] private float jumpForce = 30f;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated game object that is called fireBall
	[SerializeField] GameObject fireBall; 


	//Declaire public variable 
	//This variable is assigned to a designated direction positive one point.
	//This is the positive one point, the direction the Dwarf is facing.
	//Sice it is positive it is facing to the right and not flipped horizontally yet.
	//The object is at its initial position facing right.
	public int directionDwarf = 1;

	//Declaire private variable
	//This variable is assigned to a designated Animator variable.
	//Which means this is Animator interface is assigned in order to 
	//control mecanim and its animation system.
	private Animator _animator;

	//Declaire private variable
	//This variable is assigned to a designated Rigidbody2D variable.
	//Which means this is assigned as a physics component of this 2D Dwarf sprite 
	//component. This is assigned in order to 
	//use the physics of this game object in the scene
	private Rigidbody2D _rigidBody;

	private CapsuleCollider2D _capsCol;

	//Declaire private variable
	//This variable is assigned to a designated boolean
	//varibale in order to work as a flag and to check
	//if this flag is true in order to trigger the appropriate functions when it is approriate.
	private bool _dead = false;

	//Declaire private variable
	//This variable is assigned to a designated boolean
	//varibale in order to work as a flag and to check
	//if this flag is true in order to trigger the appropriate functions when it is approriate.
	private bool _hurt = false;

	//Declaire private variable.
	//This variable is assigned to a designated game object that is called ShoteOut
	private GameObject ShoteOut = null;

	// Use this for initialization
	void Start () 
	{


		_capsCol = gameObject.GetComponent<CapsuleCollider2D> ();
		//Here the set up of the Rigidbody2D component. 
		//This Rigidbody2D Component is accessed from this specific game object
		//which the script is attached to the Dwarf game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Rigidbody2D that allows to access the physics part of this game object.
		//This is set up, so the specific methods could be applied to control 
		//this game object's physics and invoke such as velocity method when appropriate.
		//Now that the Rigidody is also glabbaly declaired, this means that Rigidbody 
		//could be used in update function and apply a designated force on to it and move the game object.
		_rigidBody = gameObject.GetComponent<Rigidbody2D> ();


		//Here the set up of the Animator component. 
		//This Animator Component is accessed from this specific game object
		//which the script is attached to teh Dwarf game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Animator and its parameters when appropriate to set up its animation when 
		//the specific state is invoked.
		//This is set up, so the specific methods could be applied to control 
		//this game object's Animator and invoke its state methods when appropriate.
		_animator = gameObject.GetComponent<Animator> ();

		//This returns the Dwarf game object to the spawn point position game object.
		//When the user starts the game, the Dwarf position transform point will be equals
		//to the spawn poing traform position. This enables for Dwarf game object to start atthat specific point
		//its (initial position).
		gameObject.transform.position = spawnPoint.position;

		Player.Instance.HasMineral = false;

	}

	// Update is called once per frame
	//The fixed update is used because in this game the collisions should be detected.
	//So, it is used to detect the collisions.
	void FixedUpdate () 
	{

		//Checks if the boolean flag Dead is true
		if (!_dead || !_hurt) 
		{

			//float jump = Input.GetAxis ("Jump");

			//Using the Unity's Editor -> Input controls, the Vertical axis is getting accessed
			//for crawl which is s key is declaired
			float crawl = Input.GetAxis ("Vertical");

			//Using the Unity's Editor -> Input controls, the Fire1 is getting accessed
			//for Dwarf's Fire which is space key is declaired as fire variable.
			float fire = Input.GetAxis ("Fire1");

			//Using the Unity's Editor -> Input controls, the Vertical axis and Horizontal axis is getting accessed for 
			//the game object's direction.
			Vector2 forceVector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

			//if (jump > 0 && isGrounded()) 

			//Here to check if the user has pressed the w key and 
			//if the game object's line cast is crossed with the platform 2D collider
			//if the game object is grounded on the platform.
			if(Input.GetKeyDown("w") && isGrounded() )
			{

				//When that condition is true then apply force vertically. 
				//The vector 2 up vector getting multiplied by the strength of the predifined force
				//in order to lift the game object upwards.
					_rigidBody.AddForce (Vector2.up * jumpForce);


					
			}

			//When the vectical axis is less then zero negative key s
			//is pressed
			if (crawl < 0) 
			{
				//If this condition is true then set Cwarl bool parameter of Animator to true in
				//order to trigger the crawl animation.
				_animator.SetBool ("Crawl", true);

				_capsCol.size = new Vector2(1.03f, 1);

				_capsCol.offset = new Vector2(0, -0.25f);


			}


			else 
			{
				//When the condition is false then reset the Crawl paramenter bool to false.
				//In order to stop the crawl animation
				_animator.SetBool ("Crawl", false);

				_capsCol.size = new Vector2(1.03f, 1.49f);

				_capsCol.offset = new Vector2(0, 0);

			}

			//If the fire positive key is getting pressed that means above zero then invoke this condition
			if (fire >0) 
			{
				//If this condition is true then set Fire bool parameter of Animator to true in
				//order to trigger the Dwarf's attack animation.
				_animator.SetBool ("Fire", true);

				//First the Dwarf game object's tranform gets accessed to get
				//the x and y coordinates of the Dwarf's position point.
				//Storing the current Dwarf's position in Vector2 positionOfDwarf variable 
				Vector2 positionOfDwarf = new Vector2 (this.gameObject.transform.position.x, this.gameObject.transform.position.y);

				//Instantiate (fireBall, positionOfDwarf, Quaternion.identity);

				//Check if the fire ball has been destroyed. This is in order
				//to intantiate/create the fireball sequencially. After certain amount of time 
				//till the previous fireball has been destroyed. This is in orderto apply
				//breaks between the fireballs when it is getting fired again by Dwarf game object.
				if(ShoteOut == null)
				{
					//Once the fire ball previous was destryed intanciate/create another 
					//fireball on to the Dwarf's position
					ShoteOut = Instantiate (fireBall, positionOfDwarf, Quaternion.identity);

				//Instantiate (fire, positionOfDwarf, Quaternion.identity);
				}

			}

			else
			{
				//After the fire statement was invoked then reset the Fire boolean to false
				//in order to terminate the Dwarf's attack animation.
				_animator.SetBool ("Fire", false);
			}
			
			//Here the predifined strength ofthe force is applied to the horizantal and vertical axis
			//in order to control the game object in the scene.
			_rigidBody.AddForce (forceVector * force);


			//_animator.SetInteger ("Velocity", (int)(Mathf.Abs (_rigidBody.velocity.x) * 1000));

			//Getting the Velocity integer to a specified number
			_animator.SetInteger ("Velocity", (int)(_rigidBody.velocity.x * 1000));

			//Getting the direction of the sprite
			if (_rigidBody.velocity.x < 0) 
			{
				//If the sprite direction is heading less than zero then flip the image horizontally
				gameObject.transform.localScale = new Vector3 (-1, 1, 1);

				//setting up the direction of the Dwarf game object's to negative 1
				directionDwarf = -1;
			} 
			else if (_rigidBody.velocity.x > 0) 
			{
				//if the Dwarf game object heading direction is posive then leave the sprite direction as it is.
				//do not flip it horizontally
				gameObject.transform.localScale = new Vector3 (1, 1, 1);

				//setting up the direction of the Dwarf game object's to positive 1
				directionDwarf = 1;
			}

			//Set the animation of Dwarf's jump and its boolean parameter Grounded to 
			//the designated isGrounded() function that return true orfalse depending
			//if the game object's line cast crosees with the platform collider2D.
			//If if crosses with the platform collider 2D the Grounded parameter is false.
			//If the line cast/ray does not cross the platform collider then return to true
			//in order to trigger the Dwarf's jump animation.
			_animator.SetBool ("Grounded", isGrounded ());
		}



	}

	//This is to check is the Dwarf game object is on the platform or is in the air.
	//By checking if Dwarf game object is on the platform the line Ray is drawn

	private bool isGrounded()
	{
		//Accessing the Dwarf game object's sprite renderer
		//This is used later to access the sprite's size and access the sprite boundary size in order to 
		//draw the ray line appropriatly.
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();

		//Drawing the line and measuring it by appropriate amount of the sprite's size.
		//The line cast requiresthe start point and end point this is where
		//the game object would be detected wheter the line cast has hit the platform collider
		//this wherewould be detectedifthisgame object is on the platform. That is why y axis point is subtracted 
		//of the game object's bound size that come from
		//the sprite renderer, in a way that a litle bit line is just a bit underneath of the sprite.
		RaycastHit2D res = Physics2D.Linecast (
			new Vector2(gameObject.transform.position.x, 
				gameObject.transform.position.y-(sr.bounds.size.y/2+0.1f)), 

			new Vector2(gameObject.transform.position.x, 
				gameObject.transform.position.y-(sr.bounds.size.y/2+0.2f))); 

	//The Deug is useful because it will notify if this game object has hitthe other collider
		//such as platform. This is useful to see if the line is long enough to detect 
		//the other platform collider.
		Debug.DrawLine (new Vector2(gameObject.transform.position.x, 
			gameObject.transform.position.y-(sr.bounds.size.y/2+0.1f)), 
			new Vector2(gameObject.transform.position.x, gameObject.transform.position.y-(sr.bounds.size.y/2+0.2f)));

		//If the Raycasthit2D is not null means that the collider has
		//crossed with the other collider such as platform collider
		//that means that the Dwarf game object is on the platform and return false
		if (res != null && res.collider!=null) 
		{
			//debug draw the line, Debug outputs/prints the name of the object 
			//that they ray collides with.
			Debug.Log (res.collider.gameObject.name);
		}
		//when res is equal to true to null that means that this game object 
		//is not colliding with anything that means this game object is in the air.
			return res.collider != null;
		
	}

	public void Death()
	{

		//set the boolean flag _dead to true
		_dead = true;

		//set animator's boolean paracmeter to true in order to invoke the Dwarf's death animation
		_animator.SetBool ("Dead", true);


	}

	public void Hurt()
	{

		//set the boolean flag _dead to true
		_hurt = true;

		//set animator's boolean paracmeter to true in order to invoke the Dwarf's death animation
		_animator.SetBool ("Hurt", true);


	}
	//This is return the direction of theDwarf game object
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


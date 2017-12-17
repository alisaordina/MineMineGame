using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The DwarfController.cs script
 * Alisa Ordina
 * id: 100967526
 * November 17, 2017
 * Modified on December 11, 2017
 * 
 * This script component is attached to Dwarf game object in the scene.
 * This following code does few things such as
 * applying force which changes the Dwarf's speed and its jump speed.
 * It allows the user to control Dwarf game object
 * in the scene by applying the specific input and the application of forces 
 * and physics in the hosrizontal and vertical direction in enabling the user to control their avatar.
 * So, using physics and forces would allow the user to control the Dwarf game object in the scene.
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
	[SerializeField] private Transform spawnPoint = null;

	//Declaration of the public force variable that could be edited in Unity's Inspector 
	//in order to define the strength of the force for the 
	//Dwarf's jump. This force would be multiplied to the vector up, which would be applied 
	//for the strength of the Dwarf's jump. When it is appropriate to apply such as using an arrow up key to 
	//activate the Dwarf's jump for this Dwarf game object in the scene.
	[SerializeField] private float jumpForce = 30f;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated game object that is called fireBall
	[SerializeField] GameObject fireBall = null; 


	//Declaire public variable 
	//This variable is assigned to a designated direction positive one point.
	//This is the positive one point, represents the direction the Dwarf is facing.
	//Since it is positive, it is facing to the right and not flipped horizontally yet.
	//The object is at its initial position facing to the right.
	public int directionDwarf = 1;

	//Declaire private variable
	//This variable is assigned to a designated Animator component.
	//An Animator interface is assigned in order to 
	//control mecanim and its animation system.
	private Animator _animator;

	//Declaire private variable
	//This variable is assigned to a designated Rigidbody2D component of this game object.
	//It is a physics component of this 2D Dwarf sprite 
	//component. This is assigned in order to 
	//use the physics of this game object in the scene
	private Rigidbody2D _rigidBody;


	//Declaire private variable
	//This variable is assigned to a designated Capsule Collider2D component of this game object.
	//It is a physics component of this 2D Dwarf sprite 
	//component. This is assigned in order to 
	//use the physics of this game object in the scene and to control
	//the size of the capsule collider 2D in order to change that size of the collider 2D
	//in order to have it smaller when the Dwarf game object is in crouched position.
	//This Capsule Collider2D game object's component is stored under variable _capsCol.
	private CapsuleCollider2D _capsCol;

	//Declaire private variable
	//This variable is assigned to a designated boolean
	//varibale in order to work as a flag and to check
	//if this flag is true in order to trigger the appropriate functions when it is approriate.
	private bool _isDead = false;

	//Declaire private variable
	//This variable is assigned to a designated boolean
	//varibale in order to work as a flag and to check
	//if this flag is true in order to trigger the appropriate functions when it is approriate.
	//private bool _ishurt = false;

	//Declaire private variable.
	//This variable is assigned to a designated game object that is called ShoteOut
	private GameObject ShoteOut = null;

	// Use this for initialization
	void Start () 
	{

		//Here the set up of the Capsule Collider2D component. 
		//This Capsule Collider2D Component is accessed from this specific game object
		//which the script is attached to the Dwarf game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Capsule Collider2D that allows to access the physics Collider2D part of this game object.
		//This is set up, so the specific methods could be applied to control 
		//this game object's 2D Capsule Collider and invoke methods such as the size of the 2DCapsule Collider when appropriate.
		//Now that the Capsule Collider2D is also glabally declaired, this means that Capsule Collider2D 
		//could be used in the update function and apply a designated sizes of the CapsuleCollider 2D 
		//on to this game object.
		_capsCol = gameObject.GetComponent<CapsuleCollider2D> ();

		//Here the set up of the Rigidbody2D component. 
		//This Rigidbody2D Component is accessed from this specific game object
		//which the script is attached to the Dwarf game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Rigidbody2D that allows to access the physics part of this game object.
		//This is set up, so the specific methods could be applied to control 
		//this game object's physics and invoke such as velocity method when appropriate.
		//Now that the Rigidody is also glabally declaired, this means that Rigidbody 
		//could be used in update function and apply a designated force on to 
		//this game object and move it when appropriate.
		_rigidBody = gameObject.GetComponent<Rigidbody2D> ();


		//Here the set up of the Animator component. 
		//This Animator Component is accessed from this specific game object
		//which the script is attached to the Dwarf game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Animator and its parameters when appropriate to set up its animation when 
		//the specific state is invoked.
		//This is set up, so the specific methods could be applied to control 
		//this game object's Animator and invoke its state methods when appropriate.
		_animator = gameObject.GetComponent<Animator> ();

		//This returns the Dwarf game object to the spawn point position game object.
		//When the user starts the game, the Dwarf position transform point will be equals
		//to the spawn poing traform position. This enables for Dwarf game object to start at that specific point
		//its (initial position).
		gameObject.transform.position = spawnPoint.position;

		//Setting up the boolean flag that indicated that the Dwarf game object 
		//has not yet fulfilled the game's objection which is to find and pick up the mineral.
		//So, here is the initial mineral boolean flagwhichis set to false
		Player.Instance.HasMineral = false;

	}

	// Update is called once per frame
	//The fixed update is used because in this game the collisions should be detected.
	//So, it is used to detect the collisions.
	void FixedUpdate () 
	{

		//Checks if the boolean flag isDead is equals to false in order to trigger this statement.
		//if this statement is not true and Dwarf is dead and boolean flag _isDead is true then this tatement does not get invoked
		//and the user cannot apply all the controls to this Dwarf game object in the scene.
		if (!_isDead) 
		{

			//float jump = Input.GetAxis ("Jump");

			//Using the Unity's Editor -> Input controls, the Vertical axis is getting accessed
			//for crawl which is 's' key and 'down' arrow key is declaired
			float crawl = Input.GetAxis ("Vertical");

			//Using the Unity's Editor -> Input controls, the Fire1 is getting accessed
			//for Dwarf's Fire which is 'space' key is declaired.
			float fire = Input.GetAxis ("Fire1");

			//Using the Unity's Editor -> Input controls, the Vertical axis and Horizontal axis is getting accessed for 
			//the game object's direction.
			Vector2 forceVector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

			//if (jump > 0 && isGrounded()) 

			//------------------------------------------------------------------------------
			//Debug purpose
			//if(Input.GetKeyDown("w") && Input.GetKeyDown(KeyCode.UpArrow) && isGrounded())
			//{

				//Apply the indicated jumpforce variable, float number force strength and multiply to the vector's up direction.
				//_rigidBody.AddForce (Vector2.up * jumpForce);

			//}
			//-------------------------------------------------------------------------------

			//Here to check if the user did press the w key and 
			//if the game object's line cast has crossed with the platform 2D collider 
			//which means if Dwarf game object is on the platform.
			//So, if the game object is grounded on the platform, and if the w key was pressed, then invoke this statement.
			if(Input.GetKeyDown("w") && isGrounded())
				{

				//Apply the indicated jumpforce variable, float number force strength and multiply to the vector's up direction.
					_rigidBody.AddForce (Vector2.up * jumpForce);

				}

			//Here to check if the user did press the 'up' arrow key and 
			//if the game object's line cast has crossed with the platform 2D collider 
			//which means if Dwarf game object is on the platform.
			//So, if the game object is grounded on the platform, and if the the 'up' arrow key was pressed, then invoke this statement.
			else if(Input.GetKeyDown(KeyCode.UpArrow) && isGrounded())
			{

				//When that condition is true then apply force vertically. 
				//The vector 2 up vector getting multiplied by the strength of the predifined force
				//in order to lift the game object upwards.
					_rigidBody.AddForce (Vector2.up * jumpForce);


					
			}

			//------------------------------------------------------------------------------
			//Debug purpose
			/*if (Input.GetKeyDown (KeyCode.UpArrow)) 
			  {
				Jump ();
			} 
			else if (Input.GetKeyDown ("w")) 
			{
				Jump ();
			}*/
			//------------------------------------------------------------------------------

			//When the vectical axis is less then zero negative key s
			//is pressed
			if (crawl < 0) 
			{
				
				//If this condition is true then set Cwarl bool parameter of Animator to true in
				//order to trigger the crawl animation.
				_animator.SetBool ("Crawl", true);

				//Apply changes to the Capsule Collider2D in order for this Dwarf
				//game object to get to narrower places.
				//----Vertically--------------------------
				_capsCol.size = new Vector2(1.03f, 1);

				//----Horizantally------------------------
				_capsCol.offset = new Vector2(0, -0.25f);

			}


			else 
			{
				//When the condition is false then reset the Crawl paramenter bool to false.
				//In order to stop the crawl animation
				_animator.SetBool ("Crawl", false);

				//Apply changes to the Capsule Collider2D in order for this Dwarf's
				//game object to reset the Capsule Collider2D back to its normal size.
				//----Vertically--------------------------
				_capsCol.size = new Vector2(1.03f, 1.49f);

				//----Horizantally------------------------
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

				//Check if the fire ball has been destroyed. This is in order
				//to intantiate/create the new fireball game object in specific order. After certain amount of time 
				//till the previous fireball has been destroyed. This is in order to apply
				//breaks between the fireballs when it is getting fired again by Dwarf game object.
				if(ShoteOut == null)
				{
					//Once the fire ball previous was destroyed intantiate/create another 
					//new fireball on to the Dwarf's position
					ShoteOut = Instantiate (fireBall, positionOfDwarf, Quaternion.identity);


					//Previously used
					//Instantiate (fire, positionOfDwarf, Quaternion.identity);

				}

			}

			else
			{
				
				//After the fireball function statement was invoked, then reset the Fire 
				//boolean of animator parameter to false in order to 
				//terminate the Dwarf's attack animation.
				_animator.SetBool ("Fire", false);
			}
			
			//Here the predifined strength of the force is applied to the horizantal and vertical axis
			//in order to control the Dwarf game object in the scene.
			_rigidBody.AddForce (forceVector * force);

			//Previously used
			//_animator.SetInteger ("Velocity", (int)(Mathf.Abs (_rigidBody.velocity.x) * 1000));

			//Setting up the Velocity as an integer to a specified number
			_animator.SetInteger ("Velocity", (int)(_rigidBody.velocity.x * 1000));

			//Getting the direction of the sprite
			//if the sprite, Dwarf game object is facing to the left direction, it is less than zero 
			if (_rigidBody.velocity.x < 0) 
			{
				//If that is true then the sprite direction is less than zero, then flip the image horizontally
				//in its local scale from the transform component of this game object.
				gameObject.transform.localScale = new Vector3 (-1, 1, 1);

				//setting up the direction of the Dwarf game object to negative 1
				//indicating that the Dwarf game object is facing to the left
				directionDwarf = -1;
			} 

			//Getting the direction of the sprite
			//if the sprite, Dwarf game object is facing to the right direction, it is greater than zero 
			else if (_rigidBody.velocity.x > 0) 
			{
				//if the Dwarf game object heading direction is posive then leave the sprite direction as it is.
				//do not flip it horizontally,
				//in its local scale from the transform component of this game object.
				gameObject.transform.localScale = new Vector3 (1, 1, 1);

				//setting up the direction of the Dwarf game object's to positive 1
				//indicating that the Dwarf game object is facing to the right
				directionDwarf = 1;
			}

			//Set the animation of Dwarf's jump and its boolean parameter Grounded to 
			//the designated isGrounded() function will return boolean true or false depending
			//if the game object's line cast crosees with the platform collider2D.
			//If if crosses with the platform collider 2D the Grounded parameter is false.
			//If the line cast/ray does not cross the platform collider then return to true
			//in order to trigger the Dwarf's jump animation.
			_animator.SetBool ("Grounded", isGrounded ());
		}
	}

	//---------------------------------------------------------
	//Previously used
	/*private void Jump()
	{
		if (isGrounded ()) {
			_rigidBody.AddForce (Vector2.up * jumpForce);

		}
	}*/
	//---------------------------------------------------------


	//This is to check is the Dwarf game object is on the platform or is in the air.
	//By checking if Dwarf game object is on the platform the line Ray is drawn
	private bool isGrounded()
	{
		
		//Accessing the Dwarf game object's sprite renderer
		//This is used later to access the sprite's size and access the sprite boundary size in order to 
		//draw the ray line appropriatly.
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();

		//Drawing the line and measuring it by appropriate amount of the sprite's size.
		//The line cast requires the start point and end point this is where
		//the game object would be detected wheter the line cast has hit the platform collider
		//this is where would be detected if this game object is on the platform. That is why y axis point is subtracted 
		//of the game object's bound size.The size is referenced from the
		//sprite renderer, in a way that a litle bit line is just a bit underneath of the sprite.
		RaycastHit2D res = Physics2D.Linecast (
			new Vector2(gameObject.transform.position.x, 
				gameObject.transform.position.y-(sr.bounds.size.y/2+0.1f)), 

			new Vector2(gameObject.transform.position.x, 
				gameObject.transform.position.y-(sr.bounds.size.y/2+0.2f))); 

		//The Debug is useful because it will show if the line is long enough to detect 
		//the other platform collider.
		Debug.DrawLine (new Vector2(gameObject.transform.position.x, 
			gameObject.transform.position.y-(sr.bounds.size.y/2+0.1f)), 
			new Vector2(gameObject.transform.position.x, gameObject.transform.position.y-(sr.bounds.size.y/2+0.2f)));

		//If the Raycasthit2D is not null means that the collider has
		//crossed with the other collider such as platform collider
		//that means that the Dwarf game object is on the platform.
		if (res != null && res.collider!=null) 
		{
			//If it is true, if the Dwarf game object is on the platform 
			//then debug output/print the name of the object's name
			//that they ray cast collides with.
			Debug.Log (res.collider.gameObject.name);
		}

		//This result will return boolean which is set up in jump animator 
		//indicating whether the Dwarf game object is true or false in the air or on the ground.
		//Setting up the animator and its animation jump when appropriate.
			return res.collider != null;
		
	}

	//This function, sets up the death animation in its animator and resets 
	//the boolean isDead to true, indicating that the Dwarf game object is dead.
	public void Death()
	{

		//set the boolean flag _dead to true
		_isDead = true;

		//set animator's boolean paracmeter to true in order to invoke the Dwarf's death animation
		_animator.SetBool ("Dead", true);

	}


	//This function, sets up the different hurt/death animation in its animator and resets 
	//the boolean isDead to true, indicating that the Dwarf game object is hurt and then dead.
	public void Hurt()
	{
		//set the boolean flag _dead to true
		_isDead = true;

		//set animator's boolean parameter to true in order to invoke the Dwarf's death animation
		_animator.SetBool ("Hurt", true);

	}

	//This is return the direction of theDwarf game object
	public int DwarfDirection()
	{

		//Previously used
		//--------------------------------
		/*if (_rigidBody.velocity.x < 0) 
		  {

			directionDwarf = -1;
		} 
		else 
		{
			
			directionDwarf = 1;
		}
		*/
		//--------------------------------


		return directionDwarf;

	}

	//This is a public function that returns boolean which
	//indicates if the Dwarf game objecthad dies and the dead animation has been triggered.
	//This is used for cancelling other colliders in the other scripts that would access this boolean method.
	public bool isDwarfDead()
	{
		//Setting up a new variable, with its indicated boolean variable.
		//This new variable would be updated if Dwarf's death animation was triggered.
		bool isDwarf_Dead = _isDead;

		//Return the boolean value, that would indicated whether this Dwarf game object is still alive
		return isDwarf_Dead;
	}


}


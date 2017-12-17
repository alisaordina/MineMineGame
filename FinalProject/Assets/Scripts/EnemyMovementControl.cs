using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The EnemyController.cs script
 * Alisa Ordina
 * id: 100967526
 * November 25, 2017
 * 
 * This following script component is attached to enemy game object in the scene.
 * This following code allows to access the enemy game object and its components in order
 * to apply an appropriate movement behaviour. The layer mask would be used not really
 * for the colision but more for the line casting similar that was coded in DwarfController script.
 * So, creating a mask based on the layer this could be used for line casting and applying
 * appropriate movements on the ground. Becuase this enemy game object is assigned to its own
 * designated ground spot the line casting is applied in order to detect when the enemy hits the wall platform 
 * has reached. When the enemy game object detects the wall platfrom it is not grounded
 * then the enemy game object will be inversed flipped horizontalally to go to the oposite way, reset the position
 * and travel in an oposite way until it reaches the other end point of the platform's wall.
 * 
 * 
*/
public class EnemyMovementControl : MonoBehaviour 
{

	//The enemy layer is set as everything except unselected enemy in order to apply the flip when its appropriate.
	//setting up the enemy layermask and set its value from Unity's inspector.
	[SerializeField] private LayerMask enemyMask;

	//Declaire public variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated horizontal x axis speed of the enemy's game object's movement.
	[SerializeField] private float speed = 1;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated game object that is called enemyExplosion
	[SerializeField] GameObject enemyExplosion = null;

	//Declaire private variable
	//This variable is assigned to a designated Rigidbody2D variable.
	//Which means this is assigned as a physics component of this 2D enemy sprite 
	//component. This is assigned in order to 
	//use the physics of this game object in the scene
	private Rigidbody2D _rigidBody;

	//This variable is from Unity the transform component 
	//The Transform is defined in Unity as position, rotation and scale
	//Here it is going to be used for its coordinates, it is going to be used for the position.
	private Transform _transform;

	//Set up the scale of enemy game object's width
	private float _width;
	//0.60

	//Set up the scale of enemy game object's height
	private float _height;
	//0.60

	//Declaire private variable
	//This is declaration of the collection/array of the audio sounds
	private AudioSource[] _audioSources;

	//Declaire private variable
	//This is declaration of audio source for enemy's explosion sound
	private AudioSource _enemyExplosionSound;

	//Declaire private variable
	//This is declaration of audio source for the enemy's sound
	private AudioSource _enemySound;

	//Declaire private variable
	//This variable is assigned to a designated boolean
	//varibale in order to work as a flag and to check
	//if this flag is true in order to trigger the appropriate functions when it is approriate.
	private bool _isDead = false;

	//This is declaration of the monster control for its monster control script
	private DwarfController _dwarfCtrl;


	// Use this for initialization
	void Start () 
	{

		//previously used
		//_enemyExplosionSound = gameObject.GetComponent<AudioSource> ();

		//Here the set up the collections/components of the Audio Source. 
		//This Audio Source Components is accessed from this specific game object
		//which this script is attached to enemy game object in the scene.
		//Basically, from this game object, the Get Components is invoked which allows to 
		//access the Audio Source.
		//This set up, so the specific methods could be applied in order to control 
		//this game object's Audio Sources and invoke Play method when appropriate.
		this._audioSources = gameObject.GetComponents<AudioSource> ();

		//building the collection/array of sound souces in the specific order of the specific sound source
		//This is declaration for Enemy's Explosion sound.
		this._enemyExplosionSound = this._audioSources [0];

		//building the collection/array of sound souces in the specific order of the specific sound source
		//This is declaration for the Enemy's sound
		this._enemySound = this._audioSources [1];


		//Here the set up of the Rigidbody2D component. 
		//This Rigidbody2D Component is accessed from this specific game object
		//which the script is attached to the enemy game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Rigidbody2D that allows to access the physics part of this game object.
		//This is set up, so the specific methods could be applied to control 
		//this game object's physics and invoke such as velocity method when appropriate.
		//Now that the Rigidbody is also glabally declaired, this means that Rigidbody 
		//could be used in update function and apply a designated force on 
		//to this game object and move the game object.
		_rigidBody = gameObject.GetComponent<Rigidbody2D> ();

		//Here the set up of the Transform. 
		//The Transform is accessed from this specific game object
		//which the script is attached to which is enemy game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Transform which is scale, rotation and its position.
		//This is set up so the specific methods could be applied to control 
		//this game object's position in the scene.
		_transform = gameObject.GetComponent<Transform> ();

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
		

		//First to draw the line, start with the starting position of the enemy which
		//would be equals to the enemy game object position which is casted as Vector 2
		//and it is subtracted to move to the right position in front of the enemy game object and multiply
		//with the sprite's width and subtract the right position with the multiple of 0.2f to extend the line a bit at
		//the front of the enemy sprite game object inorder to detect the wall in front of this sprite when it moves on the ground.
		//This is to track if the enemy game object is grounded as it goes towards the wall of the platfrom this function 
		//would be triggered to reverse this enemy game object movement in order for it to go in an oposite way.
		Vector2 LineCastPos = (Vector2)gameObject.transform.position -
			(Vector2)gameObject.transform.right * _width - (Vector2)gameObject.transform.right*speed*0.2f;


		//The Debug would helps to see how this line is drawn in order to adjust it if needs to be.
		//This function takes the starting line which is the lineCastPos that is stated above
		//and the end would be adding the enemy Vector2 left multiple of its width value.
		Debug.DrawLine (LineCastPos, LineCastPos + (Vector2.left*_width));

		//Now is Grounded will be set and calculate if this enemy game object is grounded. Whether 
		//this enemy game object is grounded the value for it is calculated by using the Physics2D
		//and the start of the linecast which is the LineCastPos variable and the end value which is 
		//LineCastPos + Vector2.left*_width and the third parameter is enemymask the reason why enemy mask is there
		//this way the line will not get the enemy game object on to its account it will only solely 
		//detect the platform collider2D and if the enemy gameobject if it is grounded or not
		bool isGrounded = Physics2D.Linecast (LineCastPos, LineCastPos + (Vector2.left*_width), enemyMask);

		//Check to see if the enemy game object is grounded
		//from the Physics 2D above function
		//If it is grounded then this statement gets invoked which rotates this enemy
		if (isGrounded) 
		{
			//if it is true and enemy game object is grounded the linecast detected the wall platform then this
			//function get invoked.
			//So, if this enemy game object is grounded, this game object should be switched to go to the oposite way.
			//The rotate is used because the line would be rotated along with the enemy game object, if flip horizontally is applied
			//like previously -1, 1, 1 the line cast would not stay the same. So that is why the whole game object
			//should be rotated in the eulerAngles in its Rigid body component is applied.
			Vector3 currentRotation = _transform.eulerAngles;

			//Then rotate the enemy game object along with the linecast and the whole thing will be applied
			//and rotate it around in the y axis in 180 degrees which mean turn it around
			currentRotation.y += 180;

			//update the position of enemy game object
			_transform.eulerAngles = currentRotation;
		}

		//Setting up the velocity of this enemy game object from its Rigidbody component
		//In order to move this enemy game object the rigidbody velocity is used.
		//This stores the current velocity of this enemy game object from its rigidbody
		Vector2 vel = _rigidBody.velocity;

		//Then apply the indicated speed to this enemy game object in horizontal axis
		//This is the set up the velocity of its designated predifined strength speed in horizontal axis.
		vel.x = -_transform.right.x * speed;

		//Update the enemy gameobject's speed
		//set it back to the enemy game object's body.
		_rigidBody.velocity = vel;

		//Checking to see if this enemy game object is dead.
		//If it is dead then this statement is getting executed,
		//and this enemy game object is getting destroyed.
		if (_isDead) 
		{


			if (this._enemyExplosionSound.isPlaying) 
			{

			} 
			else 
			{
				//Destroy the enemy game object from the scene.
				Destroy (this.gameObject);
			}
		}
	}

	//This is the function that would detect the collision with
	//this enemy game object. When Unity calculates that the enemy object
	//has intercepted with a different game object this function would be executed.
	//This is the fact that when the enemy object has intercepted with another
	//game object it triggered (intercepted) with each other.
	//This function is executed and the following conditions are invoked.
	public void OnTriggerEnter2D(Collider2D other)
	{

		//When the tags are created onto specific game objects, this is where
		//the tags are taken onto the advantage.
		//When the game object's tag is equal to 'Player' tag and 
		//when the collision happends, the interception with the another game object happends
		//this function is triggered and the condition statements 
		//are invoked which compares the game object's tags.
		//When the game object's tag is equal to 'Player' tag
		if (other.gameObject.tag.Equals ("Player")) 
		{

			//If it is true, then access the other game object in its tag name defined as 'Player'. 
			//Then get the other game object's component script under name DwarfController script 
			//and store it under variable _dwarfCtl
			_dwarfCtrl = other.gameObject.GetComponent<DwarfController> ();

			//If the dwarfCtrl component is assigned and is not null
			//then execute the stamement
			if (_dwarfCtrl != null) 
			{

				//if it is true and the DwarfController script is assighed and not null,
				//then in the dwarf controller script that stored in dwarfCtrl
				//variable if the Dwarf is not dead and isDwarfDead boolean is equals to false 
				//meaning Dwarf is not dead, then execute this statement
				if (!_dwarfCtrl.isDwarfDead ()) 
				{

					//If enemy sound is assigned and is not null, then execute this statement
					if (this._enemySound != null) 
					{
						
						//If the enemy game object has enemySound Audio Source component then 
						//play its attached audio clip
						this._enemySound.Play ();
					}
				}
			}



		}

		//if the collision happened with the fire ball game object and its tag is equals to the fireball tag
		if (other.gameObject.tag.Equals ("fireBall")) 
		{
			
			//If it is true then, Enemy's Explosion Sound is checked if it assigned and is not null, then execute this statement
			if (this._enemyExplosionSound != null) 
			{

				//If the enemy game object has enemyExplosionSound Audio Source component then 
				//play its attached audio clip
				this._enemyExplosionSound.Play ();
			}

			//If this is true and the collision happened with the fireball tag
			//then instantiate/create the enemy explosion game object on to the scene 
			//on to the fireball transform game object's position in the scene.
			Instantiate (enemyExplosion).GetComponent<Transform> ().position =
				other.gameObject.GetComponent<Transform> ().position;

			//previously used
			//-----------------------------------------------------------------------------------
			//once the collision has happened and the creation of the enemy explosion was created
			//destroy/remove the fire ball game object from the scene
			//Destroy (other.gameObject);

			//Once the collision has happened and the fire ball game object was removed from the scene then
			//destroy/remove the enemy game object from the scene
			//Destroy (this.gameObject);
			//-----------------------------------------------------------------------------------


			//Once the collision has happened between the enemy and with the fire ball game object in the scene then
			//reset the enemy game object's boolean variable isDead to true.
			_isDead= true;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The MonsterController.cs script
 * Alisa Ordina
 * id: 100967526
 * November 25, 2017
 * 
 * This following script component is attached to the Monster game object in the scene.
 * This following code allows to access the Monster game object and its components in order
 * to apply an appropriate movement behaviour. The layer mask would be used not really
 * for the colision but more for the line casting similar that was coded in DwarfController script.
 * So, creating a mask based on the layer this could be used for line casting and applying
 * appropriate movements on the ground. Becuase this Monster game object is assigned to its own
 * designated ground spot the line casting is applied in order to detect when the Monster hits the wall platform 
 * has reached. When the Monster game object detects a wall platfrom it is not grounded
 * then the Monster game object will be inversed flipped horizontalally to go to the oposite way, reset the position
 * and travel in an oposite way until it reaches the other end point of the platform's wall.
 * 
*/

public class MonsterController : MonoBehaviour 
{

	//The Monster layer is set as everything except unselected Monster in order to apply the flip when its appropriate.
	//setting up the Monster layermask and set its value from Unity's inspector.
	[SerializeField] private LayerMask monsterMask;

	//Declaire public variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated horizontal x axis speed of the Monster's game object's movement.
	[SerializeField] private float speed = 1;

	//This is declaration of the Dwarf Controller for its Dwarf Controller script
	private DwarfController _dwarfCtrl;

	//This is declaration of the Monster for its Monster class as stored in Life variable.
	private Monster _life;

	//Declaire private variable
	//This variable is assigned to a designated Rigidbody2D variable.
	//Which means this is assigned as a physics component of this 2D Monster sprite 
	//component. This is assigned in order to 
	//use the physics of this game object in the scene
	private Rigidbody2D _rigidBody;

	//This variable is from Unity the transform component 
	//The Trasform is defined in Unity as position, rotation and scale
	//Here it is going to be used for its coordinates, it is going to be used for the position.
	private Transform _transform;

	//set up the scale of the Monster game object's width
	private float _width;

	//set up the scale of Monster game object's height
	private float _height;

	//Declaire private variable
	//This is declaration of the collection/array of the audio sounds
	private AudioSource[] _audioSources;

	//Declaire private variable
	//This is declaration of audio source for Monster's passed Out Sound.
	private AudioSource _passedOutSound;

	//Declaire private variable
	//This is declaration of audio source for the Monster's sound
	private AudioSource _monsterSound;

	//Declaire private variable
	//This variable is assigned to a designated boolean
	//varibale in order to work as a flag and to check
	//if this flag is true in order to trigger the appropriate functions when it is approriate.
	private bool _isDead = false;

	//Declaire private variable
	//This variable is assigned to a designated Animator variable.
	//Which means this is Animator interface is assigned in order to 
	//control mecanim and its animation system.
	private Animator _animator;

	//Declaire private variable
	//an integer is declaired and is applyed when the Monster game object gets hit.
	private int _imune;

	// Use this for initialization
	void Start () 
	{

		//This establishes the link between 
		//the MonsterController class and between
		//the Monster class. This way the MonsterController class could access 
		//the Monster's public property life counter variable. 
		//This only works because the Monster class has access to the MonsterController 
		//class that is noted in the Monster script code.
		//Without that this would not work.
		//Then the Monster class has to be intantiated in order for the
		//MonsterController class to communicate the life counter variable.
		_life = new Monster (this);

		//Initialize the imune integer to value of 10.
		_imune = 10;

		//Here the set up of the Animator component. 
		//This Animator Component is accessed from this specific game object
		//which the script is attached to the Monster game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Animator and its parameters when appropriate to set up its animation when 
		//the specific state is invoked.
		//This is set up, so the specific methods could be applied and to control 
		//this game object's Animator and invoke its state methods when appropriate.
		_animator = gameObject.GetComponent<Animator> ();

		//Here the set up the collections/components of the Audio Source. 
		//This Audio Source Components is accessed from this specific game object
		//which this script is attached to Monster game object in the scene.
		//Basically, from this game object, the Get Components is invoked which allows to 
		//access the Audio Source.
		//This set up, so the specific methods could be applied in order to control 
		//this game object's Audio Sources and invoke Play method when appropriate.
		this._audioSources = gameObject.GetComponents<AudioSource> ();

		//Building the collection/array of the sound souces in the specific order of the specific sound source
		//This is declaration for Monster's passedout sound, passedOutSound variable.
		this._passedOutSound = this._audioSources [0];

		//Building the collection/array of sound souces in the specific order of the specific sound source
		//This is declaration for the Monster's sound
		this._monsterSound = this._audioSources [1];

		//Here the set up of the Rigidbody2D component. 
		//This Rigidbody2D Component is accessed from this specific game object
		//which the script is attached to the Monster game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Rigidbody2D that allows to access the physics part of this game object.
		//This is set up, so the specific methods could be applied to control 
		//this game object's physics and invoke such as velocity method when appropriate.
		//Now that the Rigidody is also glabbaly declaired, this means that Rigidbody 
		//could be used in update function and apply a designated force on to it and move the game object.
		_rigidBody = gameObject.GetComponent<Rigidbody2D> ();

		//Here the set up of the Tranform. 
		//The Transform is accessed from this specific game object
		//which the script is attached to which is Monster game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Transform which is scale, rotation and its position.
		//This is set up so the specific methods could be applied to control 
		//this game object's position in the scene.
		_transform = gameObject.GetComponent<Transform> ();

		//Accessing the Monster game object's sprite renderer
		//This is used later to access the sprite's size and access the sprite boundary size in order to 
		//draw the ray line appropriatly.
		SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer> ();

		//set up the Monster's sprite width size
		//from the reference from the sprite rendeder component
		_width = spriteR.bounds.extents.x;

		//set up the Monster sprite height size
		//from the reference from the sprite rendeder component
		_height = spriteR.bounds.extents.y;

	}

	// Update is called once per frame
	void FixedUpdate () 
	{


		//Checks if the boolean flag isDead is equals to false in order to trigger this statement.
		//if this statement is not true and the Monster is dead and boolean flag _isDead is true then this statement does not get invoked
		//and the movements to the Monster game object cannot be applied to this Monster game object in the scene.
		if(!_isDead)
		{

			//Checks to see if the imune counter integer is greater than 0. 
			if (_imune > 0) 
			{
				_imune--;
			}
		
			//First to draw the line, start with the starting position of the Monster which
			//would be equals to the Monster game object position which is casted as Vector 2
			//and it is subtracted to move to the right position in front of the enemy game object and multiply
			//with the sprite's width and subtract the right position with the multiple of 0.2f to extend the line a bit at
			//the front of the Monster sprite game object in order to detect the wall in front of this sprite when it moves on the ground.
			//This is to track if the Monster game object is grounded as it goes towards the wall of the platfrom this function 
			//would be triggered to reverse this Monster game object movement in order for it to go in an oposite way.
			Vector2 LineCastPos = (Vector2)gameObject.transform.position -
				(Vector2)gameObject.transform.right * _width - (Vector2)gameObject.transform.right*speed*0.2f;


			//The Debug would helps to see how this line is drawn in order to adjust it if needs to be.
			//This function takes the starting line which is the lineCastPos that is stated above
			//and the end would be adding the Monster Vector2 left multiple of its width value.
			Debug.DrawLine (LineCastPos, LineCastPos + (Vector2.left*_width));

			//Now is Grounded will be set and calculate if this Monster game object is grounded. Whether 
			//this enemy game object is grounded the value for it is calculated by using the Physics2D
			//and the start of the linecast which is the LineCastPos variable and the end value which is 
			//LineCastPos + Vector2.left*_width and the third parameter is the monsterMask the reason why the Monster mask is there
			//this way the line will not get the Monster game object on to its account it will only solely 
			//detect the platform collider2D and if the Monster gameobject if it is grounded or not
			bool isGrounded = Physics2D.Linecast (LineCastPos, LineCastPos + (Vector2.left*_width), monsterMask);


		
		//Check to see if the Monster game object is grounded has hit another platform collider
		//if it did hit another 2D collider this statement is getting triggered.
		if (isGrounded) 
		{
				//if it is true and Monster game object is grounded the linecast detected the wall platform then this
				//function get invoked.
				//So, if this Monster game object is grounded, this game object should be switched to go to the oposite way.
				//The rotate is used because the line would be rotated along with the Monster game object, if flip horizontally is applied
				//like previously -1, 1, 1 the line cast would not stay the same. So that is why the whole game object
				//should be rotated in the eulerAngles in its Rigid body component is applied.
				Vector3 currentRotation = _transform.eulerAngles;

				//then rotate the Monster game object along with the linecast and the whole thing will be applied
				//and rotate it around in the y axis in 180 degrees which mean turn it around.
				currentRotation.y += 180;

				//update the position of Monster game object
				_transform.eulerAngles = currentRotation;
		}

			//Setting up the velocity of this Monster game object from its Rigidbody component
			//In order to move this Monster game object the rigidbody velocity is used.
			//This is set up to know/to store the current velocity of this Monster game object from its rigidbody.
			//This stores the current velocity of this Monster game object from its rigidbody
			Vector2 vel = _rigidBody.velocity;



		/*
		//Was used previously
		//--------------------------------------------------------------------------
		 if (isDead) 
		{
			

			//else 
			//{
				//Destroy (this.gameObject);
			//}
			_animator.SetBool ("passedOut", true);
		
		} 
		//--------------------------------------------------------------------------
		*/

			
			//Then apply the indicated speed to this Monster game object in horizontal axis
			//This is the set up the velocity of its designated predifined strength speed in horizontal axis.
			vel.x = -_transform.right.x * speed;

			//update the Monster game object's speed
			//set it back to the Monster game object's body.
			_rigidBody.velocity = vel;
		
			//The reset the attack paramenter bool is set to false.
			//In order to stop the attack animation
			_animator.SetBool ("attack", false);
		}
	}

	//This is the function that would detect the collision with
	//this Monster game object. When Unity calculates that the Monster object
	//has intercepted with a different game object this function would be executed.
	//This is the fact that when the Monster object has intercepted with another
	//game object it triggered (intercepted) with each other.
	//This function is executed and the following conditions is invoked.
	public void OnTriggerEnter2D(Collider2D other)
	{

		//Checks if the boolean flag isDead is equals to false in order to trigger this statement.
		//if this statement is not true and the Monster is dead and boolean flag _isDead is true then this statement does not get invoked
		//and the movements to the Monster game object cannot be applied to this Monster game object in the scene.
		if (!_isDead) 
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

						//Setting up the boolean isAttacking value to true
						bool isAttacking = true;

						//Trigger the Monster's attack when the Monster game object has collided
						//with the Dwarf game object's tag 'Player'. Then set Monster's
						//animator boolean attack to true to trigger the Monster's attack animation
						_animator.SetBool ("attack", true);

						//If the Monster game object is attacking and the condition is true then trigger this statement
						if (isAttacking)
						{
							//Check to see if the Monster's audio source component is assigned and not equals to null
							//This statement gets invoked
							if (_monsterSound != null) 
							{
								//If it is true and this audio is assigned 
								//and has monsterSound Audio Source component then 
								//play its attached audio clip
								_monsterSound.Play ();

								//Reset the boolean isAttacking variable to false
								isAttacking = false;
							}
						}
					}
				}

		


			}
		}

			


		//if the collision happened with the fire ball game object and its tag is equals to the fireball tag
		if (other.gameObject.tag.Equals ("fireBall")) 
		{

			//Then destroy the FireBall game object when it is collided with the Monster Collider2D
			Destroy (other.gameObject);

			//Check to see if the Monster is dead
			if (_isDead) 
			{
				
			}

			//if the _imune integer counter value is 0 
			else if (_imune <= 0 )
			{
				//If it is true and the imune is 0 then trigger the Monster's Hurt function
				//that reduces the Monster's life counter
				_life.Hurt();

				//If the Monster's passedOutSound audio source is assigned and not equals to null
				//then trigger this statement
				if (this._passedOutSound!=null) 
				{
					//If it is true and this audio is assigned 
					//and has passedOutSound Audio Source component then 
					//play its attached audio clip
					this._passedOutSound.Play();

				} 

				//Resets the imunevalue to 10.
				_imune = 10;
			}
	

		}
	}

	//Is a public function that returns whether the Monster is dead or not.
	public bool isPassed()
	{

		//return the isDead boolean value
		return _isDead;
	}

	public void PassedOut()
	{
		//Sets the isDead to true and later sets the animation passedout to true later.
		_isDead = true;

		//Sets up the passedOut animator's boolean parametor passedOut to true in order
		//to trigger its animation of the Monter's passed out animation in the scene.
		_animator.SetBool ("passedOut", true);
	}
}

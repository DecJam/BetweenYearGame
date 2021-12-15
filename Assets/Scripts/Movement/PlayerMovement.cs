using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script managing player movements
/// </summary>
public class PlayerMovement : MonoBehaviour
{
	private CharacterController m_CharacterController = null;

	[Header("Jumping")]
	[SerializeField,Range(0,50)] private int m_JumpHeight = 1;				// The players jump height
	[SerializeField] private int m_JumpAmount = 1;							// The amount of times the player can jump
	[SerializeField] private LayerMask m_GroundLayer;						// The layer that is to be used as the ground
	[SerializeField,Range(-10,10)] private float m_GravityMultiplier = 1;	// A multiplier for gravity
	private float m_GroundCheckDistance = 2;	// The height the player has to be under to be "Grounded"
	private float m_Gravity = 9.81f;			// The value used for grvity
	private int m_JumpsLeft = 0;				// The remaining jumps after the player has jumped
	private bool IsGrounded = true;				// If the player is currently grounded
	private bool WasGrounded = false;           // If the player was grounded before the action

	private bool m_JumpPressed = false;         // If the "Jump" button has been pressed
	public bool JumpPressed { get { return m_JumpPressed; } set { m_JumpPressed = value; } } // Getter and Setter for if "Jummp" has been pressed

	[Header("Movement")]
	[SerializeField, Range(0, 20)] private float m_MovementSpeed = 1;		// The players movement speed in M/s
		
	private Vector2 m_MousePosition;			// The mouse position
	public Vector2 MousePosition { get { return m_MousePosition; } set { m_MousePosition = value; } } // Getter and Setter for the mouses position

	[SerializeField] private Vector3 m_Velocity; // The velocity of the player

	private Vector3 m_MovementInput;			// A vector2 representing the movement values
	public Vector3 MovementInput { get {return m_MovementInput;} set {m_MovementInput = value;} } // Getter and Setter for the movement input

	/// <summary>
	/// Called on script being loaded,
	/// Initializes variables to defaults
	/// </summary>
	private void Awake()
	{
		m_CharacterController = Player.Instance.characterController;
		m_JumpsLeft = m_JumpAmount;
		m_Velocity = Vector3.zero;
	}

	/// <summary>
	/// Called every frame,
	/// Updates things every frame
	/// </summary>
	private void LateUpdate()
	{
		// Raycasts to see if player is grounded
		Ray groundRay = new Ray(transform.position, -Vector3.up);
		RaycastHit hit;
		IsGrounded = Physics.Raycast(groundRay, out hit, m_GroundCheckDistance, m_GroundLayer);
		
		// Player has just landed
		if (!WasGrounded && IsGrounded && Mathf.Abs(m_Velocity.y) < 1)
		{
			OnGrounded();
		}

		// Applies gravity
		m_Velocity.y += (-m_Gravity * m_GravityMultiplier) * Time.deltaTime;

		// Sets Y velocity to 0 if grounded
		if (IsGrounded && m_Velocity.y < 0)
		{
			m_Velocity.y = 0;
		}

		// Moves the player and looks at mouse
		m_CharacterController.Move(m_Velocity * Time.deltaTime);
		LookAtMouse();

		// Resets jump
		m_JumpPressed = false;
	}

	/// <summary>
	/// Called when the player becomes grounded
	/// Updates variables
	/// </summary>
	private void OnGrounded()
	{
		m_JumpsLeft = m_JumpAmount;
		WasGrounded = true;
	}

	/// <summary>
	/// Called when the "Jump" buttion is pressed,
	/// Performs a jump
	/// </summary>
	public void Jump()
	{
		if (m_JumpsLeft > 0)
		{
			m_Velocity.y = Mathf.Sqrt(m_JumpHeight * 2f * m_Gravity);
			m_JumpsLeft--;
			WasGrounded = false;
		}
	}

	/// <summary>
	/// Called while one of the movement buttons have been pressed,
	/// Updates the velocity with the movement
	/// </summary>
	/// <param name="movementInput"></param>
	public void Movement(Vector2 movementInput)
	{
		m_Velocity.x = movementInput.x * m_MovementSpeed;
		m_Velocity.z = movementInput.y * m_MovementSpeed;
	}

	/// <summary>
	/// Called every Update,
	/// Makes the player rotate to look at the mouse
	/// </summary>
	private void LookAtMouse()
	{
		Ray cameraRay;
		RaycastHit cameraRayHit;
		cameraRay = Camera.main.ScreenPointToRay(m_MousePosition);
		
		// Performs a raycast to find the position on the ground the mouse is currently
		if(Physics.Raycast(cameraRay, out cameraRayHit))
		{
			Vector3 targetPos = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
			Player.Instance.characterController.transform.LookAt(targetPos);
		}
	}
}

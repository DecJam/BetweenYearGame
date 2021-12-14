using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Jumping")]
	[SerializeField] private int m_JumpHeight = 0;
	[SerializeField] private float m_GroundCheckDistance = 2;
	[SerializeField] private int m_JumpAmount = 1;
	[SerializeField] private int m_JumpsLeft = 0;
	[SerializeField] private LayerMask m_GroundLayer;
	private CharacterController m_CharacterController = null;

	[SerializeField] private bool IsGrounded = true;
	[SerializeField] private bool WasGrounded = false;

	private Vector3 m_MousePosition;
	public Vector3 MousePosition { get { return m_MousePosition; } set { m_MousePosition = value; } }

	[SerializeField] private float m_Gravity = -9.81f;
	[SerializeField,Range(1,10)] private float m_GravityMultiplier = 1;
	[SerializeField,Range(0, 20)] private float m_MovementSpeed = 1;

	[SerializeField] private Vector3 m_Velocity;

	private Vector3 m_MovementVector;

	private bool m_JumpPressed = false;
	public bool JumpPressed {get { return m_JumpPressed;} set {m_JumpPressed = value;} }

	private Vector3 m_MovementInput;
	public Vector3 MovementInput { get {return m_MovementInput;} set {m_MovementInput = value;} }

	private void Awake()
	{
		m_CharacterController = Player.Instance.characterController;
		m_JumpsLeft = m_JumpAmount;
		m_MovementVector = Vector3.zero;
		m_Velocity = Vector3.zero;
	}

	private void LateUpdate()
	{
		Ray groundRay = new Ray(transform.position, -Vector3.up);
		RaycastHit hit;
		IsGrounded = Physics.Raycast(groundRay, out hit, m_GroundCheckDistance, m_GroundLayer);
		
		//
		if (IsGrounded && m_Velocity.y < 0)
		{
			m_Velocity.y = 0;
		}

		if (!WasGrounded && IsGrounded && Mathf.Abs(m_Velocity.y) < 1)
		{
			OnGrounded();
		}

		m_JumpPressed = false;
		m_Velocity.y += (-m_Gravity * m_GravityMultiplier) * Time.deltaTime;
		m_CharacterController.Move(m_Velocity * Time.deltaTime);
		LookAtMouse();
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

	public void Jump()
	{
		if (m_JumpsLeft > 0)
		{
			m_Velocity.y = Mathf.Sqrt(m_JumpHeight * 2f * m_Gravity);
			m_JumpsLeft--;
			WasGrounded = false;
		}
	}

	public void Movement(Vector2 movementInput)
	{
		m_Velocity.x = movementInput.x * m_MovementSpeed;
		m_Velocity.z = movementInput.y * m_MovementSpeed;
	}

	private void OnDrawGizmos()
	{
		Ray groundRay = new Ray(transform.position, -Vector3.up);
		RaycastHit hit;
		if (Physics.Raycast(groundRay, out hit, m_GroundCheckDistance, m_GroundLayer))
		{
			// Line from hoverpoint to hit and draws a sphere
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(transform.position, hit.point);
		}
	}
	public void UpdateMousePos(Vector2 mousePos)
	{
		m_MousePosition = Camera.main.ScreenToWorldPoint(mousePos);
	} 
	private void LookAtMouse()
	{
		Vector3 dir = m_MousePosition - Player.Instance.characterController.transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Player.Instance.characterController.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		Player.Instance.characterController.transform.rotation = Quaternion.Euler(new Vector3(0.0f, angle, 0.0f));
	}
}

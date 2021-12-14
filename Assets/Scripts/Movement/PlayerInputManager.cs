using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
	PlayerInputActions m_PlayerInput = null;
	Player m_Player = null;

	private void Awake()
	{
		m_PlayerInput = new PlayerInputActions();

		m_PlayerInput.Player.Jump.started += ctx => OnJumpPressed(ctx);

		m_PlayerInput.Player.Movement.performed += ctx => OnMovementPress(ctx);
		m_PlayerInput.Player.Movement.canceled += ctx => OnMovementPress(ctx);
	}

	private void Start()
	{
		m_Player = Player.Instance;
		
	}

	/// <summary>
	/// Called when the "jump" button is pressed,
	/// Changes the jump value
	/// </summary>
	/// <param name="ctx">The context of the input</param>
	private void OnJumpPressed(InputAction.CallbackContext ctx)
	{
		m_Player.Movement.Jump();
		Debug.Log("Player Input: Jump pressed");
	}

	/// <summary>
	/// Called when a "movement" key is pressed and released,
	/// Changes the movement vector
	/// </summary>
	/// <param name="ctx">The context of the input</param>
	private void OnMovementPress(InputAction.CallbackContext ctx)
	{
		m_Player.Movement.Movement(ctx.ReadValue<Vector2>());
		Debug.Log("Player Input: Movement pressed" + ctx.ReadValue<Vector2>());
	}

	private void Update()
	{
		m_Player.Movement.UpdateMousePos(m_PlayerInput.Player.MousePosition.ReadValue<Vector2>());
	}

	private void OnEnable()
	{
		m_PlayerInput.Enable();
	}

	private void OnDisable()
	{
		m_PlayerInput.Disable();
	}
}



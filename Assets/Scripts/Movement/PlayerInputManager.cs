using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
	PlayerInputActions m_PlayerInput = null;	// The input map being used
	Player m_Player = null;						// A reference to the player script

	/// <summary>
	/// Called when the script is being loaded,
	/// Assigns functions to the input maps actions
	/// </summary>
	private void Awake()
	{
		m_PlayerInput = new PlayerInputActions();

		//m_PlayerInput.Player.Jump.started += ctx => OnJumpPressed(ctx);

		m_PlayerInput.Player.Movement.performed += ctx => OnMovementPress(ctx);
		m_PlayerInput.Player.Movement.canceled += ctx => OnMovementPress(ctx);

		m_PlayerInput.Player.ReturnToPreviousUI.performed += ctx => OnPreviousUIPress(ctx);
	}

	/// <summary>
	/// Called before first frame
	/// Initializes variables
	/// </summary>
	private void Start()
	{
		m_Player = Player.Instance;	
	}

	/// <summary>
	/// Called when the "jump" button is pressed,
	/// Changes the jump value
	/// </summary>
	/// <param name="ctx">The context of the input</param>
	//private void OnJumpPressed(InputAction.CallbackContext ctx)
	//{
	//	m_Player.Movement.Jump();
	//	Debug.Log("Player Input: Jump pressed");
	//}

	/// <summary>
	/// Called when a "movement" key is pressed and released,
	/// Changes the movement vector
	/// </summary>
	/// <param name="ctx">The context of the input</param>
	private void OnMovementPress(InputAction.CallbackContext ctx)
	{
		m_Player.Movement.Movement(ctx.ReadValue<Vector2>());
	}

	private void OnPreviousUIPress(InputAction.CallbackContext ctx)
	{
		UIManager.Instance.UIController.ReturnToPreviousUI();
	}



	/// <summary>
	/// Called every frame,
	/// Updates the mouse position
	/// </summary>
	private void Update()
	{
		m_Player.Movement.MousePosition = m_PlayerInput.Player.MousePosition.ReadValue<Vector2>();
		m_Player.m_Weapon.GetComponent<Gun>().FirePressed = m_PlayerInput.Player.FireWeapon.ReadValue<float>() > 0; 
	}

	/// <summary>
	/// Called when the script is active
	/// Enables the input system
	/// </summary>
	private void OnEnable()
	{
		m_PlayerInput.Enable();
	}

	/// <summary>
	/// Called when the script is deactivated
	/// Disables the input system
	/// </summary>
	private void OnDisable()
	{
		m_PlayerInput.Disable();
	}
}



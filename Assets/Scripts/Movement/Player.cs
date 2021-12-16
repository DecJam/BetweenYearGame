using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that manages and references all scripts relating to the player
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] public CharacterController characterController = null;  // The player's rigidbody

    [SerializeField] public GameObject m_Weapon = null;

    [Header("Attached Scripts")]
	[SerializeField] public PlayerMovement Movement = null;                 // The player's movement script
    [SerializeField] public PlayerInputManager Input = null;                // The player's input script

    private static Player m_Instance;                                       // The current instance of the player
    public static Player Instance                                           // The public current instance of MenuController
    {
        get { return m_Instance; }
    }

    void Awake()
    {
        // Initialize Singleton
        if (m_Instance != null && m_Instance != this)
            Destroy(this.gameObject);
        else
            m_Instance = this;
    }
}
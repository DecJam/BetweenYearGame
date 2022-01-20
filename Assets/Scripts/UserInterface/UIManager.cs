using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] public UIController UIController = null;
    [SerializeField] public SettingsUI SettingsUI = null;
    private static UIManager m_Instance;                                       
    public static UIManager Instance                                           
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

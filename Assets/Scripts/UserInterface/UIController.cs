using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{ 
	public enum UIState
	{
		MainMenu,
		Game,
		Paused,
		EndScreen,
		Settings,
		Credits
	}

	[SerializeField] GameObject m_MainMenuUI = null;
	[SerializeField] GameObject m_GameUI = null;
	[SerializeField] GameObject m_PausedUI = null;
	[SerializeField] GameObject m_EndScreenUI = null;
	[SerializeField] GameObject m_SettingsUI = null;
	[SerializeField] GameObject m_CreditsUI = null;

	private UIState m_State;
	private GameObject m_CurrentUI;

	private Stack<UIState> m_UIStack;

	private void Awake()
	{
		m_UIStack = new Stack<UIState>();
		m_UIStack.Push(UIState.MainMenu);
		m_CurrentUI = m_MainMenuUI;
	}


	/// <summary>
	/// Using the stack returns to the previous UI
	/// </summary>
	public void ReturnToPreviousUI()
	{
		if (m_State == UIState.Game)
		{
			LoadPausedUI();
		}

		else if(m_UIStack.Count >= 2)
		{
			m_UIStack.Pop();
			OnExitPreviousUI();
			m_State = m_UIStack.Peek();
			UpdateUI();
		}
	}

	public void LoadMainMenu()
	{
		m_UIStack.Clear();
		m_UIStack.Push(UIState.MainMenu);
		OnExitPreviousUI();
		UpdateUI();
	}

	public void LoadGameUI()
	{
		m_UIStack.Clear();
		m_UIStack.Push(UIState.Game);
		OnExitPreviousUI();
		UpdateUI();
	}

	public void LoadPausedUI()
	{
		m_UIStack.Push(UIState.Paused);
		OnExitPreviousUI();
		UpdateUI();
	}

	public void LoadEndScreenUI()
	{
		m_UIStack.Clear();
		m_UIStack.Push(UIState.EndScreen);
		OnExitPreviousUI();
		UpdateUI();
	}

	public void LoadSettingsUI()
	{
		m_UIStack.Push(UIState.Settings);
		OnExitPreviousUI();
		UpdateUI();
	}

	public void LoadCredits()
	{
		m_UIStack.Push(UIState.Credits);
		OnExitPreviousUI();
		UpdateUI();
	}

	private void OnExitPreviousUI()
	{

	}

	public void ExitToDesktop()
	{
		Application.Quit();
	}

	private void UpdateUI()
	{
		m_CurrentUI.SetActive(false);
		m_State = m_UIStack.Peek();

		switch (m_State)
		{
			case UIState.MainMenu:
				m_CurrentUI = m_MainMenuUI;
				Time.timeScale = 0;
				break;

			case UIState.Game:
				m_CurrentUI = m_GameUI;
				Time.timeScale = 1;
				break;

			case UIState.Paused:
				m_CurrentUI = m_PausedUI;
				Time.timeScale = 0;
				break;

			case UIState.EndScreen:
				m_CurrentUI = m_EndScreenUI;
				Time.timeScale = 0;
				break;

			case UIState.Settings:
				m_CurrentUI = m_SettingsUI;
				Time.timeScale = 0;
				break;


			case UIState.Credits:
				m_CurrentUI = m_CreditsUI;
				Time.timeScale = 0;
				break;
		}

		m_CurrentUI.SetActive(true);	

	}
}

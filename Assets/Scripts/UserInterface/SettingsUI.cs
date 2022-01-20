using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SettingState
{
	Sound,
	Video,
	Controls
}

public class SettingsUI : MonoBehaviour
{
	private SettingState m_State;
	private GameObject m_CurrentUI = null;
	[SerializeField] private GameObject m_SoundUI = null;
	[SerializeField] private GameObject m_VideoUI = null;
	[SerializeField] private GameObject m_ControlsUI = null;
	[SerializeField] private Button m_SoundButton = null;
	[SerializeField] private Button m_VideoButton = null;
	[SerializeField] private Button m_ControlButton = null;

	private void Awake()
	{
		m_State = SettingState.Sound;
		m_CurrentUI = m_SoundUI;
		m_CurrentUI.SetActive(true);
	}

	public void LoadSound()
	{
		m_State = SettingState.Sound;
		OnExitPreviousState();
		UpdateState();
	}
	
	public void LoadVideo()
	{
		m_State = SettingState.Video;
		OnExitPreviousState();
		UpdateState();
	}

	public void LoadControls()
	{
		m_State = SettingState.Controls;
		OnExitPreviousState();
		UpdateState();
	}

	private void UpdateState()
	{
		m_CurrentUI.SetActive(false);

		switch (m_State)
		{
			case SettingState.Sound:
				m_CurrentUI = m_SoundUI;
				m_SoundButton.GetComponent<Image>().color = m_SoundUI.GetComponent<Image>().color;
				break;

			case SettingState.Video:
				m_CurrentUI = m_VideoUI;
				m_VideoButton.GetComponent<Image>().color = m_VideoUI.GetComponent<Image>().color;
				break;

			case SettingState.Controls:
				m_CurrentUI = m_ControlsUI;
				m_ControlButton.GetComponent<Image>().color = m_ControlsUI.GetComponent<Image>().color;
				break;
		}

		m_CurrentUI.SetActive(true);
	}

	private void OnExitPreviousState()
	{
		switch (m_State)
		{
			case SettingState.Sound:
				m_SoundButton.GetComponent<Image>().color = Color.white;
				break;

			case SettingState.Video:
				m_VideoButton.GetComponent<Image>().color = Color.white;
				break;

			case SettingState.Controls:
				m_ControlButton.GetComponent<Image>().color = Color.white;
				break;
		}

	}
}

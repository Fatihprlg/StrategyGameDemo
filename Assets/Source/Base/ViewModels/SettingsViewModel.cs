using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsViewModel : ScreenElement
{
    [SerializeField] private GameObject audioOn, audioOff;
    [SerializeField] private GameObject settingsMenu;
    private bool settingsMenuState;
    private bool soundState;
    public override void Initialize()
    {
        base.Initialize();
        soundState = AudioManager.Instance.IsAudioOn;
        SetVisuals();
    }
    
    public void SoundButtonClicked()
    {
        ToggleSound(!soundState);
    }


    public void ToggleSound(bool state)
    {
        AudioManager.Instance.SetAudioState(state);
        soundState = state;
        SetVisuals();
    }


    public void SettingsMenuTrigger(bool state)
    {
        settingsMenuState = state;
        settingsMenu.SetActive(state);
    }

    public void SettingsMenuTrigger()
    {
        SettingsMenuTrigger(!settingsMenuState);
    }

    private void SetVisuals()
    {
        audioOn?.SetActive(soundState);
        audioOff?.SetActive(!soundState);
    }

    public void ReturnButton() => SceneController.Instance.RestartScene();
}

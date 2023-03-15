using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsViewModel : ScreenElement
{
    [SerializeField] private GameObject settingsMenu;
    private bool settingsMenuState;
    public void SettingsMenuTrigger(bool state)
    {
        settingsMenuState = state;
        settingsMenu.SetActive(state);
    }

    public void SettingsMenuTrigger()
    {
        SettingsMenuTrigger(!settingsMenuState);
    }

    public void ReturnButton() => SceneController.Instance.RestartScene();
}

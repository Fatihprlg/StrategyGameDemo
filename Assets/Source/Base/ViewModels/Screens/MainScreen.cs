using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MainScreen : ScreenModel
{
    [SerializeField] private GameObject mainView;
    [SerializeField] private TMP_Dropdown teamSelectionDropdown;
    
    public override void Initialize()
    {
        base.Initialize();
        teamSelectionDropdown.value = (int)PlayerDataModel.Data.PlayerTeam;
    }

    public void NewGameButton()
    {
        mainView.SetActive(false);
        screenElements[0].SetActiveGameObject(true);
    }

    public void LoadGameButton()
    {
        mainView.SetActive(false);
        screenElements[1].SetActiveGameObject(true);
    }

    public void BackButton()
    {
        screenElements[0].SetActiveGameObject(false);
        screenElements[1].SetActiveGameObject(false);
        mainView.SetActive(true);
    }

    public void OnTeamSelected(int teamIndex)
    {
        PlayerDataModel.Data.PlayerTeam = (Teams)teamIndex;
    }
}
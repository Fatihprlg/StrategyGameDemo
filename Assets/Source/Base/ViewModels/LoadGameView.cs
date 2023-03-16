using UnityEngine;
using UnityEngine.UI;

public class LoadGameView : ScreenElement
{
    [SerializeField] private Button[] loadButtons;
    [Dependency] private GameController _gameController;
    public override void Initialize()
    {
        this.Inject();
        for (int i = 0; i < loadButtons.Length; i++)
        {
            if(LevelDataModel.Data.levelEntityDatas.Count > i)
            {
                loadButtons[i].interactable = true;
                loadButtons[i].SetText($"Map {LevelDataModel.Data.levelEntityDatas[i].levelIndex + 1}");
            }
            else
            {
                loadButtons[i].SetText("<Empty Save Slot>");
                loadButtons[i].interactable = false;
            }
        }
    }

    public void OnLoadButtonClicked(int index)
    {
        PlayerDataModel.Data.LevelIndex = LevelDataModel.Data.levelEntityDatas[index].levelIndex;
        _gameController.ChangeState(GameStates.Game);
    }
}
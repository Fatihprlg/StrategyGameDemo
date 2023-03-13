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
                loadButtons[i].onClick.RemoveAllListeners();
                loadButtons[i].onClick.AddListener(() =>
                    PlayerDataModel.Data.LevelIndex = LevelDataModel.Data.levelEntityDatas[i].levelIndex);
                loadButtons[i].onClick.AddListener(() =>_gameController.ChangeState(GameStates.Game));
                loadButtons[i].SetText($"Level {i}");
            }
            else
            {
                loadButtons[i].SetText("<Empty Save Slot>");
                loadButtons[i].interactable = false;
            }
        }
    }
}
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.Events;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelController : MonoBehaviour, IInitializable
{
    public bool initializeOnAwake;
    public int forceLevelIndex = -1;
    public LevelModel activeLevel;
    [SerializeField] private Object levels;
    [SerializeField] private MultiplePoolModel entityPools;
    [SerializeField] private Registry entityRegistry;
    [SerializeField] private UnityEvent onLevelLoaded;
    [Dependency] private CameraController _cameraController;
    [Dependency] private GridViewModel gridView;
    private SceneController _sceneController;
    private LevelList levelModels;

    private void Awake()
    {
        if (!initializeOnAwake) return;
        Init();
    }

    public void Initialize()
    {
        if (initializeOnAwake) return;
        Init();
    }

    private void Init()
    {
        this.Inject();
        EntityFactory.SetRegistry(entityRegistry);
        EntityFactory.SetEntityPools(entityPools);
#if UNITY_EDITOR
        GetLevels();
#endif
        _sceneController = SceneController.Instance;
        DeserializeLevels();
        activeLevel = null;
        LoadLevel(forceLevelIndex >= 0 ? forceLevelIndex : PlayerDataModel.Data.LevelIndex);
        _cameraController.SetGridHalfSize(activeLevel.grid.GetLength(0)/2);
    }

    private void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levelModels.list.Count) levelIndex = 0;
        PlayerDataModel.Data.Level = levelIndex + 1;
        PlayerDataModel.Data.LevelIndex = levelIndex;

        LoadLevelHelper(levelIndex);
    }
    public void ReplayLevel()
    {
        LevelEntityDataList save =LevelDataModel.Data.levelEntityDatas.FirstOrDefault(d => d.levelIndex == PlayerDataModel.Data.LevelIndex);
        if (save is not null) LevelDataModel.Data.levelEntityDatas.Remove(save);
        _sceneController.RestartScene();
    }

    #region UTILS
    private void DeserializeLevels()
    {
        levelModels = JsonHelper.LoadJson<LevelList>(levels.ToString());
    }

    private void LoadLevelHelper(int levelIndex)
    {
        if (levelModels.list.Count == 0) return;
        if (levelModels.list.Count <= levelIndex)
        {
            levelIndex = 0;
        }
        else if (levelIndex < 0)
        {
            levelIndex = levelModels.list.Count - 1;
        }

        ClearScene();
        activeLevel = levelModels.list[levelIndex];
        GridHandler.InitializeGrid(activeLevel.grid);
        var placedEntities = LevelAdapter.LoadLevel(activeLevel, gridView);
        GridHandler.PlaceEntitiesOnGrid(placedEntities.ToArray());
        onLevelLoaded?.Invoke();
    }

    private void ClearScene()
    {
        LevelAdapter.ClearScene();
        activeLevel = null;
    }
    #endregion
    
#if UNITY_EDITOR
    private void GetLevels()
    {
        Object asset = AssetDatabase.LoadAssetAtPath<Object>(Constants.Strings.LEVELS_PATH);
        levels = asset;
    }
#endif

    
}
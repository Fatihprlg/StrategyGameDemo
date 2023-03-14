using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class LevelEditor : EditorWindow
{
    [SerializeField] private MultiplePoolModel entityPools;
    [SerializeField] private Registry _registry;
    [SerializeField] private GridViewModel gridView;
    [SerializeField] private Texture2D WalkableRef;
    [SerializeField] private Texture2D NonWalkableRef;
    [SerializeField] private LevelModel activeLevel;
    
    private float fillPercent = .65f;
    private int editorLevelIndex, gridWidth = 32, gridHeight = 32, stepCount = 15, liveNeighboursRequired = 4;
    private int[,] currentAutomaton;
    private SerializedObject serializedObject;
    private GUIContent saveContent, loadContent, clearSceneContent, overrideContent, resetDataContext, cellularAutomatonContext;
    private Texture2D currentAutomatonTexture;
    private LevelList levelModels;
    private Object levels;
    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        _registry = AssetDatabase.LoadAssetAtPath<Registry>(Constants.Strings.REGISTRY_PATH);
        WalkableRef = AssetDatabase.LoadAssetAtPath<Texture2D>(Constants.Strings.WALKABLE_REF_PATH);
        NonWalkableRef = AssetDatabase.LoadAssetAtPath<Texture2D>(Constants.Strings.NON_WALKABLE_REF_PATH);

        gridView = FindObjectOfType<GridViewModel>();
        entityPools = FindObjectsOfType<MultiplePoolModel>()[0];
        GetLevels();
        saveContent = new GUIContent
        {
            text = "Save Level"
        };

        loadContent = new GUIContent
        {
            text = "Load Level"
        };

        overrideContent = new GUIContent
        {
            text = "Override Level"
        };

        clearSceneContent = new GUIContent
        {
            text = "Clear Scene"
        };

        resetDataContext = new GUIContent
        {
            text = "Reset Levels"
        };
        cellularAutomatonContext = new GUIContent
        {
            text = "Create Map With CellularAutomaton"
        };
    }

    [MenuItem("My Game Lib/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelEditor));
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(entityPools)));
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_registry)));
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(gridView)));
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(WalkableRef)));
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NonWalkableRef)));
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(activeLevel)));
        EditorGUILayout.Space(10);
        serializedObject.ApplyModifiedProperties();

        EditorUtils.DrawUILine(Color.white);
        gridWidth = EditorGUILayout.IntField("Grid Width ", gridWidth);
        gridHeight = EditorGUILayout.IntField("Grid Height ", gridHeight);
        stepCount = EditorGUILayout.IntField("Step Count ", stepCount);
        liveNeighboursRequired = EditorGUILayout.IntSlider("Live Neighbours Required ", liveNeighboursRequired, 0, 8);
        fillPercent = EditorGUILayout.Slider("Fill Percent ", fillPercent, 0, 1);
        
        if (GUILayout.Button(cellularAutomatonContext))
        {
            CreateMapWithCellularAutomaton();
        }

        if (currentAutomatonTexture)
        {
            Rect lastRect = GUILayoutUtility.GetLastRect();
            Rect automatonRect = new (position.width/4, lastRect.y + 40, position.width/2,
                position.width/2);
            Rect labelRect = automatonRect;
            labelRect.height = 20;
            labelRect.y -= 20;
            EditorGUI.PrefixLabel(labelRect, new GUIContent("Preview: "));
            EditorGUI.DrawPreviewTexture(automatonRect, currentAutomatonTexture);
            GUILayout.Space(labelRect.height + automatonRect.height);
        }
        
        EditorUtils.DrawUILine(Color.white);
        if (GUILayout.Button(saveContent))
        {
            SaveLevel();
        }
        EditorUtils.DrawUILine(Color.white);
        if (GUILayout.Button(overrideContent))
        {
            OverrideLevel();
        }
        EditorUtils.DrawUILine(Color.white);
        editorLevelIndex = EditorGUILayout.IntField("Level Index to Load", editorLevelIndex);
        if (GUILayout.Button(loadContent))
        {
            LoadLevel(editorLevelIndex);
        }
        EditorUtils.DrawUILine(Color.white);
        if (GUILayout.Button(clearSceneContent))
        {
            LevelAdapter.ClearScene();
        }
        EditorUtils.DrawUILine(Color.white);
        if (GUILayout.Button(resetDataContext))
        {
            ResetLevelsData();
        }
        EditorUtils.DrawUILine(Color.white);
        EditorGUILayout.EndVertical();
    }

    private void CreateMapWithCellularAutomaton()
    {
        currentAutomaton = CellularAutomatonCreator.CreateAutomata(gridWidth, gridHeight, fillPercent, stepCount, liveNeighboursRequired);
        int x = gridWidth;
        int y = gridHeight;
        const int cellHeightAsPixel = Constants.Numerical.CELL_HEIGHT_AS_PIXEL;
        currentAutomatonTexture = new Texture2D(gridWidth * cellHeightAsPixel,
            gridHeight * cellHeightAsPixel);
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                var pixels = currentAutomaton[i, j] == (int)CellTypes.Walkable
                    ? WalkableRef.GetPixels()
                    : NonWalkableRef.GetPixels();
                currentAutomatonTexture.SetPixels(i * cellHeightAsPixel, j * cellHeightAsPixel, cellHeightAsPixel, cellHeightAsPixel, pixels);
            }
        }
        currentAutomatonTexture.Apply();
        gridView.SetGridView(currentAutomatonTexture);
    }

    private void ResetLevelsData()
    {
        levelModels.list.Clear();
        string[] files = System.IO.Directory.GetFiles($"{Application.dataPath}{Constants.Strings.RENDERED_TEXTURES_PATH}", "*.png");
        foreach (string file in files)
        {
            System.IO.File.Delete(file);
        }
        JsonHelper.SaveJson(levelModels, Constants.Strings.LEVELS_PATH);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    
    private static void SaveAll(LevelModel lvlModel)
    {
        var poolItems = FindObjectsOfType<PoolItemModel>();
        var poolItemDatas = poolItems.Select(poolItemModel => poolItemModel.GetData()).ToList();
        lvlModel.poolItems = poolItemDatas;
    }
    
    private void SaveWorldItems(LevelModel level, string path, bool _override = false)
    {
        SaveAll(level);
        if (_override)
        {
            levelModels.list.Insert(activeLevel.index, level);
            levelModels.list.Remove(activeLevel);
        }
        else levelModels.list.Add(level);

        JsonHelper.SaveJson(levelModels, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void LoadLevel(int levelIndex)
    {
        GetLevels();
        if (levelModels == null || levelModels.list.Count == 0)
        {
            Debug.LogError($"Level models ({levelModels}) is null or empty.");
            return;
        }
        activeLevel = levelModels.list.FirstOrDefault(lv => lv.index == levelIndex);
        if (activeLevel is null)
        {
            Debug.LogError($"There is no level with given index {levelIndex}");
            return;
        }

        currentAutomaton = activeLevel.grid;
        currentAutomatonTexture = Helpers.Other.LoadTexture($"{Constants.Strings.RENDERED_TEXTURES_PATH}{activeLevel.name}");
        EntityFactory.SetRegistry(_registry);
        EntityFactory.SetEntityPools(entityPools);
        LevelAdapter.LoadLevel(activeLevel, gridView, true);
    }
    
    private void SaveLevel()
    {
        GetLevels();
        LevelModel level = new()
        {
            name = $"Level {levelModels.list.Count}",
            index = levelModels.list.Count,
            grid = currentAutomaton
        };
        Helpers.Other.SaveTexture(currentAutomatonTexture, Constants.Strings.RENDERED_TEXTURES_PATH, level.name);

        SaveWorldItems(level, Constants.Strings.LEVELS_PATH);
        GetLevels();
        LevelAdapter.ClearScene();
        Debug.Log($"{level.name} saved to path {Constants.Strings.LEVELS_PATH}");
    }

    private void GetLevels()
    {
        try
        {
            Object asset = AssetDatabase.LoadAssetAtPath<Object>(Constants.Strings.LEVELS_PATH);
            levels = asset;
            levelModels = JsonHelper.LoadJson<LevelList>(levels.ToString());
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    private void OverrideLevel()
    {
        if (levelModels == null) GetLevels();
        if(currentAutomatonTexture)
            Helpers.Other.SaveTexture(currentAutomatonTexture, Constants.Strings.RENDERED_TEXTURES_PATH, activeLevel.name);
        if (currentAutomaton is { Length: > 0 })
            activeLevel.grid = currentAutomaton;
        SaveWorldItems(activeLevel, Constants.Strings.LEVELS_PATH, true);
        Debug.Log($"{activeLevel.name} overriden to path {Constants.Strings.LEVELS_PATH}");
    }

}
using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataHandler : MonoBehaviour, IInitializable
{
    public PlayerDataModel Player;
    public LevelDataModel Level;
    private bool isInitialized;
    public void Initialize()
    {
        Player = new PlayerDataModel().Load();
        Level = new LevelDataModel().Load();
        isInitialized = true;
    }

    internal void ClearAllData()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.dat");
        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }

        PlayerPrefs.DeleteAll();

        if (Directory.GetFiles(Application.persistentDataPath, "*.dat").Length == 0)
        {
            Debug.Log("Data Clear Succeed");
        }
    }

    private void SaveDatas()
    {
        if(!isInitialized) return;
        PlayerDataModel.Data.Save();
        LevelEntityDataList datas = new ()
        {
            levelIndex = PlayerDataModel.Data.LevelIndex,
            entityDatas = GridHandler.GetAllItemsOnGrid().ToLevelEntityDataList()
        };
        LevelEntityDataList currentData = LevelDataModel.Data.levelEntityDatas.FirstOrDefault(d => d.levelIndex == datas.levelIndex);
        if (currentData is not null) LevelDataModel.Data.levelEntityDatas.Remove(currentData);
        LevelDataModel.Data.levelEntityDatas.Add(datas);
        LevelDataModel.Data.Save();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveDatas();
        }
    }

    private void OnDisable()
    {
        SaveDatas();
    }
}
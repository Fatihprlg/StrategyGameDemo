using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class DataHandlerWindow : EditorWindow
{
    private string dataModelName;
    private  GUIContent resetContent, createContent;
    [MenuItem("My Game Lib/DataHandler")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(DataHandlerWindow));
    }

    private void OnEnable()
    {
        createContent = new GUIContent
        {
            text = "Create New DataModel"
        };

        resetContent = new GUIContent
        {
            text = "Clear All Datas"
        };
    }

    private void OnGUI()
    {
        if (GUILayout.Button(resetContent))
        {
            ClearAllData();
        }
        dataModelName = EditorGUILayout.TextField("Data Model Name", dataModelName);
        if (GUILayout.Button(createContent))
        {
            CreateNewDataModel(dataModelName);
        }
        
    }

    private static void ClearAllData()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.dat");
        foreach (string file in files)
        {
            File.Delete(file);
        }

        PlayerPrefs.DeleteAll();

        if (Directory.GetFiles(Application.persistentDataPath, "*.dat").Length == 0)
        {
            Debug.Log("Data Clear Succeed");
        }
    }

    private static void CreateNewDataModel(string DataName)
    {
        Regex regexItem = new ("^[a-zA-Z0-9 ]*$");

        if (DataName != null && !char.IsNumber(DataName.ToCharArray().ElementAt(0)) && regexItem.IsMatch(DataName))
        {
            DataName = DataName.Replace(" ", "");
            string targetPath = Application.dataPath + Constants.Strings.NEW_DATA_MODEL_PATH + DataName + ".cs";
            string sampleDataModelPath = Application.dataPath + Constants.Strings.SAMPLE_DATA_MODEL_PATH;
            string sampleDataModelText = File.ReadAllText(sampleDataModelPath);
            sampleDataModelText = sampleDataModelText.Replace("SampleDataModel", DataName);
            
            if (File.Exists(targetPath) == false)
            {
                Debug.Log("Creating DataModel: " + targetPath);
                using StreamWriter outfile =
                    new (targetPath);
                outfile.Write(sampleDataModelText);
            }
            else
                Debug.LogError("There is a data model with the same name!");
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.LogError("Check Data Name!");
        }
    }
}
using System;
using System.Collections.Generic;

[Serializable] 
public class LevelDataModel : DataModel 
{ 
    public static LevelDataModel Data;
    public List<LevelEntityDataList> levelEntityDatas = new ();
    
    public LevelDataModel Load() 
    {
        if (Data == null)
        {
            Data = this;
            object data = LoadData();
            if (data != null)
            {
                Data = (LevelDataModel)data;
            }
        }
        return Data;
    }
    public void Save()
    {
        Save(Data);
    }
}
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class DataModel
{
    protected virtual void Save(object data)
    {
        BinaryFormatter bf = new ()
        {
            Binder = new CustomizedBinder()
        };
        FileStream file = new (Application.persistentDataPath + "/" + GetType().Name + ".dat",
            FileMode.Create, FileAccess.Write, FileShare.None);
        bf.Serialize(file, data);
        file.Close();
        file.Dispose();
    }

    public virtual void Delete()
    {
        if (File.Exists(Application.persistentDataPath + "/" + GetType().Name + ".dat"))
        {
            File.Delete(Application.persistentDataPath + "/" + GetType().Name + ".dat");
        }
    }

    public bool DataExists()
    {
        return File.Exists(Application.persistentDataPath + "/" + GetType().Name + ".dat");
    }

    protected object LoadData()
    {
        string path = Application.persistentDataPath + "/" + GetType().Name + ".dat";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new ()
            {
                Binder = new CustomizedBinder()
            };
            FileStream file = new (Application.persistentDataPath + "/" + GetType().Name + ".dat",
                FileMode.Open, FileAccess.Read, FileShare.None);
            if (!(file.CanSeek && file.Length == 0L))
            {
                object data = bf.Deserialize(file);
                file.Close();
                file.Dispose();
                return data;
            }
            else return null;
        }
        else
        {
            return null;
        }
    }
}

internal sealed class CustomizedBinder : SerializationBinder
{
    public override Type BindToType(string assemblyName, string typeName)
    {
        Type returntype = null;
        const string sharedAssemblyName = "BaseAssembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        assemblyName = Assembly.GetExecutingAssembly().FullName;
        typeName = typeName.Replace(sharedAssemblyName, assemblyName);
        returntype =
            Type.GetType($"{typeName}, {assemblyName}");

        return returntype;
    }

    public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
    {
        base.BindToName(serializedType, out assemblyName, out typeName);
        assemblyName = "Assembly-BaseAssembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

internal sealed class CustomizedBinder : SerializationBinder
{
    public override Type BindToType(string assemblyName, string typeName)
    {
        const string sharedAssemblyName =
            "Assembly-BaseAssembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        assemblyName = Assembly.GetExecutingAssembly().FullName;
        typeName = typeName.Replace(sharedAssemblyName, assemblyName);

        Type returnType = Type.GetType(typeName);

        return returnType;
    }

    public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
    {
        base.BindToName(serializedType, out assemblyName, out typeName);
        assemblyName = "BaseAssembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
    }
}
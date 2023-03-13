using System.Collections.Generic;


public static class InspectorExtensions
{
    public static object[] GetParameterValues(this List<ParameterView> parameters)
    {
        object[] objParamters = new object[parameters.Count];

        for (int i = 0; i < objParamters.Length; i++)
        {
            objParamters[i] = parameters[i].Value;
        }

        return objParamters;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CustomEditorView : MethodAttributeViewModel
{
    private MethodInfo targetMethod;

    public override void Draw(Object target)
    {
        if (targetMethod != null)
            targetMethod.Invoke(target, null);

        base.Draw(target);
    }

    public override void CheckMethod(MethodInfo method)
    {
        object[] attributeDatas = method.GetCustomAttributes(typeof(EditorCustomInpector), true);

        if (attributeDatas.Length > 0)
        {
            targetMethod = method;
        }

        base.CheckMethod(method);
    }
}
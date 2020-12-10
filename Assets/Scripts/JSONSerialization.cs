using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class JSONSerialization
{
    static string jsonString;

    public static void Save(object toSave)
    {
       jsonString= JsonUtility.ToJson(toSave);
    }

    public static T Load<T>()
    {
        T toRet = JsonUtility.FromJson<T>(jsonString);
        return toRet;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

static class XMLDataSerialization
{
    public static void Write(string filePath, object toSave)
    {
        var serializer = new XmlSerializer(typeof(ObjectInfoSaveLoad));
        string path = Path.Combine(Application.streamingAssetsPath, filePath);
        var stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, toSave);
        stream.Close();
    }

    public static T Read<T>(string filePath)
    {
        T toRet;
        string path = Path.Combine(Application.streamingAssetsPath, filePath);

        if(File.Exists(path))
        {
            var serializer = new XmlSerializer(typeof(ObjectInfoSaveLoad));
            var stream = new FileStream(path, FileMode.Open);
            toRet = (T)serializer.Deserialize(stream);
            stream.Close();
        }
        else
            toRet = default(T);

        return toRet;
    }
}

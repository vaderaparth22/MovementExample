using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

public class Spawner : MonoBehaviour
{
    public int spawnCount;
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public Transform spawnArea;
    public LayerMask layerToCheck;

    const string fileName = "myData";
    const string XmlfileName = "xmldata.xml";

    List<GameObject> allObjects;
    List<ObjectInfo> objInfoList;
    ObjectInfoSaveLoad objSaveLoad = new ObjectInfoSaveLoad();

    private void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        allObjects = new List<GameObject>();

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos = GetValidPosition();
            Quaternion rot = Quaternion.Euler(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));

            int randomNumber = Random.Range(0, 2);
            GameObject obj;

            if (randomNumber == 0)       //spawn cube
            {
                obj = Instantiate(cubePrefab, pos, rot);
            }
            else                        //spawn sphere
            {
                obj = Instantiate(spherePrefab, pos, rot);
            }

            obj.transform.name = randomNumber.ToString();

            GetOrSetRandomColor(obj.gameObject);

            allObjects.Add(obj);
        }
    }

    void SpawnWithData(ObjectInfoSaveLoad loadedClass)
    {
        List<ObjectInfo> infoList = loadedClass.GetInfoList();

        for (int i = 0; i < infoList.Count; i++)
        {
            ObjectInfo info = infoList[i];

            Vector3 pos = info.GetPosition();
            Vector3 rot = info.GetRotation();
            Vector3 velo = info.GetVelocity();
            Vector3 angularVelo = info.GetAngularVelocity();
            Color color = info.GetColor();
            int shapeId = info.GetShapeId();

            GameObject obj;

            if (shapeId == 0)
            {
                obj = Instantiate(cubePrefab);
                
            }
            else
            {
                obj = Instantiate(spherePrefab);
            }

            obj.transform.name = shapeId.ToString();
            obj.transform.position = pos;
            obj.transform.eulerAngles = rot;
            obj.GetComponent<MeshRenderer>().material.color = color;
            obj.GetComponent<Rigidbody>().velocity = velo;
            obj.GetComponent<Rigidbody>().angularVelocity = angularVelo;

            allObjects.Add(obj);
        }
    }

    Vector3 GetValidPosition()
    {
        Collider[] objectColliders;
        Vector3 pos;

        do
        {
            pos = new Vector3(Random.Range(-spawnArea.localScale.x / 2, spawnArea.localScale.x / 2), spawnArea.position.y, Random.Range(-spawnArea.localScale.z / 2, spawnArea.localScale.z / 2));
            objectColliders = Physics.OverlapSphere(pos, 2f, layerToCheck);

        } while (objectColliders.Length > 0);

        return pos;
    }

    Color GetOrSetRandomColor(GameObject obj)
    {
        Color newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        obj.GetComponent<MeshRenderer>().material.color = newColor;
        return newColor;
    }

    public void ResetAndSpawn()
    {
        foreach (GameObject item in allObjects)
            Destroy(item);

        allObjects.Clear();
        objSaveLoad.GetInfoList().Clear();

        SpawnObjects();
    }

    #region Binary Saving
    public void SaveBinary()
    {
        for (int i = 0; i < allObjects.Count; i++)
        {
            Transform current = allObjects[i].transform;

            ObjectInfo info = new ObjectInfo();
            info.SetPosition(current.position);
            info.SetRotation(current.eulerAngles);
            info.SetShapeId(int.Parse(current.name));
            info.SetColor(current.GetComponent<MeshRenderer>().material.color);
            info.SetVelocity(current.GetComponent<Rigidbody>().velocity);
            info.SetAngularVelocity(current.GetComponent<Rigidbody>().angularVelocity);

            objSaveLoad.AddInfo(info);
        }

        BinaryDataSaving.SaveDataToDisk(fileName, objSaveLoad);
    }

    public void LoadBinary()
    {
        foreach (GameObject item in allObjects)
            Destroy(item);

        allObjects.Clear();

        ObjectInfoSaveLoad loadedClass = BinaryDataSaving.LoadDataFromDisk<ObjectInfoSaveLoad>(fileName);
        SpawnWithData(loadedClass);
    }
    #endregion

    #region XML Saving
    public void SaveXML()
    {
        for (int i = 0; i < allObjects.Count; i++)
        {
            Transform current = allObjects[i].transform;

            ObjectInfo info = new ObjectInfo();
            info.SetPosition(current.position);
            info.SetRotation(current.eulerAngles);
            info.SetShapeId(int.Parse(current.name));
            info.SetColor(current.GetComponent<MeshRenderer>().material.color);
            info.SetVelocity(current.GetComponent<Rigidbody>().velocity);
            info.SetAngularVelocity(current.GetComponent<Rigidbody>().angularVelocity);

            objSaveLoad.AddInfo(info);
        }

        XMLDataSerialization.Write(XmlfileName, objSaveLoad);
    }

    public void LoadXML()
    {
        foreach (GameObject item in allObjects)
            Destroy(item);

        allObjects.Clear();

        ObjectInfoSaveLoad loadedClass = XMLDataSerialization.Read<ObjectInfoSaveLoad>(XmlfileName);
        SpawnWithData(loadedClass);
    }
    #endregion
}

[System.Serializable]
[XmlRoot("ObjectSaveLoadClass")]
public class ObjectInfoSaveLoad
{
    [XmlArray("Objects"), XmlArrayItem("ObjectInfoList")]
    public List<ObjectInfo> objectInfoList = new List<ObjectInfo>();

    public void AddInfo(ObjectInfo objectInfoRef)
    {
        objectInfoList.Add(objectInfoRef);
    }

    public List<ObjectInfo> GetInfoList()
    {
        return this.objectInfoList;
    }
}

static class BinaryDataSaving
{
    public static void SaveDataToDisk(string filePath, object toSave)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Path.Combine(Application.streamingAssetsPath, filePath);
        FileStream file = File.Create(path);
        bf.Serialize(file, toSave);
        file.Close();
    }

    public static T LoadDataFromDisk<T>(string filePath)
    {
        T toRet;
        string path = Path.Combine(Application.streamingAssetsPath, filePath);

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            toRet = (T)bf.Deserialize(file);
            file.Close();
        }
        else
            toRet = default(T);

        return toRet;
    }
}

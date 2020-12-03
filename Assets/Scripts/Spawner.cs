using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Spawner : MonoBehaviour
{
    public int spawnCount;
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public Transform spawnArea;
    public LayerMask layerToCheck;

    List<GameObject> allObjects;
    ObjectInfoSaveLoad objSaveLoad = new ObjectInfoSaveLoad();

    private void Start()
    {
        SpawnAtStart();
    }

    void SpawnAtStart()
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

            SetRandomColor(obj.gameObject);

            allObjects.Add(obj);

            ObjectInfo info = new ObjectInfo();
            info.SetPosition(pos);
            objSaveLoad.AddInfo(info);
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

    void SetRandomColor(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public void ResetAndSpawn()
    {
        foreach (GameObject item in allObjects)
            Destroy(item);

        allObjects.Clear();

        SpawnAtStart();
    }

    public void SaveBinary()
    {
        BinaryDataSaving.SaveDataToDisk("myData", objSaveLoad);
    }

    public void LoadBinary()
    {
        
    }
}

[System.Serializable]
class ObjectInfoSaveLoad
{
    List<ObjectInfo> objectInfoList = new List<ObjectInfo>();

    public void AddInfo(ObjectInfo objectInfoRef)
    {
        objectInfoList.Add(objectInfoRef);
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

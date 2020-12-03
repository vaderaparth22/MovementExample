using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int spawnCount;
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public Transform spawnArea;
    public LayerMask layerToCheck;

    List<GameObject> allObjects;
    ObjectsData dataClass;

    private void Start()
    {
        SpawnAtStart();
        dataClass = new ObjectsData();
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

            SetRandomColor(obj);

            allObjects.Add(obj);
            dataClass.AddPosition(pos);
            dataClass.AddRotation(rot);
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
        dataClass.positionList.Clear();
        dataClass.rotationList.Clear();

        SpawnAtStart();
    }
}

[System.Serializable]
class ObjectsData
{
    public List<Vector3> positionList { get; private set; }
    public List<Quaternion> rotationList { get; private set; }

    public ObjectsData()
    {
        positionList = new List<Vector3>();
        rotationList = new List<Quaternion>();
    }

    public void AddPosition(Vector3 posToAdd)
    {
        positionList.Add(posToAdd);
    }

    public void AddRotation(Quaternion rotToAdd)
    {
        rotationList.Add(rotToAdd);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    Vector3Serializable pos;

    public void SetPosition(Vector3 posToSet)
    {
        this.pos = new Vector3Serializable(posToSet.x, posToSet.y, posToSet.z);
    }

    public Vector3 GetPosition()
    {
        return new Vector3(this.pos.x, this.pos.y, this.pos.z);
    }
}

[System.Serializable]
public struct Vector3Serializable
{
    public float x;
    public float y;
    public float z;

    public Vector3Serializable(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

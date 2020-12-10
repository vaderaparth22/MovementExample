using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class ObjectInfo
{
    public int shapeId;

    public Vector3Serializable pos;

    public Vector3Serializable rot;

    public Vector3Serializable color;

    public Vector3Serializable velo;

    public Vector3Serializable angularVelo;

    public void SetPosition(Vector3 posToSet)
    {
        this.pos = new Vector3Serializable(posToSet.x, posToSet.y, posToSet.z);
    }

    public Vector3 GetPosition()
    {
        return new Vector3(this.pos.x, this.pos.y, this.pos.z);
    }

    public void SetRotation(Vector3 rotToSet)
    {
        this.rot = new Vector3Serializable(rotToSet.x, rotToSet.y, rotToSet.z);
    }

    public Vector3 GetRotation()
    {
        return new Vector3(this.rot.x, this.rot.y, this.rot.z);
    }

    public void SetColor(Color colorToSet)
    {
        this.color = new Vector3Serializable(colorToSet.r, colorToSet.g, colorToSet.b);
    }

    public Color GetColor()
    {
        return new Color(this.color.x, this.color.y, this.color.z);
    }

    public void SetVelocity(Vector3 veloToSet)
    {
        this.velo = new Vector3Serializable(veloToSet.x, veloToSet.y, veloToSet.z);
    }

    public Vector3 GetVelocity()
    {
        return new Vector3(this.velo.x, this.velo.y, this.velo.z);
    }

    public void SetAngularVelocity(Vector3 veloToSet)
    {
        this.angularVelo = new Vector3Serializable(veloToSet.x, veloToSet.y, veloToSet.z);
    }

    public Vector3 GetAngularVelocity()
    {
        return new Vector3(this.angularVelo.x, this.angularVelo.y, this.angularVelo.z);
    }

    public void SetShapeId(int shapeId)
    {
        this.shapeId = shapeId;
    }

    public int GetShapeId()
    {
        return this.shapeId;
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

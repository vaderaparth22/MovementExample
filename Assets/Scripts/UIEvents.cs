using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public Spawner spawner;

    public void Reset()
    {
        spawner.ResetAndSpawn();
    }

    #region SAVE
    public void SaveBinary()
    {
        spawner.SaveBinary();
    }

    public void SaveXML()
    {

    }

    public void SaveJSON()
    {

    }
    #endregion


    #region LOAD
    public void LoadBinary()
    {
        spawner.LoadBinary();
    }

    public void LoadXML()
    {

    }
    
    public void LoadJSON()
    {

    }
    #endregion
}

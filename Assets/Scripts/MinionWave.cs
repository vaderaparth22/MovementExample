using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MinionWave
{
	public enum WaveType
	{
		Meele,
		Super
	}

	public WaveType Type;
	public GameObject Prefab;
	public int Count;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Chunk", menuName = "Chunk")]
public class Chunk:ScriptableObject
{
    public GameObject chunkObject;
    public float width;
    public Vector3 enemyPosition = new Vector3(0, 5, 0);
 
}

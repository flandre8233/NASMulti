using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSpawner : MonoBehaviour
{
    public static GameObject Spawn(string FileName, float DelayDestroyTime)
    {
        return Spawn(FileName, new Vector3(), DelayDestroyTime);
    }

    public static GameObject Spawn(string FileName, Vector3 Pos, float DelayDestroyTime)
    {
        return Spawn(FileName, Pos, Quaternion.identity, DelayDestroyTime);
    }

    public static GameObject Spawn(string FileName, Vector3 Pos, Quaternion Rotation, float DelayDestroyTime)
    {
        GameObject SpawnObj = Spawn(FileName, Pos, Rotation);
        ObjectPool.instance.DelayPool(SpawnObj , DelayDestroyTime);
        return SpawnObj;
    }
    public static GameObject Spawn(string FileName)
    {
        return Spawn(FileName, new Vector3());
    }

    public static GameObject Spawn(string FileName, Vector3 Pos)
    {
        return Spawn(FileName, Pos, Quaternion.identity);
    }

    public static GameObject Spawn(string FileName, Vector3 Pos, Quaternion Rotation)
    {
        GameObject SpawnedObject = ObjectPool.instance.GetObjectForType(FileName, false);
        SpawnedObject.transform.position = Pos;
        SpawnedObject.transform.rotation = Rotation;
        return SpawnedObject;
    }

   
}

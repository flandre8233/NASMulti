using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : SingletonMonoBehavior<MapView>
{
    [SerializeField] Transform SpawnParent;
    GameObject[,] MapViews;



    public void DrawMapUnSync(int x, int y, int content)
    {
        GameData Data = GameRoom.instance.Room.GameData;
        MapViews[x, y].GetComponent<BelongChanger>().Belong(content);
    }

    public void DrawMap(int x, int y)
    {
        GameData Data = GameRoom.instance.Room.GameData;
        DrawMapUnSync(x, y, Data.GetMapContent(x, y));
    }

    public void DrawMap()
    {
        int bounder = 3;
        for (int y = 0; y < bounder; y++)
        {
            for (int x = 0; x < bounder; x++)
            {
                DrawMap(x, y);
            }
        }
    }

    void BuildMap()
    {
        GameData Data = GameRoom.instance.Room.GameData;
        int bounder = 3;
        MapViews = new GameObject[bounder, bounder];
        for (int y = 0; y < bounder; y++)
        {
            for (int x = 0; x < bounder; x++)
            {
                GameObject Spawnobject = ResourcesSpawner.Spawn("Cube", new Vector3(-1 + x, -1 + y, 0));
                Spawnobject.transform.SetParent(SpawnParent);
                MapViews[x, y] = Spawnobject;
                Spawnobject.GetComponent<ObjectCollision>().x = x;
                Spawnobject.GetComponent<ObjectCollision>().y = y;
            }
        }
    }

    private void OnEnable()
    {
        BuildMap();
        DrawMap();
    }

    private void OnDisable()
    {
        foreach (var item in MapViews)
        {
            Destroy(item);
        }
        MapViews = null;
    }
}

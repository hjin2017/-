using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManger : MonoBehaviour
{
    public string type;
    public int Size;
    public GameObject Prefab;
    public int coll;
    public float dist;
    public Vector3 startPos;
    public GameObject GroupPrefab;

    private ST_GameOBJ sT_GameOBJ;
    private GAME_MANAGER manager;
    // Use this for initialization
    void Start()
    {
        manager =GameObject.Find("OBJmanager").GetComponent<GAME_MANAGER>();
        sT_GameOBJ.ArrGameOBJ = new ST_GameObject[Size];
        sT_GameOBJ.Size = Size;
        for (int i = 0; i <Size; i++)
        {
            sT_GameOBJ.ArrGameOBJ[i].GameOBJ = GameObject.Instantiate(Prefab) as GameObject;
            sT_GameOBJ.ArrGameOBJ[i].GameOBJ.transform.parent = GroupPrefab.transform;
            sT_GameOBJ.ArrGameOBJ[i].GameOBJ.transform.position = GroupPrefab.transform.position + startPos + new Vector3(i % coll * dist, 0, i / coll * dist);
            sT_GameOBJ.ArrGameOBJ[i].Index = i;
        }
        manager.inputOBJ(sT_GameOBJ, type);
    }
}

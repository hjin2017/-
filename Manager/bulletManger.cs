using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bulletManger : MonoBehaviour {

    public string type;
    public int Size;
    public GameObject Prefab;
    public GameObject GroupPrefab;

    private ST_Gamebullet sT_GameOBJ;
    private GAME_MANAGER manager;
    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("OBJmanager").GetComponent<GAME_MANAGER>();
        sT_GameOBJ.ArrGameOBJ = new ST_GameObject[Size];
        sT_GameOBJ.Size = Size;
        for (int i = 0; i < Size; i++)
        {
            sT_GameOBJ.ArrGameOBJ[i].GameOBJ = GameObject.Instantiate(Prefab) as GameObject;
            sT_GameOBJ.ArrGameOBJ[i].GameOBJ.transform.parent = GroupPrefab.transform;
            sT_GameOBJ.ArrGameOBJ[i].Index = i;
            sT_GameOBJ.ArrGameOBJ[i].GameOBJ.SetActive(false);
            sT_GameOBJ.Group = GroupPrefab;
            sT_GameOBJ.Prev = 1;
        }
        manager.inputBullet(sT_GameOBJ, type);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ST_GameObject
{
    public int Index;
    public GameObject GameOBJ;
};

public struct ST_GameOBJ
{
    public int Size;
    public ST_GameObject[] ArrGameOBJ;
};

public struct ST_Gamebullet
{
    public int Prev;
    public int Size;
    public ST_GameObject[] ArrGameOBJ;
    public GameObject Group;
};

public class GAME_MANAGER : MonoBehaviour {

    private Dictionary<string, ST_GameOBJ> mapOBJ;
    private Dictionary<string, ST_Gamebullet> mapBullet;
    private int[] ListStage;
    public int ListIndex;

    private void Start()
    {
        mapOBJ = new Dictionary<string, ST_GameOBJ>();
        mapBullet= new Dictionary<string, ST_Gamebullet>();
        ListStage = new int[ListIndex];
        for (int i = 0; i < ListIndex; i++)
            ListStage[i]=i;
        ListIndex -= 1;
    }
   
    public void inputOBJ(ST_GameOBJ pOBJ , string type )
    {
        mapOBJ.Add(type, pOBJ);
    }
    public void reSetOBJ(string type)
    {
        ST_GameOBJ OBJ;
        mapOBJ.TryGetValue(type, out OBJ);
        for (int i=0;i< OBJ.Size; i++)
       {
            OBJ.ArrGameOBJ[i].GameOBJ.SetActive(true);
       }

    }
    public void resettrick()
    {
        StartCoroutine("resetT");
    }
   IEnumerator resetT()
    {
        ST_GameOBJ OBJ;
        mapOBJ.TryGetValue("큐브", out OBJ);

        int i = OBJ.Size - 1;
        Debug.Log(i);
        while (true)
        {
            for (int j = 0; j<5; j++)
            {
                Debug.Log(i);
                OBJ.ArrGameOBJ[i].GameOBJ.SetActive(true);
                if (i == 0)
                    StopCoroutine("resetT");
                i--;
            }
           
            yield return new WaitForSeconds(0.4f);
        }
       
    }
    public void inputBullet(ST_Gamebullet pbullet, string type)
    {
        mapBullet.Add(type, pbullet);
    }

    public void bulletFir(string type,Vector3 pos, Vector3 dir)
    {
        ST_Gamebullet BULLET = mapBullet[type];
        mapBullet.TryGetValue(type,out BULLET);

        if (BULLET.Prev == BULLET.Size) BULLET.Prev = 0;
        if(BULLET.ArrGameOBJ[BULLET.Prev].GameOBJ.gameObject.GetComponent<Rigidbody>()!=null)
        BULLET.ArrGameOBJ[BULLET.Prev].GameOBJ.gameObject.GetComponent<Rigidbody>().Sleep();

        BULLET.ArrGameOBJ[BULLET.Prev].GameOBJ.transform.position = pos + dir + BULLET.Group.transform.position;
        //BULLET.ArrGameOBJ[BULLET.Prev].GameOBJ.transform.forward = dir;
        BULLET.ArrGameOBJ[BULLET.Prev].GameOBJ.SetActive(true);
        BULLET.Prev++;
        mapBullet[type] = BULLET;
 
    }
    public void bulletrest(string type)
    {
        ST_Gamebullet OBJ;
        mapBullet.TryGetValue(type, out OBJ);

        for (int i = 0; i < OBJ.Size; i++)
        {
            OBJ.ArrGameOBJ[i].GameOBJ.SetActive(false);
        }
    }

    public void setBar(int Index)
    {
        ListIndex = Index;
        Debug.Log(ListIndex);
    }
    public int intbar()
    {
        return ListStage[ListIndex];
      
    }
}

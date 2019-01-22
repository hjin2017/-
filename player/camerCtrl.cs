using UnityEngine;
using System.Collections;

public class camerCtrl : MonoBehaviour {
    private GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            transform.forward = player.transform.forward;
            transform.position = player.transform.position;
        }
        transform.Rotate(new Vector3(30, 0, 0));
    }

	// Update is called once per frame
	void Update () {
        Vector3 origPosition = transform.position;
        if (player == null) return;
        transform.position = Vector3.Lerp(origPosition,
            new Vector3(player.transform.position.x , 
            player.transform.position.y + 10,
            player.transform.position.z -10), 
            Time.deltaTime * 1.5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayManager : MonoBehaviour
{
    PhotonView view;
    int health;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "InstaKill")
        {
            health -= 1;
            Debug.Log("Health = "+health);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

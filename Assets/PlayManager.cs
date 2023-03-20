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
    [System.Serializable]
    public struct HealthData
    {
        public int currentHealth;
        public bool hasChanged;
    }

    PhotonView view;
    public HealthData healthData;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "InstaKill")
        {
            healthData.currentHealth -= 1;
            healthData.hasChanged = true;
            Debug.Log("Health = "+healthData.currentHealth);
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

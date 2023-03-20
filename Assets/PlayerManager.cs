using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerManager : MonoBehaviour
{
    PhotonView view;
    public GameObject controller;
   
    public SmoothFollow camera;
    private void Awake()
    {
        view = GetComponent<PhotonView>();
       
    }

    private void Start()
    {
        camera = GameObject.Find("Camera").GetComponent<SmoothFollow>();
        if (view.IsMine)
        {
            CreateController();
        }
    }

    // Start is called before the first frame update
    void CreateController()
    {

        Transform spawn= SpawnPlayers.instance.GetSpawnPoint();
        controller = PhotonNetwork.Instantiate("PlayerController", spawn.position, spawn.rotation, 0, new object[] { view.ViewID });
        controller.GetComponent<MovementScript>().playerManager = this;
        camera.targetObject = controller;
        camera.target = controller.transform;
    }

    public void Die()
    {
        Debug.Log("Died (Player Manager)");
        PhotonNetwork.Destroy(controller);
        CreateController();
    }
}

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
    int kills = 0;
    int deaths = 0;
    int score = 0;
   
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
        camera.targetObject = controller.GetComponent<MovementScript>().cameraTarget;
        camera.target = controller.GetComponent<MovementScript>().cameraTarget.transform;
    }

    public void Die()
    {
        Debug.Log("Died (Player Manager)");
        PhotonNetwork.Destroy(controller);
        CreateController();
        deaths++;
        score-=1;
        Hashtable hash = new Hashtable();
        hash.Add("deaths", deaths);
        hash.Add("score", score);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

    }

    public void GetKill(){

        view.RPC("RPC_GetKill", view.Owner);

    }

    [PunRPC]
    void RPC_GetKill()
    {
        kills++;
        score+=1;

        Hashtable hash = new Hashtable();
        hash.Add("kills", kills);
        hash.Add("score", score);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        
    }
    
    public static PlayerManager Find(Player player){

        return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.view.Owner == player);

    }
}

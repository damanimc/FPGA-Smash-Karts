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
   

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (view.IsMine) {
            CreateController();
        }
    }

    // Start is called before the first frame update
    void CreateController()
    {
        
    }

}

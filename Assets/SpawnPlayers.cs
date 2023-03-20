using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public SmoothFollow camera;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX,maxX),0,Random.Range(minZ,maxZ));
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        player.GetComponent<MovementScript>().nameTag.text = PhotonNetwork.NickName;
        camera.targetObject = player;
        camera.target = player.transform;
    }
}

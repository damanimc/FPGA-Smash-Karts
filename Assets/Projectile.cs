using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Projectile : MonoBehaviour
{
    PhotonView view;
    [SerializeField] int maxBounces=3;
    private int bounces = 0;
    // Start is called before the first frame update
    private void Awake()
    {
      
    }
    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.tag == "Player") { PhotonNetwork.Destroy(this.gameObject); }
        bounces += 1;
       
    }
    private void FixedUpdate()
    {
        if (bounces >= maxBounces) { PhotonNetwork.Destroy(this.gameObject); }
    }

}

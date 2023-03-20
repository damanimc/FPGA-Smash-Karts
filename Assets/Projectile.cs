using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Projectile : MonoBehaviour
{
    PhotonView view;
    [SerializeField] float lifeSpan=2f;
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(Wait(lifeSpan, 0));
       

    }
    private void OnCollisionEnter(Collision collision)
    {
      
            if (collision.gameObject.tag == "Player") { PhotonNetwork.Destroy(this.gameObject); }
      
       
    }
    IEnumerator Wait(float seconds, int index)
    {
        
            yield return new WaitForSeconds(seconds);
            PhotonNetwork.Destroy(gameObject);
       
    }
}

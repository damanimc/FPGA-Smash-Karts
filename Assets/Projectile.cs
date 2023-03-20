using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Projectile : MonoBehaviour
{
    PhotonView view;
    [SerializeField] float lifeSpan=5f;
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(Wait(lifeSpan, 0));
        view = GetComponent<PhotonView>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (view.IsMine)
        {
            if (collision.gameObject.tag == "Player") { PhotonNetwork.Destroy(gameObject); }
        }
       
    }
    IEnumerator Wait(float seconds, int index)
    {
        if (view.IsMine)
        {
            yield return new WaitForSeconds(seconds);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}

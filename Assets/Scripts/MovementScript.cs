using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class MovementScript : MonoBehaviour
{
    public TMP_Text nameTag;
    public float movSpeed = 50;
    public float maxSpeed = 15;
    public float drag = 0.98f;
    public float steerAngle = 20;
    public float traction = 1;
    private Rigidbody thisRb;
    private float health = 1.0f;
    PhotonView view;

    // Variables
    private Vector3 moveForce;

    // Start is called before the first frame update
    private void Start()
    {
     
        view = GetComponent<PhotonView>();
        nameTag.text = view.Owner.NickName;
    }
    void shoot()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
           
            // Moving
            moveForce += transform.forward * movSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += moveForce * Time.deltaTime;

            // Steering
            float steerInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * steerInput * moveForce.magnitude * steerAngle * Time.deltaTime);

            // drag and max speed limit
            moveForce *= drag;
            moveForce = Vector3.ClampMagnitude(moveForce, maxSpeed);

            // traction
            Debug.DrawRay(transform.position, moveForce.normalized * 3);
            Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
            moveForce = Vector3.Lerp(moveForce.normalized, transform.forward, traction * Time.deltaTime) * moveForce.magnitude;
            
        }
        
    }
}

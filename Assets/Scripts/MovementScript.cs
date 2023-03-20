using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Diagnostics;
using System.Threading.Tasks;

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

    float right = 0.0f;
    int button;
    int toMove;

    // Variables
    private Vector3 moveForce;

    private async Task RunProcess()
    {
    ProcessStartInfo startInfo = new ProcessStartInfo
    {
        FileName = @"C:\intelFPGA_lite\20.1\quartus\bin64\nios2-terminal.exe",
        Arguments = "--instance 0",
        UseShellExecute = false,
        RedirectStandardOutput = true,
        CreateNoWindow = false
    };

    Process process = new Process();
    process.StartInfo = startInfo;
    process.Start();

    while (true)
    {
        if (!process.HasExited)
        {

            string line = await process.StandardOutput.ReadLineAsync();
            UnityEngine.Debug.Log(line);

            if (line == "l0")
            {
                right = -1.0f;
                button = 0;

            }
            else if (line == "l1")
            {
                right = -1.0f;
                button = 1;

            }
            else if (line == "l2")
            {
                right = -1.0f;
                button = 2;

            }
            else if (line == "l3")
            {
                right = -1.0f;
                button = 3;

            }
            else if (line == "r0")
            {
                right = 1.0f;
                button = 0;

            }
            else if (line == "r1")
            {
                right = 1.0f;
                button = 1;

            }
            else if (line == "r2")
            {
                right = 1.0f;
                button = 2;

            }
            else if (line == "r3")
            {
                right = 1.0f;
                button = 3;

            }
            else if (line == "n0")
            {
                right = 0.0f;
                button = 0;

            }
            else if (line == "n1")
            {
                right = 0.0f;
                button = 1;

            }
            else if (line == "n2")
            {
                right = 0.0f;
                button = 2;

            }
            else if (line == "n3")
            {
                right = 0.0f;
                button = 3;

            }
        }
        else
        {
            UnityEngine.Debug.LogError("The process has exited unexpectedly.");
            break;
        }

        // Wait for a short amount of time before checking for new data again
        //await Task.Delay(1); // Delay for 100 milliseconds
    }
    }

    // Start is called before the first frame update
    private void Start()
    {
     
        view = GetComponent<PhotonView>();
        // RunProcess(); uncomment to use fpga
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
            // if(button > 1){
            //     toMove = 1;
            // }
            // else {
            //     toMove = 0;
            // } uncomment to use fpga
           
            // Moving
            // moveForce += transform.forward * movSpeed * toMove * Time.deltaTime; uncomment to use fpga
            moveForce += transform.forward * movSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += moveForce * Time.deltaTime;

            // Steering
            // float steerInput = right; uncomment to use FPGA
            float steerInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * steerInput * moveForce.magnitude * steerAngle * Time.deltaTime);

            // drag and max speed limit
            moveForce *= drag;
            moveForce = Vector3.ClampMagnitude(moveForce, maxSpeed);

            // traction
            UnityEngine.Debug.DrawRay(transform.position, moveForce.normalized * 3);
            UnityEngine.Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
            moveForce = Vector3.Lerp(moveForce.normalized, transform.forward, traction * Time.deltaTime) * moveForce.magnitude;
            
        }
        
    }
}

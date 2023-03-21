using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Diagnostics;
using System.Threading.Tasks;



public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    CharacterController ch;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public float forward = 0.0f;
    public float right = 0.0f;
      
    public bool isGrounded;
    Rigidbody rb;


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
                    // Check the value of the line and move the cow accordingly
            if (line == "l")
            {
                right = -1.0f;
                forward = 0.0f;
            }
            else if (line == "r")
            {
                right = 1.0f;
                forward = 0.0f;
            }
            else if (line == "b")
            {
                forward = -1.0f;
                right = 0.0f;
            }
            else if (line == "f")
            {
                forward = 1.0f;
                right = 0.0f;
            }
            else if (line == "fr")
            {
                forward = 1.0f;
                right = 1.0f;
            }
            else if (line == "fl")
            {
                forward = 1.0f;
                right = -1.0f;
            }
            else if (line == "br")
            {
                forward = -1.0f;
                right = 1.0f;
            }
            else if (line == "bl")
            {
                forward = -1.0f;
                right = -1.0f;
            }
            else if (line == "i")
            {
                forward = 0.0f;
                right = 0.0f;
            }
        }
        else
        {
            UnityEngine.Debug.LogError("The process has exited unexpectedly.");
            break;
        }

        // Wait for a short amount of time before checking for new data again
        // await Task.Delay(1); // Delay for 100 milliseconds
    }
}


    // Start is called before the first frame update
    void Start()
    {
        ch = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        RunProcess();
    }



    // Update is called once per frame
    void Update()
    {
            


        // float x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime * 4;
        // float z = (Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime * 4) + 0.2f;
        float x = right * moveSpeed * Time.deltaTime * 4;
        float z = (forward * moveSpeed * Time.deltaTime * 4) + 0.2f;
        ch.Move(new Vector3(z,0,-x));
        if(Input.GetKeyDown(KeyCode.Space)){
      
            ch.Move(new Vector3(0,0.4f,0));
            isGrounded = false;
        }
    }


}


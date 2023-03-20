using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;

using TMPro;
public class MovementScript : MonoBehaviour, Damagable
{
    public SpawnPlayers spawnManager;
    public float health;
    public TMP_Text nameTag;
    float movSpeed = 60;
    float maxSpeed = 15;
    float drag = 0.98f;
    float steerAngle = 20;
    float traction = 3;
    private Rigidbody rb;
    PhotonView view;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletPrefab;
    float right = 0.0f;
    int button;
    int toMove;
    const float maxHealth = 10f;
    // Variables
    private Vector3 moveForce;
    public PlayerManager playerManager;
    private StreamWriter _inputStreamWriter;

    private async Task RunProcess()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = @"C:\intelFPGA_lite\20.1\quartus\bin64\nios2-terminal.exe",
            Arguments = "--instance 0",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardInput = true,
            CreateNoWindow = false
        };

        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();
        _inputStreamWriter = process.StandardInput;

        while (true)
        {
            if (!process.HasExited)
            {

                string line = await process.StandardOutput.ReadLineAsync();
                UnityEngine.Debug.Log(line);
                //int hlth = healthData.currentHealth;


                //   if(0==1){
                //if(hlth == 100){
                //    await _inputStreamWriter.WriteLineAsync('f');
                //    await _inputStreamWriter.FlushAsync();
                //}
                //else if(hlth == 0){
                //    await _inputStreamWriter.WriteLineAsync('d');
                //    await _inputStreamWriter.FlushAsync();
                //}
                //else {
                //    await _inputStreamWriter.WriteLineAsync('h');
                //    await _inputStreamWriter.FlushAsync();
                //    if(hlth > 9){
                //        string hlths = hlth.ToString();
                //        await _inputStreamWriter.WriteLineAsync(hlths[0]);
                //        await _inputStreamWriter.FlushAsync();
                //        await _inputStreamWriter.WriteLineAsync(hlths[1]);
                //        await _inputStreamWriter.FlushAsync();
                //    }
                //    else{
                //        await _inputStreamWriter.WriteLineAsync('0');
                //        await _inputStreamWriter.FlushAsync();
                //        await _inputStreamWriter.WriteLineAsync((char)hlth);
                //        await _inputStreamWriter.FlushAsync();
                //    }
                // }
                //health = false;
                //    }

                //    if (line == "l0")
                //    {
                //        right = -1.0f;
                //        button = 0;

                //    }
                //    else if (line == "l1")
                //    {
                //        right = -1.0f;
                //        button = 1;

                //    }
                //    else if (line == "l2")
                //    {
                //        right = -1.0f;
                //        button = 2;

                //    }
                //    else if (line == "l3")
                //    {
                //        right = -1.0f;
                //        button = 3;

                //    }
                //    else if (line == "r0")
                //    {
                //        right = 1.0f;
                //        button = 0;

                //    }
                //    else if (line == "r1")
                //    {
                //        right = 1.0f;
                //        button = 1;

                //    }
                //    else if (line == "r2")
                //    {
                //        right = 1.0f;
                //        button = 2;

                //    }
                //    else if (line == "r3")
                //    {
                //        right = 1.0f;
                //        button = 3;

                //    }
                //    else if (line == "n0")
                //    {
                //        right = 0.0f;
                //        button = 0;

                //    }
                //    else if (line == "n1")
                //    {
                //        right = 0.0f;
                //        button = 1;

                //    }
                //    else if (line == "n2")
                //    {
                //        right = 0.0f;
                //        button = 2;

                //    }
                //    else if (line == "n3")
                //    {
                //        right = 0.0f;
                //        button = 3;

                //    }
                //}
                //else
                //{
                //    UnityEngine.Debug.LogError("The process has exited unexpectedly.");
                //    break;
                //}

                // Wait for a short amount of time before checking for new data again
                //await Task.Delay(1); // Delay for 100 milliseconds
                //}
            }

        }
    }
        // Start is called before the first frame update
        void Start()
        {
            
            health = maxHealth;
          
            // RunProcess(); uncomment to use fpga
        
            nameTag.text = view.Owner.NickName;

        }

        void Awake()
        {
            view = GetComponent<PhotonView>();
            rb = GetComponent<Rigidbody>();
            playerManager = PhotonView.Find((int)view.InstantiationData[0]).GetComponent<PlayerManager>();
        }
   
    
   
        void shoot()
        {
            UnityEngine.Debug.Log(" Shot fired");
        //GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, shootPoint.position, shootPoint.rotation);
        //bullet.GetComponent<Rigidbody>().velocity = shootPoint.forward * 3f;
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hit))
            {
                UnityEngine.Debug.Log(hit.collider.gameObject.name+" has been hit");
                hit.collider.gameObject.GetComponent<Damagable>()?.TakeDamage(1);
            }
        }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "InstaKill")
            {
                health -= 10;

                UnityEngine.Debug.Log(view.Owner.NickName + " : " + health + "HP");


            }
        }

        void FixedUpdate()
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
                rb.position += moveForce * Time.deltaTime;

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
        // Update is called once per frame
        void Update()
        {

            if (view.IsMine)
            {

             

                if(Input.GetMouseButtonDown(0))
                {
                    shoot();
                }

            }

        }
        public void TakeDamage(float damage)
        {
            view.RPC("RPC_TakeDamage", RpcTarget.All, damage);
        }

        [PunRPC]
        void RPC_TakeDamage(float damage)
        {
            if (!view.IsMine)
            {
              return;
            }

            health -= damage;
            if (health <= 0)
            {
                playerManager.Die();
            }

        }
       
       

}

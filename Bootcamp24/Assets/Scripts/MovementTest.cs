using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MovementTest : MonoBehaviourPunCallbacks
{
    //Not : SprintTime sýfýrlandýðýnda en az 20 olana kadar oyuncu tekrar koþamaz.

    PhotonView pw;

    public GameObject cube;
    public float moveSpeed; //hareket hýzý

    public Camera cam;

    public Rigidbody rb;

    public bool canRun = true; //koþmasý için izin
    float sprintTime = 100; //stamina süresi
    public float sprintFloors; //sprintTime'in artma ve azalma katsayýsý
    public bool isRunning = false; //koþup koþmadýðýný kontrol ediyor

    public float minXRot;
    public float maxXRot;

    private float curXRot;
    public float rotateSpeed;

    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sprintTime = 100;
        pw = GetComponent<PhotonView>();
        

        if (pw.IsMine == true)
        {
            GetComponent<Renderer>().material.color = Color.green;
            
            
        }
    }


    void Update()
    {
        camController();
        //serverda sadece bizde olmasý istenilen deðiþiklikler bu if'in içinde yazýlýr
        if (pw.IsMine == true)
        {
            //sprintTime'ý 100 ile 0 arasýnda tutmak için
            sprintTime = Mathf.Clamp(sprintTime, 0.0f, 100.0f);

            //hareket fonkisyonunu çaðýrýr
            move();

            //deðerleri kontrol etmek için console'da yazdýrýr
            Debug.Log("sprint time : " + sprintTime + " canRun : " + canRun + " isRunning :" + isRunning);

            //sprintTime'in artmasý ve kontrolü için
            if (isRunning == false)
            {
                sprintTime += sprintFloors * Time.deltaTime;

                if (sprintTime >= 20)
                {
                    canRun = true;
                }
            }

        }

    }

    void camController()
    {
        if (pw.IsMine == false)
        {
            cam.enabled = false;
        }
    }
    void move()
    {
        //Hareket kodu
        float moveX = Input.GetAxis("Vertical");
        float moveZ = Input.GetAxis("Horizontal");


        Vector3 forward = cube.transform.forward;
        forward.y = 0.0f;
        forward.Normalize();

        Vector3 right = cube.transform.right;


        Vector3 dir = forward * moveZ - right * moveX;
        dir.Normalize();

        dir *= moveSpeed * Time.deltaTime;

        transform.position += dir;

        //Kamera yönü kodu
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        if (x != 0 || y != 0)
        {

            curXRot = curXRot + (-y * rotateSpeed);
            curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);

            transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + (x * rotateSpeed), 0.0f);
        }

        //Koþma Kodu
        
        if (moveX != 0 || moveZ != 0)
        {
            if(Input.GetKey(KeyCode.LeftShift) && canRun == true)
            {
                moveSpeed = 15;
                sprintTime -= sprintFloors * Time.deltaTime;

                isRunning = true;

                if(sprintTime <= 0)
                {
                    canRun = false;
                }
                
            }

            if(Input.GetKeyUp(KeyCode.LeftShift) || canRun == false) 
            { 
                moveSpeed = 10;
                isRunning = false;
            }

        }
    }

    
}

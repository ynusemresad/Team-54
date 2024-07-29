using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MovementTest : MonoBehaviourPunCallbacks
{
    //Not : SprintTime s�f�rland���nda en az 20 olana kadar oyuncu tekrar ko�amaz.

    PhotonView pw;

    public GameObject cube;
    public float moveSpeed; //hareket h�z�

    public Camera cam;

    public Rigidbody rb;

    public bool canRun = true; //ko�mas� i�in izin
    float sprintTime = 100; //stamina s�resi
    public float sprintFloors; //sprintTime'in artma ve azalma katsay�s�
    public bool isRunning = false; //ko�up ko�mad���n� kontrol ediyor

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
        //serverda sadece bizde olmas� istenilen de�i�iklikler bu if'in i�inde yaz�l�r
        if (pw.IsMine == true)
        {
            //sprintTime'� 100 ile 0 aras�nda tutmak i�in
            sprintTime = Mathf.Clamp(sprintTime, 0.0f, 100.0f);

            //hareket fonkisyonunu �a��r�r
            move();

            //de�erleri kontrol etmek i�in console'da yazd�r�r
            Debug.Log("sprint time : " + sprintTime + " canRun : " + canRun + " isRunning :" + isRunning);

            //sprintTime'in artmas� ve kontrol� i�in
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

        //Kamera y�n� kodu
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        if (x != 0 || y != 0)
        {

            curXRot = curXRot + (-y * rotateSpeed);
            curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);

            transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + (x * rotateSpeed), 0.0f);
        }

        //Ko�ma Kodu
        
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

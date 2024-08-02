using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Movement : MonoBehaviourPunCallbacks
{
    PhotonView pw;

    public GameObject cube;
    public float moveSpeed;


    public Rigidbody rb;
    public float zipla;
    public bool ziplamaActive;
    float mv;


    public float minXRot;
    public float maxXRot;

    private float curXRot;
    public float rotateSpeed;

    private float curZoom;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pw = GetComponent<PhotonView>();

        if (pw.IsMine == true)
        {
            GetComponent<Renderer>().material.color = Color.green;
            //GameObject cam = PhotonNetwork.Instantiate("Camera", new Vector3(2.34f, 1.43f, -0.66f), new Quaternion(-0.08f, 0.73f, -0.09f, -0.66f), 0, null); 

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (pw.IsMine == true)
        {
            move();

        }

    }


    void move()
    {
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


        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        if (x != 0 || y != 0)
        {

            curXRot = curXRot + (-y * rotateSpeed);
            curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);

            transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + (x * rotateSpeed), 0.0f);
        }
    }
}

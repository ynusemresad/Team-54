using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ManagementCode : MonoBehaviourPunCallbacks
{

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    void Update()
    {

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("server'a girildi!");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby'ye girildi!");

        PhotonNetwork.JoinOrCreateRoom("oda", new RoomOptions { MaxPlayers = 5, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya girildi!");

        GameObject nesne = PhotonNetwork.Instantiate("Gamer", Vector3.zero, Quaternion.identity, 0, null);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Odadan çıkıldı!");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Lobby'den çıkıldı!");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Hata : Odaya Girilemedi!");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Hata : Herhangi Bir Odaya Girilemedi!");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Hata : Oda oluşturulamadı!");
    }


}

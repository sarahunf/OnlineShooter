using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Player = Photon.Realtime.Player;

namespace Photon.Scripts.Managers
{
    public class ConnectPhotonManager : MonoBehaviourPunCallbacks
    {
        public static ConnectPhotonManager ME;

        private void Awake()
        {
            if (ME != null && ME != this)
            {
                Destroy(gameObject);
            }
            else
            {
                ME = this;
                DontDestroyOnLoad(ME);
            }
        }

        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            ScenesManager.ME.LoadScene(ScenesManager.MainMenu);
        }

        public void CreateRoom(string room, byte maxCount = 1)
        {
            var roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = maxCount;
            CurrentGameStatics.MAX_PLAYER_COUNT = maxCount;
            PhotonNetwork.CreateRoom(room);
        }

        public void JoinRoom(string room)
        {
            if (!AllPlayerJoined())
                PhotonNetwork.JoinRoom(room);
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel(ScenesManager.Game);
        }

        public void SaveNickName(string nickname)
        {
            PhotonNetwork.NickName = nickname;
        }

        public String GetNickName()
        {
            return PhotonNetwork.NickName;
        }

        public String GetOtherNickName(PhotonView view)
        {
            return view.Owner.NickName;
        }

        public void InstantiateObject(string ojbName, Vector3 position, Quaternion rotation)
        {
            PhotonNetwork.Instantiate(ojbName, position, rotation);
        }

        public void DestroyObject(GameObject go)
        {
            Destroy(go);
        }

        public bool AllPlayerJoined()
        {
            return PhotonNetwork.IsMasterClient &&
                   PhotonNetwork.CurrentRoom.PlayerCount >= CurrentGameStatics.MAX_PLAYER_COUNT;
        }

        public int PlayersInRoomCount()
        {
            return PhotonNetwork.CurrentRoom.Players.Count;
        }

        public Player[] PlayersInRoom()
        {
            return PhotonNetwork.PlayerList;
        }
    }
}

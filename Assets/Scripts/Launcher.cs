using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Tcc.Asym
{

    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields
            [SerializeField]
            private byte maxPlayersPerRoom = 4;
        #endregion

        #region Private Fields

        /// <summary>
        /// Versão do cliente, que separa os usuários de versões diferentes.
        /// </summary>
        string gameVersion = "1";

        #endregion


        #region MonoBehaviour CallBacks

        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }


        #endregion

        #region Public Methods

        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster()
        {
            Debug.Log("Conectado a master");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("Disconectado de master por: {0}", cause);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Não conseguiu entrar na sala. Criando sala nova");

            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions{ MaxPlayers = maxPlayersPerRoom});
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Entrou na sala.");
        }
        #endregion
    }

}
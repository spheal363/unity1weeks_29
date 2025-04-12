using Unity.Netcode;
using UnityEngine;

public class NetworkLauncher : MonoBehaviour {
    private void OnGUI() {
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer) {
            if (GUI.Button(new Rect(10, 10, 200, 50), "Host")) {
                NetworkManager.Singleton.StartHost();
            }
            if (GUI.Button(new Rect(10, 70, 200, 50), "Client")) {
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour {
    [SerializeField] private BasePlayer[] players;  // 0: Shooting, 1: Roguelike

    private float switchTimerMax = 30f;
    private float switchTimer = 29f;

    private bool isSwapped = false; // ����؂�ւ��t���O

    private void Start() {
        if (IsServer) {
            UpdatePlayerControlClientRpc(isSwapped);
        }
    }

    private void Update() {
        if (!IsServer) return;

        switchTimer += Time.deltaTime;
        if (switchTimer > switchTimerMax) {
            switchTimer = 0f;
            isSwapped = !isSwapped;
            UpdatePlayerControlClientRpc(isSwapped);
            Debug.Log($"[GameManager] Role switched. isSwapped: {isSwapped}");
        }
    }

    // �S�N���C�A���g�ɑ΂��āA�ǂ���𑀍삷�ׂ�����ʒm
    [ClientRpc]
    private void UpdatePlayerControlClientRpc(bool currentSwapState) {
        ulong localId = NetworkManager.Singleton.LocalClientId;

        bool isHost = NetworkManager.Singleton.IsHost;
        bool controlShooting = (!currentSwapState && isHost) || (currentSwapState && !isHost);

        players[0].SetControlEnabled(controlShooting);
        players[1].SetControlEnabled(!controlShooting);
    }
}
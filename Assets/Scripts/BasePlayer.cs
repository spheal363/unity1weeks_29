using Unity.Netcode;
using UnityEngine;

public abstract class BasePlayer : NetworkBehaviour {
    protected NetworkVariable<bool> isMoving = new NetworkVariable<bool>(false,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    protected bool isControlEnabled = false;

    public bool IsMoving() {
        return isMoving.Value;
    }

    public void SetControlEnabled(bool enabled) {
        isControlEnabled = enabled;
        // 入力や動作のON/OFFに必要ならここで入力処理の有効/無効も切り替える
    }
}
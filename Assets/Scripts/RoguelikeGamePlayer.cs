using Unity.Netcode;
using UnityEngine;

public class RoguelikeGamePlayer : BasePlayer {
    public static RoguelikeGamePlayer Instance { get; private set; }

    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private InputManager inputManager;

    private NetworkVariable<Vector2> inputVector = new NetworkVariable<Vector2>(
        Vector2.zero,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Update() {
        Debug.Log($"[RougeGamePlayer] Update: {isControlEnabled}");
        if (isControlEnabled && !IsServer) {
            Debug.Log("**********");
            // Debug.Log($"[ShootingGamePlayer] SetMoveInputServerRpc: {inputManager.GetMovementVectorNormalized()}");
            SetMoveInputServerRpc(inputManager.GetMovementVectorNormalized());
            return;
        } else if (isControlEnabled && IsServer) {
            inputVector.Value = inputManager.GetMovementVectorNormalized();
            HandleMovement();
            Debug.Log("############");
        }

        // 操作権がないホストは移動制御のみ行う
        if (!isControlEnabled && IsServer) {
            // プレイヤーの移動を制御する
            HandleMovement();
            Debug.Log("++++++++++++++++++++++++++++++");
        }
    }


    [ServerRpc(RequireOwnership = false)]
    private void SetMoveInputServerRpc(Vector2 inputVector) {
        this.inputVector.Value = inputVector;
    }

    private void HandleMovement() {
        Vector3 moveDir = new Vector3(inputVector.Value.x, 0f, inputVector.Value.y);
        float moveDistance = moveSpeed * Time.deltaTime;

        transform.position += moveDir * moveDistance;
        isMoving.Value = inputVector.Value != Vector2.zero;

        float rotateSpeed = 10f;

        if (moveDir != Vector3.zero) {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
    }
}

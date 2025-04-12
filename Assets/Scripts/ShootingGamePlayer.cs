using UnityEngine;
using Unity.Netcode;

public class ShootingGamePlayer : BasePlayer {
    public static ShootingGamePlayer Instance { get; private set; }

    private RectTransform playerRectTransform;
    [SerializeField] private float moveSpeed = 120.0f;
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
        playerRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isControlEnabled && !IsServer)
        {
            SetMoveInputServerRpc(inputManager.GetMovementVectorNormalized());
            return;
        }
        else if (isControlEnabled && IsServer) {
            inputVector.Value = inputManager.GetMovementVectorNormalized();
            HandleMovement();
        }

        // ���쌠���Ȃ��z�X�g�͈ړ�����̂ݍs��
        if (!isControlEnabled && IsServer) {
            // �v���C���[�̈ړ��𐧌䂷��
            HandleMovement();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetMoveInputServerRpc(Vector2 inputVector) {
        this.inputVector.Value = inputVector;
    }

    private void HandleMovement() {
        Vector3 moveDirection = new Vector3(inputVector.Value.x, inputVector.Value.y, 0f);
        float moveDistance = moveSpeed * Time.deltaTime;

        playerRectTransform.localPosition += moveDirection * moveDistance;

        isMoving.Value = inputVector.Value != Vector2.zero;
    }

    public float Horizontal() {
        return inputVector.Value.x;
    }
}
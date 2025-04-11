using UnityEngine;

public class RoguelikeGamePlayer : BasePlayer {
    public static RoguelikeGamePlayer Instance { get; private set; }

    private Transform playerTransform;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private GameInputManager gameInputManager;

    private Vector2 inputVector;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Update() {
        // ÉvÉåÉCÉÑÅ[ÇÃà⁄ìÆÇêßå‰Ç∑ÇÈ
        HandleMovement();
    }

    private void HandleMovement() {
        inputVector = gameInputManager.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;

        this.transform.position += moveDirection * moveDistance;

        isMoving = inputVector != Vector2.zero;
    }
}

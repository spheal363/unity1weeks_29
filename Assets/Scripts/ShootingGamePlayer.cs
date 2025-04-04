using UnityEngine;

public class ShootingGamePlayer : BasePlayer {
    public static ShootingGamePlayer Instance { get; private set; }

    private RectTransform playerRectTransform;
    [SerializeField] private float moveSpeed = 120.0f;
    [SerializeField] private GameInputManager gameInputManager;

    private Vector2 inputVector;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
        playerRectTransform = GetComponent<RectTransform>();
    }

    private void Update() {
        // ÉvÉåÉCÉÑÅ[ÇÃà⁄ìÆÇêßå‰Ç∑ÇÈ
        HandleMovement();
    }

    private void HandleMovement() {
        inputVector = gameInputManager.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, inputVector.y, 0f);
        float moveDistance = moveSpeed * Time.deltaTime;

        playerRectTransform.localPosition += moveDirection * moveDistance;

        isMoving = inputVector != Vector2.zero;
    }

    public float Horizontal()
    {
        return inputVector.x;
    }
}
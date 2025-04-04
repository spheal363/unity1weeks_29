using UnityEngine;

public class GameInputManager : MonoBehaviour {
    public static GameInputManager Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    private void Awake() {
        Instance = this;

        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();
    }

    private void OnDestroy() {
        playerInputActions.Dispose();
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}

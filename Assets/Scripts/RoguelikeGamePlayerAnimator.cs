using UnityEngine;

public class RoguelikeGamePlayerAnimator : MonoBehaviour
{
    private const string IS_RUNNING = "IsRunning";

    [SerializeField] private BasePlayer player;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetBool(IS_RUNNING,
            player.IsMoving());
    }
}

using UnityEngine;

public class ShootingGamePlayerAnimator : MonoBehaviour {
    private const string IS_MOVING = "IsMoving";
    private const string HORIZONTAL = "Horizontal";

    [SerializeField] private BasePlayer player;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetBool(IS_MOVING,
            player.IsMoving());

        if (player is ShootingGamePlayer shootingGamePlayer) {
            animator.SetFloat(HORIZONTAL, shootingGamePlayer.Horizontal());
        }
    }
}

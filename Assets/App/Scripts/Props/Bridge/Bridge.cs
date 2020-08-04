using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Bridge : MonoBehaviour {
    [SerializeField]
    private TriggerZone zone;

    private Animator animator;

    void Awake() {
        this.zone.onInteractionEvent += this.OnBridgeActivate;

        this.animator = this.GetComponent<Animator>();
    }

    private void OnBridgeActivate() {
        Debug.Log("Activating bridge...");

        this.animator.SetBool("isOpen", !(animator.GetBool("isOpen")));
    }
}

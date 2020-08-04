using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour {
    [SerializeField]
    private TriggerZone zone;

    private Animator animator;

    void Awake() {
        this.zone.onInteractionEvent += this.OnDoorOpen;

        this.animator = this.GetComponent<Animator>();
    }

    private void OnDoorOpen() {
        Debug.Log("Opening door...");

        this.animator.SetBool("isOpen", !(animator.GetBool("isOpen")));
    }

}

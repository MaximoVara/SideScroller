using UnityEngine;

public class TriggerZone : MonoBehaviour {
    public event System.Action onInteractionEvent;

    //unity will invoke
    private void OnTriggerEnter(Collider collider) {
        Debug.Log(collider.name + " entered zone . . .");

        // Check to see if the thing that is inside of the collider, is in fact
        // something that can handle the interaction.
        var iInteractiveHandler = collider.GetComponent<IInteractiveHandler>();

        if(iInteractiveHandler == null) {
            return;
        }

        iInteractiveHandler.OnHandleInteraction(this.OnInteractionExecuted);
    }

    private void OnTriggerExit(Collider collider) {
        Debug.Log(collider.name + " exited zone . . .");

        var iInteractiveHandler = collider.GetComponent<IInteractiveHandler>();

        if (iInteractiveHandler == null) {
            return;
        }

        iInteractiveHandler.OnHandleInteraction(null);
    }

    private void OnInteractionExecuted() {
        Debug.Log("Execute Interaction...");

        this.onInteractionEvent?.Invoke();
    }
}

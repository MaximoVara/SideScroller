public interface IInteractiveHandler {
    /// <summary>
    /// This method will get called by some interactable entity and pass
    /// a callback for you to invoke, in your own manner, to let the interactive
    /// object know when you have interacted with it.
    /// </summary>
    /// <param name="interact"></param>
    void OnHandleInteraction(System.Action interact);
}
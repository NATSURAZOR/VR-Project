using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Notch : XRSocketInteractor
{
    // SerializeField - this attribute is sparingly used, most commonly to display private variables in the inspector.
    // [Range(x, y)] tells Unity that you want to restrict that field to a particular range of numbers, x and y.
    [SerializeField, Range(0, 1)] private float releaseThreshold = 0.25f;

    public Bow Bow { get; private set; }
    public PullMeasurer PullMeasurer { get; private set; }

    // lambda operator. If hamstring string is pulled tight enough that we have true -  we can shoot, false -  can't
    public bool CanRelease => PullMeasurer.PullAmount > releaseThreshold;

    // Start is called on the frame when a script is enabled just before any of the Update methods are called the first time. Like the Awake function, Start is called exactly once in the lifetime of the script. However, Awake is called when the script object is initialised, regardless of whether or not the script is enabled.
    protected override void Awake()
    {
        // This is what Awake do
        base.Awake();

        // This is our override for this function
        Bow = GetComponentInParent<Bow>();
        PullMeasurer = GetComponentInChildren<PullMeasurer>();
    }

    // This function is called when the object becomes enabled and active.
    protected override void OnEnable()
    {
        // This is what OnEnable do
        base.OnEnable();

        // Here is our ovveride
        PullMeasurer.selectExited.AddListener(ReleaseArrow);
    }

    // This function is called when the behaviour becomes disabled.
    // This is also called when the object is destroyed and can be used for any cleanup code.When scripts are reloaded after compilation has finished, OnDisable will be called, followed by an OnEnable after the script has been loaded
    protected override void OnDisable()
    {
     
        base.OnDisable();

        PullMeasurer.selectExited.RemoveListener(ReleaseArrow);
    }

    // SelectExitEventArgs - Event data associated with the event when an Interactor ends selecting an Interactable.
    public void ReleaseArrow(SelectExitEventArgs args)
    {
        // hasSelection - Indicates whether this Interaction is currently selecting an Interactable. In other words, returns whether interactablesSelected contains any Interactables.
        if (hasSelection)
        {
            // interactionManager - The XRInteractionManager that this Interactor will communicate with (will find one if null).
            interactionManager.SelectExit(this, firstInteractableSelected);
        }
    }

    // ProcessInteracto - The XRInteractionManager calls this method to update the Interactor after interaction events occur.
    public override void ProcessInteractor(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        // The phase in which updates happen.
        // Dynamic - Called every frame.Corresponds with the MonoBehaviour.Update method.
        // Fixed - Frame-rate independent. Corresponds with the MonoBehaviour.FixedUpdate method.
        // Late - Called  at the end of every frame. Corresponds with the MonoBehaviour.LateUpdate method.
        // OnBeforeRender - Called just before render.Corresponds with the UnityEngine.Application.onBeforeRender callback.
        base.ProcessInteractor(updatePhase);

        if (Bow.isSelected)
        {
            UpdateAttach();
        }
    }

    public void UpdateAttach()
    {
        // Move attach when bow is pulled, this updates the renderer as well
        attachTransform.position = PullMeasurer.PullPosition;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        // We check for the hover here too, since it factors in the recycle time of the socket
        // We also check that notch is ready, which is set once the bow is picked up
        return QuickSelect(interactable) && CanHover(interactable) && interactable is Arrow && Bow.isSelected;
    }

    private bool QuickSelect(IXRSelectInteractable interactable)
    {
        // This lets the Notch automatically grab the arrow
        return !hasSelection || IsSelecting(interactable);
    }

    private bool CanHover(IXRSelectInteractable interactable)
    {
        if (interactable is IXRHoverInteractable hoverInteractable)
            return CanHover(hoverInteractable);

        return false;
    }
}

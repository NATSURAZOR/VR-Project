using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;

public class PullMeasurer : XRBaseInteractable
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    public float PullAmount { get; private set; } = 0.0f;
    
    // lambda operation, 
    public Vector3 PullPosition => Vector3.Lerp(start.position, end.position, PullAmount);

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        PullAmount = 0;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        // when  hamstring is grab
        if (isSelected)
        {   
            // call updatePull function
            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                updatePull();
            }
        }
    }

    private void updatePull()
    {
        // Use the interactor's position to calculate amount
        Vector3 interactorPosition = firstInteractorSelecting.transform.position;

        // Figure out the new pull value, and it's position in space
        PullAmount = CalculatePull(interactorPosition);
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        // direction
        Vector3 pullDirection = pullPosition - start.position;
        // vector of direction ( from hamstring )
        Vector3 targetDirection = end.position - start.position;

        // The magnitude is the distance between the vector's origin (0,0,0) and its endpoint
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        
        // dot product of 2 vectors
        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;

        // Clamp(float value, float min, float max); return number beetwen 0 and 1
        return Mathf.Clamp(pullValue, 0.0f, 1.0f);
    }

    private void OnDrawGizmos()
    {
        // if start end end is not null than create line between this 2 points
        if (start && end)
        {
            Gizmos.DrawLine(start.position, end.position);
        }
    }
}

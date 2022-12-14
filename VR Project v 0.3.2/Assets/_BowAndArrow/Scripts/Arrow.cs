
using BNG;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : XRGrabInteractable
{
    [SerializeField] private float speed = 4000.0f;

    private new Rigidbody rigidbody;
    private ArrowCaster caster;
    public int arrowDamage = 50;

    private bool launched = false;


    private RaycastHit hit;

    protected override void Awake()
    {
        // default of Awake
        base.Awake();

        // our override
   
        rigidbody = GetComponent<Rigidbody>();
        caster = GetComponent<ArrowCaster>();
   
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        // default of Awake
        base.OnSelectExited(args);
        GetComponent<Collider>().enabled = false;
        // our override
        // check if arg is type of Notch
        if (args.interactorObject is Notch notch)
        {
            // return true if PullMeasurer.PullAmount > releaseThreshold
            if (notch.CanRelease)
                LaunchArrow(notch);
        }
    }

    // if arrow is launched than you can not launch another arrow to notch
    private void LaunchArrow(Notch notch)
    {
        // Starts a Coroutine.
        // The execution of a coroutine can be paused at any point using the yield statement.When a yield statement is used, the coroutine pauses execution and automatically resumes at the next frame.
        // Yielding of any type, including null, results in the execution coming back on a later frame, unless the coroutine is stopped or has completed.
        launched = true;
        
        ApplyForce(notch.PullMeasurer);
        StartCoroutine(LaunchRoutine());
    }

    private void ApplyForce(PullMeasurer pullMeasurer)
    {  
        // Adds a force to the Rigidbody.
        // The effects of the forces applied with this function are accumulated at the time of the call. The physics system applies the effects during the next simulation run (either after FixedUpdate, or when the script explicitly calls the Physics.Simulate method)
        rigidbody.AddForce(transform.forward * (pullMeasurer.PullAmount * speed));
    }

    // The IEnumerator variable used earlier to create the coroutine.
    private IEnumerator LaunchRoutine()
    {
        // Set direction while flying
        // Check for raycast component and if it don't hit anything than call function setDirection of the arrow
        while (!caster.CheckForCollision(out hit))
        {
            SetDirection();
            yield return null;
        }

        // Once the arrow has stopped flying
        // if it hit something than we need to disable the physics
        DisablePhysics();
        // let's child the arrow to whatever it hit 
        ChildArrow(hit);
        // check for hittable interface ( it's good for substract damage from enemy )
        // we get the hit we try to see if component on that object implements  the arrow hittable interface and we call hit on it
        CheckForHittable(hit);
    }

    private void SetDirection()
    {
        if (rigidbody.velocity.z > 0.5f)
            transform.forward = rigidbody.velocity;
    }

    private void DisablePhysics()
    {
        // on kinematick for object
        rigidbody.isKinematic = true;
        // off gravity for object
        rigidbody.useGravity = false;
    }

    private void ChildArrow(RaycastHit hit)
    {
    
        transform.SetParent(hit.transform);
        
    }

    private void CheckForHittable(RaycastHit hit)
    {
        string nameObject = hit.transform.name.ToLower();
        if (nameObject.Contains("breakable"))
        {
            Damageable d = hit.transform.gameObject.GetComponent<Damageable>();
            if (d)
            {
                d.DealDamage(arrowDamage);
                DestroyGameObject();
            }
        }

        string tagObject = hit.transform.tag.ToLower();
        if (tagObject.Contains("enemy"))
        {
            Damageable d = hit.transform.gameObject.GetComponent<Damageable>();
            
            if (d)
            {
                d.DealDamage(arrowDamage);
                DestroyGameObject();

            }
            
        }
       

        if (hit.transform.TryGetComponent(out IArrowHittable hittable))
        {

            hittable.Hit(this);

        }
            

        
    }


    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return base.IsSelectableBy(interactor) && !launched;
    }


    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}

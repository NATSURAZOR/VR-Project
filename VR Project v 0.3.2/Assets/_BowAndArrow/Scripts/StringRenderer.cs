using UnityEngine;

[ExecuteInEditMode]
public class StringRenderer : MonoBehaviour
{
    [Header("Render Positions")]
    [SerializeField] private Transform start;
    [SerializeField] private Transform middle;
    [SerializeField] private Transform end;

    private LineRenderer lineRenderer;

    // Start is called on the frame when a script is enabled just before any of the Update methods are called the first time. Like the Awake function, Start is called exactly once in the lifetime of the script. However, Awake is called when the script object is initialised, regardless of whether or not the script is enabled.
    private void Awake()
    {
   
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // Application.isEditor - Returns true if the game is being run from the Unity editor; false if run from any deployment target.
        // Application.isPlaying - Returns true when called in any kind of built Player, or when called in the Editor in Play Mode(Read Only).
        // In a built Player, this method will always return true.
        // In the Editor, it will return true if the Editor is in Play Mode.
        if (Application.isEditor && !Application.isPlaying)
            UpdatePositions();
    }

    // This function is called when the object becomes enabled and active.
    private void OnEnable()
    {
        Application.onBeforeRender += UpdatePositions;
    }

    // This function is called when the behaviour becomes disabled.
    // This is also called when the object is destroyed and can be used for any cleanup code.When scripts are reloaded after compilation has finished, OnDisable will be called, followed by an OnEnable after the script has been loaded
    private void OnDisable()
    {
        Application.onBeforeRender -= UpdatePositions;
    }

    private void UpdatePositions()
    {
        // Set positions of line renderer, middle position is the notch attach transform
        lineRenderer.SetPositions(new Vector3[] { start.position, middle.position, end.position });
    }
}

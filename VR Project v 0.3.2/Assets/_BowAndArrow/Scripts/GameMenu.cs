using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class GameMenu : MonoBehaviour
{
    [SerializeField]float m_AxisToPressThreshold = 0.5f;
    InputDevice m_InputDevice;
    [SerializeField]
    XRNode m_ControllerNode = XRNode.RightHand;
    public GameObject gameMenu;
    public LineRenderer leftRenderer;
    public LineRenderer rightRenderer;
    public Material materialMenu;
    public Material defaultMaterial;
    private GameObject light;
    public GameObject bow;

    private float time;
    public float reClickTime = 0.5f;
    public XRNode controllerNode
    {
        get => m_ControllerNode;
        set => m_ControllerNode = value;
    }
    public InputDevice inputDevice => m_InputDevice.isValid ? m_InputDevice : m_InputDevice = InputDevices.GetDeviceAtXRNode(controllerNode);

    // Start is called before the first frame update
    void Start()
    {
        light = GameObject.FindGameObjectsWithTag("Light")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPressed(InputHelpers.Button.SecondaryButton))
        {
            if (time == 0.0f || time >= reClickTime)
            {
                time = 0.0f;
                if (!gameMenu.activeSelf)
                {
                    time += Time.deltaTime;
                    gameMenu.SetActive(true);
                    leftRenderer.material = materialMenu;
                    rightRenderer.material = materialMenu;
                    light.GetComponent<Light>().intensity = 0.5f;
                    bow.SetActive(false);
                    
                }
                else if (gameMenu.activeSelf)
                {
                    time += Time.deltaTime;
                    gameMenu.SetActive(false);
                    leftRenderer.material = defaultMaterial;
                    rightRenderer.material = defaultMaterial;
                    light.GetComponent<Light>().intensity = 1.5f;
                    bow.SetActive(true);
                   
                }
            }
            time += Time.deltaTime;
        }


    }

    protected virtual bool IsPressed(InputHelpers.Button button)
    {
        inputDevice.IsPressed(button, out var pressed, m_AxisToPressThreshold);
        return pressed;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class gravity : MonoBehaviour
{
    public InputActionAsset InputAction;
    private InputAction grab;
    private float veloctiy;
    private Rigidbody2D rb;
    //private InputAction release;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grab = InputSystem.actions.FindAction("Interact");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grab.WasPressedThisFrame())
        {
            rb.simulated = true; //gravity affects objects when "simulated" is active
        }
        //enable gravity
        //disable gravity
    }
}

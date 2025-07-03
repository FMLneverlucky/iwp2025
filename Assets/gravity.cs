using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class gravity : MonoBehaviour
{
    public InputActionAsset InputAction;
    private InputAction grab;
    private InputAction release;
    private Rigidbody2D rb;

    [SerializeField]
    private Tilemap playingField;
    private BoxCollider2D objectCollider;
    void Start()
    {
        grab = InputSystem.actions.FindAction("Interact");
        release = InputSystem.actions.FindAction("Interact2");
        rb = GetComponent<Rigidbody2D>();
        objectCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //enable gravity
        if (grab.WasPressedThisFrame() && !rb.simulated)
        {
            rb.gravityScale = 1;
            rb.simulated = true; //gravity affects objects when "simulated" is active
        }
        else if (release.WasPressedThisFrame() && !rb.simulated)
        {
            rb.gravityScale = -1; //this gravity scale does make object move upwards, just having issues making this work after grabbing action
            rb.simulated = true;
        }
        Constrained();
    }

    private bool Constrained()
    {
        BoxCollider2D fieldCollider = playingField.GetComponent<BoxCollider2D>();
        float fieldboundY = fieldCollider.bounds.min.y;
        float objectboundY = objectCollider.bounds.min.y;
        if (objectboundY < fieldboundY)
        {
            rb.gravityScale = 0;
            rb.simulated = false;
        }
        return rb.simulated;
    }
}

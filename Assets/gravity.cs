using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class gravity : MonoBehaviour
{
    public InputActionAsset InputAction;
    private InputAction grab;
    private InputAction release;
    private Rigidbody2D rb;
    private enum gravityState
    {
        None = 0,
        antiGravity,
        gravity
    }
    private gravityState currentState = gravityState.None;

    [SerializeField]
    private Tilemap playingField;
    private BoxCollider2D objectCollider;
    bool boundaryHit = false; //the actual bool to check if bubbles reached any playingfield boundary
    bool detectedTop = true;
    //ooga booga scuffed method, but basically instead of making another enum to check which side bubbles constrained at, use 0 or 1 of bool to act as budget enum 

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
        if (grab.WasPressedThisFrame() && !rb.simulated && detectedTop)   //bubbles not hitting top boundary of playing field
        {
            rb.gravityScale = 1;
            currentState = gravityState.gravity;
            rb.simulated = true; //gravity affects objects when "simulated" is active
        }
        else if (release.WasPressedThisFrame() && !rb.simulated && !detectedTop) //bubbles hitting top boundary of playing field
        {
            rb.gravityScale = -1; //this gravity scale does make object move upwards, just having issues making this work after grabbing action
            currentState = gravityState.antiGravity;
            rb.simulated = true;
        }
        Constrained(currentState);
        float objectMinY = objectCollider.bounds.min.y;
        float objectMaxY = objectCollider.bounds.max.y;
        Mathf.Clamp(objectMinY, playingField.GetComponent<BoxCollider2D>().bounds.min.y, (playingField.GetComponent<BoxCollider2D>().bounds.max.y));
    }

    private bool Constrained(gravityState state)
    {
        BoxCollider2D fieldCollider = playingField.GetComponent<BoxCollider2D>();
        float fieldBoundsY = 0; //temp value, will be assigned in switch case below
        float objectBoundsY = 0;

        switch (state)
        {
            case gravityState.None:
                break;
            case gravityState.antiGravity:
                {
                    //check top of boxes
                    fieldBoundsY = fieldCollider.bounds.max.y;
                    objectBoundsY = objectCollider.bounds.max.y;
                    if (objectBoundsY > fieldBoundsY) //top of bubble collider is above playing field
                    {
                        boundaryHit = true;
                        detectedTop = true;
                    }
                    if (boundaryHit && detectedTop)
                    {
                        rb.gravityScale = 0;
                        currentState = gravityState.None;
                        rb.simulated = false;
                    }

                    break;
                }
            case gravityState.gravity:
                {
                    //check bottom of boxes
                    fieldBoundsY = fieldCollider.bounds.min.y;
                    objectBoundsY = objectCollider.bounds.min.y;
                    if (objectBoundsY < fieldBoundsY) //bottom of bubble collide is below playing field
                    {
                        boundaryHit = true;
                        detectedTop = false;
                    }
                    if (boundaryHit && !detectedTop)
                    {
                        rb.gravityScale = 0;
                        currentState = gravityState.None;
                        rb.simulated = false;
                    }

                    break;
                }
        }
        return rb.simulated;
    }
}

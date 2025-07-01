using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 Pos;
    private float Direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Pos = rb.transform.position;
        Direction = .0f;
        Debug.Log(Direction.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        Direction = InputSystem.actions.FindAction("Move").ReadValue<float>();
        Debug.Log(Direction);
        rb.AddForce((Pos.x + Direction), 0);
        transform.position.x + Direction;//only moving in one axis and direction value on changes when -1 or 1
    }
}

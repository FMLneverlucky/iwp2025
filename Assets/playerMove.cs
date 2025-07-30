using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;


public class playerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    //private float hDirection, vDirection = 0;
    private Vector2 Direction = new Vector2(0, 0);
    private InputAction shooting, moving;
    private BulletManager bulManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooting = InputSystem.actions.FindAction("shoot");
        moving = InputSystem.actions.FindAction("Move");
        bulManager = BulletManager.GetManagerInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting.IsPressed())
        {
            bulManager.CreateBullet();
        }
        bulManager.Update();

        if (moving.WasPressedThisFrame())
        {
            rb.simulated = true;
            Direction = InputSystem.actions.FindAction("Move").ReadValue<Vector2>();
            Debug.Log(Direction);
            rb.linearVelocity = Direction * 4;
            var pos = transform.position;
            pos.x = Mathf.Clamp(transform.position.x, -(Camera.main.pixelWidth), Camera.main.pixelWidth);
            pos.y = Mathf.Clamp(transform.position.y, -(Camera.main.pixelHeight), Camera.main.pixelHeight);
            transform.position = pos;
            return;
        }

        if (moving.WasPerformedThisDynamicUpdate())
        {
            rb.simulated = false;
        }

    }
}

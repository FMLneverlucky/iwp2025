using UnityEngine;

public abstract class abstractBubble : MonoBehaviour
{
    enum types
    {
        air,
        red,
        green,
        blue,
        yellow
    }
    private types type;
    private Collider2D collider;
    protected bool collided = false;
    abstractBubble()
    {
        type = types.air;
    }
}

using UnityEngine;

public abstract class abstractBubble : MonoBehaviour
{
    public enum types
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
    public abstractBubble()
    {
        type = types.air;
    }

    public types GetBubbleType()
    { 
        return type; 
    }
}

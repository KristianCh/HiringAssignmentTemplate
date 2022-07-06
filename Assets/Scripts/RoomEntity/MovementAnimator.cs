using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class controlling movement between two point
public class MovementAnimator
{
    // Animation parameters
    public float Speed;
    public float T;
    public Vector3 StartPosition;
    public Vector3 EndPosition;

    // Initialize values
    public MovementAnimator(Vector3 startPosition, Vector3 endPosition, float speed = 1)
    {
        StartPosition = startPosition;
        EndPosition = endPosition;
        Speed = speed;
        T = 0;
    }

    // Interpolate between positions and add an arc
    // Returns position vector, z value is -1 if finished
    public Vector3 UpdateAnimationPosition(float deltaTime)
    {
        T = Mathf.Min(1, T + Speed * deltaTime);

        if (T >= 1)
        {
            return new Vector3(EndPosition.x, EndPosition.y, -1);
        }
        return Vector3.Lerp(StartPosition, EndPosition, T) + new Vector3(0, Mathf.Sin(T * Mathf.PI) * 2, 0);
    }
}
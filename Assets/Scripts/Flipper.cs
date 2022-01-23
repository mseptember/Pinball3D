using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class Flipper : MonoBehaviour
{
    [SerializeField] float startPosition = 0f;
    [SerializeField] float endPosition = 45f;
    [SerializeField] float power = 10;
    [SerializeField] float damper = 1;

    HingeJoint joint;
    JointSpring spring;
    JointLimits limits;

    public enum Sides
    {
        LEFT,
        RIGHT
    }

    public Sides side;

    public int direction; // positive or negative

    void Start()
    {
        joint = GetComponent<HingeJoint>();
        //SPRING
        joint.useSpring = true;
        spring = new JointSpring();
        spring.spring = power;
        spring.damper = damper;

        //LIMITS
        joint.useLimits = true;
        limits = new JointLimits();
        limits.min = startPosition;
        limits.max = endPosition * direction;
        joint.limits = limits;
    }

    void FixedUpdate()
    {
        if (side == Sides.LEFT)
        {
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                spring.targetPosition = endPosition * direction;
            }
            else
            {
                spring.targetPosition = startPosition;
            }
        }

        if (side == Sides.RIGHT)
        {
            if(Input.GetKey(KeyCode.RightArrow))
            {
                spring.targetPosition = endPosition * direction;
            }
            else
            {
                spring.targetPosition = startPosition;
            }
        }

        joint.spring = spring;
    }
}

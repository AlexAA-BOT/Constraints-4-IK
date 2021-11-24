using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorMovement : MonoBehaviour
{
    public GameObject original;
	


    [Header("Angle Constraint")]
    public bool AngleConstraintActive;
    public bool CancelTwist;
    [Range(0.0f, 180.0f)]
    public float maxAngle;

    [Range(0.0f, 180.0f)]
    public float minAngle;

    public Transform parent;
    public Transform child;


    [Header("Plane Constraint")]
    public bool PlaneConstraintActive;
    public bool debugLines;

    public Transform plane;

    // To define how "strict" we want to be
    private float threshold = 0.00001f;




    void Update ()
    {
        float angle;
        Vector3 axis;
        
        Quaternion toRotateSwing = Swing(original.transform.localRotation);
        toRotateSwing.ToAngleAxis(out angle, out axis);
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        Quaternion swing = Quaternion.AngleAxis(angle, axis);
        transform.localRotation = Twist(original.transform.localRotation) * swing;
    }

    private Quaternion Twist(Quaternion _rot)
    {
        return new Quaternion(0, _rot.y, 0, _rot.w).normalized;
    }
    private Quaternion Swing(Quaternion _rot)
    {
        return Quaternion.Inverse(Twist(_rot)) * _rot;
    }
}

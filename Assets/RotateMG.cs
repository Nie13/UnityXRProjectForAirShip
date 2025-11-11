using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RotateMG : MonoBehaviour
{
    [SerializeField] private Transform gunHandleTransform;
    [SerializeField] private float rotationSpeed = 10f;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rigidbody;
    private float xRotationInput;
    private float yRotationInput;
    private HingeJoint horizontalRotationJoint;
    private JointMotor horizontalRotationMotor;
    private Transform verticalRotationPivot;

    private void Awake()
    {
        // Get the necessary components
        grabInteractable = GetComponent<XRGrabInteractable>();
        rigidbody = GetComponent<Rigidbody>();
        horizontalRotationJoint = gunHandleTransform.GetComponent<HingeJoint>();
        horizontalRotationMotor = horizontalRotationJoint.motor;
        verticalRotationPivot = gunHandleTransform.Find("SupportFrame/GunPivot");
    }

    private void Update()
    {
        // Check if the grabbable part of the gun is being held by an interactor
        if (grabInteractable.isSelected && grabInteractable.selectingInteractor is XRDirectInteractor)
        {
            // Get the input values from the VR controller
            xRotationInput = Input.GetAxis("XRControllerXRotation");
            yRotationInput = Input.GetAxis("XRControllerYRotation");

            // Rotate the machine gun horizontally (left to right) around the Y-axis
            float horizontalTargetRotation = horizontalRotationJoint.angle - xRotationInput * rotationSpeed;
            horizontalRotationMotor.targetVelocity = horizontalTargetRotation;
            horizontalRotationJoint.motor = horizontalRotationMotor;

            // Rotate the gun vertically (up and down) around the pivot point
            Quaternion verticalTargetRotation = Quaternion.Euler(-yRotationInput * rotationSpeed, 0, 0);
            verticalRotationPivot.localRotation *= verticalTargetRotation;
        }
    }
}

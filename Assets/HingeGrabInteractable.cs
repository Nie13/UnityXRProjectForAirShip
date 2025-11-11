using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HingeGrabInteractable : XRGrabInteractable
{
    [SerializeField] private Transform gripAttachmentPoint;

    private Quaternion originalRotation;
    private Vector3 originalPosition;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        originalRotation = args.interactor.attachTransform.localRotation;
        originalPosition = args.interactor.attachTransform.position;

        args.interactor.attachTransform.rotation = gripAttachmentPoint.rotation;
        args.interactor.attachTransform.position = gripAttachmentPoint.position;

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        args.interactor.attachTransform.localRotation = originalRotation;
        args.interactor.attachTransform.position = originalPosition;
    }
}

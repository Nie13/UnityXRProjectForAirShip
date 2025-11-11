using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunRotationController : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor interactor;
    [SerializeField] private GunGrabTrigger gunGrabTrigger;
    [SerializeField] private Transform gripAttachmentPoint;
    [SerializeField] private Transform hingeGrip;
    [SerializeField] private Transform hingeSupport;
    public float minGripAngle = -45f;
    public float maxGripAngle = 45f;
    public float minSupportAngle = -45f;
    public float maxSupportAngle = 45f;


    private bool isGrabbing;

    private void Awake()
    {
        gunGrabTrigger.OnGrabStateChanged += HandleGrabStateChanged;
    }

    private void OnDestroy()
    {
        gunGrabTrigger.OnGrabStateChanged -= HandleGrabStateChanged;
    }

    private void Update()
    {
        if (isGrabbing)
        {
            Debug.Log("is grabbed");
            RotateGrip();
            RotateSupport();
        }
    }

    private void HandleGrabStateChanged(bool grabState)
    {
        isGrabbing = grabState;
    }

    private void RotateGrip()
    {
        Vector3 toInteractor = interactor.transform.position - hingeGrip.position;
        Vector3 projected = Vector3.ProjectOnPlane(toInteractor, hingeSupport.up).normalized;
        Vector3 currentDirection = Vector3.ProjectOnPlane(hingeGrip.forward, hingeSupport.up).normalized;

        float angle = Vector3.SignedAngle(currentDirection, projected, hingeSupport.up);
        float clampedAngle = Mathf.Clamp(angle, minGripAngle, maxGripAngle);

        hingeGrip.RotateAround(hingeGrip.position, hingeSupport.up, clampedAngle);
        Debug.Log("Rotate Grip Angle: " + angle + " clampedAngle: " + clampedAngle);
    }

    private void RotateSupport()
    {
        Vector3 toInteractor = interactor.transform.position - hingeSupport.position;
        Vector3 projected = Vector3.ProjectOnPlane(toInteractor, hingeSupport.right).normalized;
        Vector3 currentDirection = Vector3.ProjectOnPlane(hingeSupport.forward, hingeSupport.right).normalized;

        float angle = Vector3.SignedAngle(currentDirection, projected, hingeSupport.right);
        float clampedAngle = Mathf.Clamp(angle, minSupportAngle, maxSupportAngle);

        hingeSupport.RotateAround(hingeSupport.position, hingeSupport.right, clampedAngle);
        Debug.Log("Rotate Support Angle: " + angle + " clampedAngle: " + clampedAngle);
    }



}

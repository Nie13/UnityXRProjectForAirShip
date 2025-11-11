using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MachineGunRotation : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor interactor;
    [SerializeField] private GunGrabTrigger trigger;

    private bool isGrabbing = false;


    private void Start()
    {
    }

    private void Update()
    {
        if (isGrabbing)
        {
            Debug.Log("Rotating machine gun.");
            RotateMachineGun();
        }
    }


    private void RotateMachineGun()
    {
        Vector3 toInteractor = interactor.transform.position - transform.position;
        Vector3 projected = Vector3.ProjectOnPlane(toInteractor, transform.right);
        transform.rotation = Quaternion.LookRotation(projected, transform.up);
        Debug.Log("Rotate Machine Gun Interactor: " + toInteractor + "  projected:  " + projected);
    }
}

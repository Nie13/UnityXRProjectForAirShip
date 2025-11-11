using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GunGrabTrigger : MonoBehaviour
{
    [SerializeField] private ActionBasedController controller;

    public event Action<bool> OnGrabStateChanged;

    private bool wasGripped = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.gameObject.name);
        if (other.TryGetComponent<XRBaseInteractor>(out XRBaseInteractor interactor) && interactor == controller.GetComponent<XRBaseInteractor>())
        {
            OnGrabStateChanged?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit: " + other.gameObject.name);
        if (other.TryGetComponent<XRBaseInteractor>(out XRBaseInteractor interactor) && interactor == controller.GetComponent<XRBaseInteractor>())
        {
            OnGrabStateChanged?.Invoke(false);
        }
    }

    private void Update()
    {
        bool isGripped = controller.activateAction.action.ReadValue<float>() >= 0.5f;
        if (isGripped != wasGripped)
        {
            Debug.Log("Grip button state changed: " + isGripped);
            wasGripped = isGripped;
        }
    }
}

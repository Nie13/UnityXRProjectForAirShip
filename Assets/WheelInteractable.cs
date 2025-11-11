using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[Serializable]
public class WheelEvent : UnityEvent<float> { }
public class WheelInteractable : XRBaseInteractable
{
    public Transform leftPosition = null;
    public Transform rightPosition = null;
    public Transform midPosition = null;

    [HideInInspector]
    public float dragPercent = 0.0f;

    protected XRBaseInteractor m_interactor = null;

    public UnityEvent onDragStart = new UnityEvent();
    public UnityEvent onDragEnd = new UnityEvent();
    public DragEvent onDragUpdate = new DragEvent();

    Coroutine m_drag = null;


    void StartDrag()
    {
        if (m_drag != null)
        {
            StopCoroutine(m_drag);
        }
        m_drag = StartCoroutine(CalculateDrag());
        onDragStart?.Invoke();
    }

    void EndDrag()
    {
        if (m_drag != null)
        {
            StopCoroutine(m_drag);
            m_drag = null;
            onDragEnd?.Invoke();            
        }
    }

    public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
    {
        Vector3 AB = b - a;
        Vector3 AV = value - a;
        return Mathf.Clamp01(Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB));
    }


    IEnumerator CalculateDrag()
    {
        if (m_interactor != null)
        {

            Vector3 line = rightPosition.localPosition - leftPosition.localPosition;
            Vector3 interactorLocalPosition = rightPosition.parent.InverseTransformPoint(m_interactor.transform.position);
            Vector3 projectedPoint = Vector3.Project(interactorLocalPosition, line.normalized);
            dragPercent = InverseLerp(rightPosition.localPosition, leftPosition.localPosition, projectedPoint);
            
            onDragUpdate?.Invoke(dragPercent);
            yield return null;
        }
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        m_interactor = interactor;
        StartDrag();
        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        EndDrag();
        m_interactor = null;
        base.OnSelectExited(interactor);
    }
}

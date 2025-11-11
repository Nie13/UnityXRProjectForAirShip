using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PushButton : MonoBehaviour
{
    public UnityEvent onPressed = new UnityEvent();
    public UnityEvent onReseted = new UnityEvent();

    public UnityEvent onInteractionStart = new UnityEvent();
    public UnityEvent onInteractionEnd = new UnityEvent();

    [Min(0.01f)]
    public float depressionDepth = 0.015f;

    [Min(0.0001f)]
    public float pressThreshold = 0.001f;

    [Min(0.0001f)]
    public float reseetThreshold = 0.001f;

    [Min(0.01f)]
    public float returnSpeed = 1.0f;

    float m_currentPressDepth = 0.0f;
    float m_yMax = 0.0f;
    float m_yMin = 0.0f;
    bool m_wasPressed = false;

    List<Collider> m_currentColliders = new List<Collider>();
    XRBaseInteractor m_interactpr = null;


    // Start is called before the first frame update
    void Start()
    {
        m_yMax = transform.localPosition.y;
    }

    void SetMinRange()
    {
        m_yMin = m_yMax - depressionDepth;
    }

    void SetHeight(float newHeight)
    {
        Vector3 currentPosition = transform.localPosition;
        currentPosition.y = newHeight;
        currentPosition.y = Mathf.Clamp(currentPosition.y, m_yMin, m_yMax);
        transform.localPosition = currentPosition;
    }

    bool IsPressed()
    {
        return transform.localPosition.y >= m_yMin && transform.localPosition.y <= m_yMin + pressThreshold;
    }

    bool IsReset()
    {
        return transform.localPosition.y >= m_yMax - reseetThreshold && transform.localPosition.y <= m_yMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_interactpr != null)
        {
            float newPressHeight = GetPressDepth(m_interactpr.transform.position);
            float deltaHeight = m_currentPressDepth - newPressHeight;
            float newPressedPosition = transform.localPosition.y - deltaHeight;

            SetHeight(newPressedPosition);

            if(!m_wasPressed && IsPressed())
            {
                onPressed?.Invoke();
                m_wasPressed = true;
            }

            m_currentPressDepth = newPressHeight;

        }
        else
        {
            if(!Mathf.Approximately(transform.localPosition.y, m_yMax))
            {
                float returnHeight = Mathf.MoveTowards(transform.localPosition.y, m_yMax, Time.deltaTime * returnSpeed);
                SetHeight(returnHeight);
            }
        }

        if(m_wasPressed && IsReset())
        {
            onReseted?.Invoke();
            m_wasPressed = false;
        }
    }

    float GetPressDepth(Vector3 interactorWorldPosition)
    {
        return transform.parent.InverseTransformPoint(interactorWorldPosition).y;
    }

    private void OnTriggerEnter(Collider other)
    {
        XRBaseInteractor interactor = other.GetComponentInParent<XRBaseInteractor>();

        if(interactor != null && !other.isTrigger)
        {
            m_currentColliders.Add(other);
            if(m_interactpr == null)
            {
                m_interactpr = interactor;
                SetMinRange();
                m_currentPressDepth = GetPressDepth(m_interactpr.transform.position);
                onInteractionStart?.Invoke();
            }
        }
    }

    void EndPress()
    {
        m_currentColliders.Clear();
        m_currentPressDepth = 0.0f;
        m_interactpr = null;
    }

    private void OnTriggerExit(Collider other)
    {
        if(m_currentColliders.Contains(other))
        {
            m_currentColliders.Remove(other);
            if(m_currentColliders.Count == 0)
            {
                onInteractionEnd?.Invoke();
                EndPress();
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public enum HandType
{
    Left,
    Right
}

public class Hand : MonoBehaviour
{
    public HandType type = HandType.Left;
    public bool isHidden { get; private set; } = false;

    public InputAction trackedAction = null;

    public InputAction gripAction = null;
    public InputAction triggerAction = null;

    public Animator handAnimator = null;

    int m_gripAmountParam = 0;
    int m_triggerAmountParam = 0;

    bool m_isCurrentlyTracket = false;

    List<Renderer> m_currentRenders = new List<Renderer>();

    Collider[] m_colliders = null;

    public bool isCollisionEnabled { get; private set; } = false;

    public XRBaseInteractor interactor = null;

    void Awake()
    {
        if(interactor == null)
        {
            interactor = GetComponentInParent<XRBaseInteractor>();
        }
    }

    private void OnEnable()
    {
        interactor.onSelectEntered.AddListener(OnGrab);
        interactor.onSelectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        interactor.onSelectEntered.RemoveListener(OnGrab);
        interactor.onSelectExited.RemoveListener(OnRelease);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_colliders = GetComponentsInChildren<Collider>().Where(childCollider => !childCollider.isTrigger).ToArray();
        trackedAction.Enable();
        m_gripAmountParam = Animator.StringToHash("GripAmount");
        m_triggerAmountParam = Animator.StringToHash("PointAmount");
        gripAction.Enable();
        triggerAction.Enable();
        Hide();
        
    }

    void UpdateAnimations()
    {
        float gripAmount = gripAction.ReadValue<float>();
        float pointAmount = triggerAction.ReadValue<float>();
        handAnimator.SetFloat(m_gripAmountParam, Mathf.Clamp01(gripAmount + pointAmount));        
        handAnimator.SetFloat(m_triggerAmountParam, pointAmount);

    }

    // Update is called once per frame
    void Update()
    {
        float isTracked = trackedAction.ReadValue<float>();
        if(isTracked == 1.0f && !m_isCurrentlyTracket)
        {
            m_isCurrentlyTracket = true;
            Show();
        } else if (isTracked == 0 && m_isCurrentlyTracket)
        {
            m_isCurrentlyTracket = false;
            Hide();
        }
        UpdateAnimations();
    }

    public void Show()
    {
        foreach (Renderer renderer in m_currentRenders)
        {
            renderer.enabled = true;
            //m_currentRenders.Add(renderer);
        }
        isHidden = false;
        EnableCollisions(true);
    }

    public void Hide()
    {
        m_currentRenders.Clear();
        Renderer[] renders = GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renders )
        {
            renderer.enabled = false;
            m_currentRenders.Add(renderer);
        }
        isHidden = true;
        EnableCollisions(false);
    }

    public void EnableCollisions(bool enabled)
    {
        if (isCollisionEnabled == enabled) return;

        isCollisionEnabled = enabled;
        foreach(Collider collider in m_colliders)
        {
            collider.enabled = isCollisionEnabled;
        }
    }

    void OnGrab(XRBaseInteractable grabbedObject)
    {
        HandControl ctrl = grabbedObject.GetComponent<HandControl>();
        if(ctrl != null)
        {
            if(ctrl.hideHand)
            {
                Hide();
            }
        }
    }

    void OnRelease(XRBaseInteractable grabbedObject)
    {
        HandControl ctrl = grabbedObject.GetComponent<HandControl>();
        if (ctrl != null)
        {
            if (ctrl.hideHand)
            {
                Show();
            }
        }
    }
}

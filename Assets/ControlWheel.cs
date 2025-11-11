using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWheel : MonoBehaviour
{
    public Transform leftOrientation = null;
    public Transform midOrientation = null;
    public Transform rightOrientation = null;

    MeshRenderer m_meshRenderer = null;



    private void Start()
    {
        m_meshRenderer = GetComponentInChildren<MeshRenderer>();
    }


    public void OnWheelRotateStart()
    {
        m_meshRenderer.material.SetColor("_Color", Color.red);

    }

    public void OnWheelRotateStop()
    {
        m_meshRenderer.material.SetColor("_Color", Color.white);
    }

    public void UpdateWheel(float percent)
    {
        Debug.Log("Wheel rotate percent: " + percent);
        transform.rotation = Quaternion.Slerp(leftOrientation.rotation, rightOrientation.rotation, percent);
    }



}

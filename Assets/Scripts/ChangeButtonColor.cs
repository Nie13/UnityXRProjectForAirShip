using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButtonColor : MonoBehaviour
{
    MeshRenderer m_meshrender = null;
    bool isEnabled = false;

    private void Awake()
    {
        m_meshrender = GetComponent<MeshRenderer>();
    }


    public void ChangeColor()
    {
        m_meshrender.material.SetColor("_Color", Random.ColorHSV(0, 1, 0.9f, 1, 0.9f, 1.0f));
    }

    public void ChangeEnableColor()
    {
        if (isEnabled)
        {
            isEnabled = false;
            m_meshrender.material.SetColor("_Color", Color.red);
        } else
        {
            isEnabled = true;
            m_meshrender.material.SetColor("_Color", Color.green);
        }
    }

    public void LogInteractionStarted()
    {
        Debug.Log("Interaction Started");
    }

    public void LogInteractionEnded()
    {
        Debug.Log("Interaction Ended");
    }
}

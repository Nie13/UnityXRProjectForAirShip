using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGControl : MonoBehaviour
{
    public Transform startOrientation = null;
    public Transform endOrientation = null;


    private void Start()
    {
    }

    public void OnLeverPullStart()
    {
    }

    public void OnLeverPullStop()
    {
    }

    public void UpdateLever(float percent)
    {
        transform.rotation = Quaternion.Slerp(startOrientation.rotation, endOrientation.rotation, percent);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGRightHandControl : MonoBehaviour
{
    public GameObject rightHandOnMG;
    // Start is called before the first frame update
    void Start()
    {
        rightHandOnMG = GameObject.FindGameObjectWithTag("RightHandVisual");
        rightHandOnMG.SetActive(false);
    }

    public void onMgGripStart()
    {
        rightHandOnMG.SetActive(true);
    }

    public void onMgGripStop()
    {
        rightHandOnMG.SetActive(false);
    }
}

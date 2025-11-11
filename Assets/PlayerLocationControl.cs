using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocationControl : MonoBehaviour
{
    public Vector3 location0;
    public Vector3 location1;
    public Vector3 location2;
    public Vector3 location3;
    public Vector3 location4;
    public Vector3 location5;

    public float rotateY0;
    public float rotateY1;
    public float rotateY2;
    public float rotateY3;
    public float rotateY4;
    public float rotateY5;

    public int currentPostion;

    public GameObject playerObject;

    private Vector3[] locations;
    private float[] rotations;

    public InputActionReference inputActionReferenceForward = null;
    public InputActionReference inputActionReferenceBackward = null;

    private void Awake()
    {
        inputActionReferenceForward.action.started += changePlayerPositionForward;
        inputActionReferenceBackward.action.started += changePlayerPositionBackward;
    }

    private void OnDestroy()
    {
        inputActionReferenceForward.action.started -= changePlayerPositionForward;
        inputActionReferenceBackward.action.started -= changePlayerPositionBackward;
    }


    // Start is called before the first frame update
    void Start()
    {
        locations = new[] {location0, location1, location2, location3, location4, location5};
        rotations = new[] { rotateY0, rotateY1, rotateY2, rotateY3, rotateY4, rotateY5 };
        playerObject = GameObject.FindGameObjectWithTag("Player");

    }


    public void changePlayerPositionForward (InputAction.CallbackContext context)
    {
        currentPostion++;
        if (currentPostion >= locations.Length)
        {
            currentPostion = 0;
        }

        playerObject.transform.localPosition = locations[currentPostion];
        playerObject.transform.Rotate(new Vector3(0, rotations[currentPostion], 0));
    }

    public void changePlayerPositionBackward(InputAction.CallbackContext context)
    {
        currentPostion--;
        if (currentPostion <= -1)
        {
            currentPostion = locations.Length - 1;
        }

        playerObject.transform.localPosition = locations[currentPostion];
        playerObject.transform.Rotate(new Vector3(0, rotations[currentPostion], 0));
    }



}

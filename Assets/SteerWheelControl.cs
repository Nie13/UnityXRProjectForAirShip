using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerWheelControl : MonoBehaviour
{

    /*public GameObject rightHand;
    private Transform rightHandOriginalParent;
    private bool rightHandOnWheel = false;
    //LeftHand
    public GameObject leftHand;
    private Transform leftHandOriginalParent;
    private bool leftHandOnWheel = false;

    public Transform[] snappPositions;

    //Wheels/objects to controll whith wheel
    public GameObject Vehicle;
    private Rigidbody VehicleRigidBody;

    public float currentSteeringWheelRotation = 0;

    //turn dampening, lower number makes the vehicle take longer time to reach target rotation
    //for vehicle to just copy steering wheel movement use high number like 9999;
    private float turnDampening = 10;


    public Transform directionalObject;


    // Start is called before the first frame update
    void Start()
    {
        VehicleRigidBody = Vehicle.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ReleaseHandsFromWheel();

        ConvertHandRotationToSteeringWheelRotation();

        TurnVehicle();

        currentSteeringWheelRotation = -transform.rotation.eulerAngles.z;
    }

    private void TurnVehicle()
    {
        //Turns Wheels compared to the steering wheel
        var turn = -transform.rotation.eulerAngles.z;
        if (turn < -350)
        {
            turn = turn + 360;
        }

        VehicleRigidBody.MoveRotation(Quaternion.RotateTowards(Vehicle.transform.rotation, Quaternion.Euler(0, turn, 0), Time.deltaTime * turnDampening));
    }

    private void ConvertHandRotationToSteeringWheelRotation()
    {
        if (rightHandOnWheel == true && leftHandOnWheel == false)
        {
            Quaternion newRot = Quaternion.Euler(0, Vehicle.transform.rotation.eulerAngles.y, rightHandOriginalParent.transform.rotation.eulerAngles.z);
            directionalObject.rotation = newRot;
            transform.parent = directionalObject;
        }
        else if (rightHandOnWheel == false && leftHandOnWheel == true)
        {
            Quaternion newRot = Quaternion.Euler(0, Vehicle.transform.rotation.eulerAngles.y, leftHandOriginalParent.transform.rotation.eulerAngles.z);
            directionalObject.rotation = newRot;
            transform.parent = directionalObject;
        }
        else
        if (rightHandOnWheel == true && leftHandOnWheel == true)
        {
            Quaternion newRotLeft = Quaternion.Euler(0, Vehicle.transform.rotation.eulerAngles.y, leftHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion newRotRight = Quaternion.Euler(0, Vehicle.transform.rotation.eulerAngles.y, rightHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion finalRot = Quaternion.Slerp(newRotLeft, newRotRight, 1.0f / 2.0f);
            directionalObject.rotation = finalRot;
            transform.parent = directionalObject;
        }
    }

    private void ReleaseHandsFromWheel()
    {
        //If you are using another interaction tool kit change OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch)
        //to your version of get up
        if (rightHandOnWheel == true && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {

            rightHand.transform.parent = rightHandOriginalParent;
            rightHand.transform.position = rightHandOriginalParent.position;
            rightHand.transform.rotation = rightHandOriginalParent.rotation;
            rightHandOnWheel = false;
        }
        //If you are using another interaction tool kit change OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch)
        //to your version of get up
        if (leftHandOnWheel == true && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            leftHand.transform.parent = leftHandOriginalParent;
            leftHand.transform.rotation = leftHandOriginalParent.rotation;
            leftHand.transform.position = leftHandOriginalParent.position;
            leftHandOnWheel = false;
        }

        if (leftHandOnWheel == false && rightHandOnWheel == false)
        {
            //reset steering wheel to not be parent of directional object if wheel is released
            transform.parent = transform.root;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            //Place RightHand
            //I use Oculus integration Change this "OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch)" to match whatever interaction SDK you are using.
            if (rightHandOnWheel == false && OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                PlaceHandOnWheel(ref rightHand, ref rightHandOriginalParent, ref rightHandOnWheel);
            }
            //Place left hand
            //Oculus integration Change this "OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch)" to match whatever interaction SDK you are using.
            if (leftHandOnWheel == false && OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
            {
                PlaceHandOnWheel(ref leftHand, ref leftHandOriginalParent, ref leftHandOnWheel);
            }
        }
    }

    private void PlaceHandOnWheel(ref GameObject hand, ref Transform originalParent, ref bool handOnWheel)
    {
        //Set variables to the first snapp position in array
        var shortestDistance = Vector3.Distance(snappPositions[0].position, hand.transform.position);
        var bestSnapp = snappPositions[0];
        //loop through all snapp positions
        foreach (var snappPosition in snappPositions)
        {
            //if no hand is child of this snapp position
            if (snappPosition.childCount == 0)
            {
                //distance between hand and snapp position
                var distance = Vector3.Distance(snappPosition.position, hand.transform.position);
                //if distance is shorter than current shortest distance
                if (distance < shortestDistance)
                {
                    //set this distance to the shortest adn this snapp to the bestsnapp
                    shortestDistance = distance;
                    bestSnapp = snappPosition;
                }
            }
        }
        //we need XHandOriginalParent to be able to reset hand after release
        originalParent = hand.transform.parent;

        //set best snapp as parent and hand position to snapp position
        hand.transform.parent = bestSnapp.transform;
        hand.transform.position = bestSnapp.transform.position;

        handOnWheel = true;
    }*/
}

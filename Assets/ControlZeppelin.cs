using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlZeppelin : MonoBehaviour
{

    public GameObject go;
    public Rigidbody rb;

    private float currentHeight;
    private float leftEngineSpeed;
    private float rightEngineSpeed;
    private float engineDiffDir;
    private float combinedSpeed;
    private float heightChange;
    private float currentDirDelta;

    // Start is called before the first frame update
    void Start()
    {
        currentHeight = go.transform.position.y;
        rb = gameObject.GetComponent<Rigidbody>();
        leftEngineSpeed = 1f;
        rightEngineSpeed = 1f;
        engineDiffDir = 0f;
        combinedSpeed = 1f;
        heightChange = 0f;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (leftEngineSpeed - rightEngineSpeed > 0.2f )
        {
            engineDiffDir = 0.5f;
        } else if (leftEngineSpeed - rightEngineSpeed < -0.2f)
        {
            engineDiffDir = -0.5f;
        } else
        {
            engineDiffDir = 0f;
        }

        if (currentDirDelta > 0.15f || currentDirDelta < -0.15f)
        {
            engineDiffDir += (currentDirDelta * 3f);
        }

        combinedSpeed = leftEngineSpeed + rightEngineSpeed;
        


        //Vector3 neededRotation = new Vector3(0, transform.rotation.y + engineDiffDir, 0);

        if (engineDiffDir != 0f)
        {
            //rb.MoveRotation(Quaternion.RotateTowards(go.transform.rotation, Quaternion.Euler(0, engineDiffDir, 0), Time.deltaTime * 10f));
            
            transform.Rotate(Vector3.up * engineDiffDir * Time.deltaTime);

            //Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, transform.rotation.y + engineDiffDir, 0), Time.deltaTime * 10f);
        }

        Vector3 newDir = transform.forward.normalized;

        if (transform.position.y != currentHeight)
        {
            if (transform.position.y < currentHeight)
            {
                heightChange = 1f;
                newDir += transform.up.normalized;
            }
            else
            {
                heightChange = -1f;
                newDir -= transform.up.normalized;
            }

        } else
        {
            heightChange = 0;
        }


    }

    public void directionControl(float direction)
    {
        float converDir = direction * 2 - 1f;
        currentDirDelta = converDir;
    }

    private void FixedUpdate()
    {
        transform.Translate(0, heightChange * Time.deltaTime, combinedSpeed * Time.deltaTime, Space.Self);
    }

    public void heightControl(float percentage)
    {
        float height = 100 + 200 * percentage;
        Vector3 currentTransform = transform.position;
        currentTransform.y = height;
        currentHeight = height;
    }

    public void leftEngineControl (float percentage)
    {
        float enginePercent = percentage * 1.2f - 0.2f;
        leftEngineSpeed = enginePercent;
    }

    public void rightEngineControl(float percentage)
    {
        float enginePercent = percentage * 1.2f - 0.2f;
        rightEngineSpeed = enginePercent;
    }
}

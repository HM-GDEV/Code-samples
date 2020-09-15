using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float accelerationRate;
    [SerializeField] [Range(0f, 5f)] float dampTime;
    [SerializeField] [Range(0f, 30f)] float maxVelocity;
    public GameObject startPoint;
    public Vector3 destination;
    public float currentVelocity = 0;
    public Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        destination = CalculateDestination(startPoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 forwardDir = -transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, forwardDir * 15, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, forwardDir, out hit, 15))
        {
            if(hit.transform.gameObject.tag == "AI_Car")
            {
                currentVelocity = Mathf.Lerp(currentVelocity, 0, 5 * Time.deltaTime);
            }
            else
            {
                
                if (currentVelocity < maxVelocity)
                {
                    currentVelocity += accelerationRate * Time.deltaTime;
                }
            }
        }
        else
        {
            
            if (currentVelocity < maxVelocity)
            {
                currentVelocity += accelerationRate * Time.deltaTime;
            }
        }
        currentVelocity = Mathf.Min(currentVelocity, maxVelocity);
        transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime*currentVelocity);
        Vector3 lookPos = transform.position - destination;
        lookPos.y = 0;
        var rot = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime*dampTime);
    }
    public Vector3 CalculateDestination(Vector3 pos)
    {
        return new Vector3(pos.x, gameObject.transform.position.y, pos.z);
    }

}

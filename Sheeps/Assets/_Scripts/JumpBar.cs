using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBar : MonoBehaviour
{
    [SerializeField]
    private float _jampForse;

    private void FixedUpdate()
    {
        //Debug.Log(transform.forward);   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sheeps")
        {
            //Debug.Log(-transform.eulerAngles);
            //Debug.Log(transform.eulerAngles);
            if (transform.InverseTransformDirection(other.transform.parent.forward).z > 0)
            {
                if(transform.InverseTransformPoint(other.transform.position).z<0)
                other.transform.parent.GetComponent<Sheeps>().JumpSheeps(_jampForse,transform.forward);
            }
            else
            {
                if (transform.InverseTransformPoint(other.transform.position).z > 0)
                    other.transform.parent.GetComponent<Sheeps>().JumpSheeps(_jampForse, -transform.forward);
            }
        }
    }


}

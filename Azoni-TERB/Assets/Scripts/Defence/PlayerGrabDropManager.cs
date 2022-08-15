using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabDropManager : MonoBehaviour
{
    [SerializeField] Transform objGrabPoint, objDropPoint;
    [SerializeField] float rayDist = 2f;

    private string [] states = {"Grabbing","Dropping"};
    public string currentState;
    GameObject hitObj = null, currentHitObj = null;

    TrashManager currenttrm = null;
    Transform obj = null;
    // Start is called before the first frame update
    void Start()
    {
        currentState = states[1];
        objGrabPoint = objGrabPoint.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDist)){
            hitObj =  hit.transform.gameObject;
            Vector3 forward = transform.TransformDirection(Vector3.forward) * rayDist;
            Debug.DrawRay(transform.position, forward, Color.green);       
        }else {
            hitObj = null;
        }
        GrabItem();
    }

    public void GrabDrop(){        
        if (hitObj != null){
            CheckHitObj();
        }else if (currentState == states[0] && currentHitObj != null){
            DropItem();
        }
    }

    void CheckHitObj(){
        if(hitObj.GetComponent<StationManager>() != null && hitObj.GetComponent<TrashManager>() == null){

            StationManager stm = hitObj.GetComponent<StationManager>();
            
            if(currentState == states[1] && stm.itemCount > stm.minItemCount){

                //or simply 
                currentState = states[0];
                //grab item
                GameObject obje = stm.RestItem();
                obj = obje.transform;
                currentHitObj = obje;

            }
            else if (currentState == states[0] && stm.itemCount <= stm.maxItemCount && stm.itemCount >= stm.minItemCount){


                currentState = states[1];
                Debug.Log("Deb");
                //leave item on top of the station
                obj = null;
                stm.AddItem(this.currentHitObj);
                currentHitObj = null;
            }else if (currentState == states[1] && stm.itemCount == stm.minItemCount){
                Debug.Log("noo");
            }

        } else if(hitObj.GetComponent<TrashManager>() != null && hitObj.GetComponent<StationManager>() == null){

            TrashManager trm = hitObj.GetComponent<TrashManager>();
            currenttrm = trm;
            Debug.Log("hit");
            if (currentState == states[1] && trm.currentState == trm.states[1]){
                currentState = states[0];
                trm.currentState = trm.states[0];
                //grab item
                currentHitObj = hitObj;
                obj = currentHitObj.transform;

            } 

        } else {

            //not an interactable object

        }
    }

    void GrabItem(){
        if (currentState == states[0] && obj != null){
            obj.position = objGrabPoint.position;
        }
    }

    void DropItem(){
        obj = null;
        currentState = states[1];
        currentHitObj.transform.position = objDropPoint.position;
        currentHitObj = null;        
        currenttrm.currentState = currenttrm.states[1];
        currenttrm = null;
    }
}

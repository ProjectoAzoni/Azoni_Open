using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabDropManager : MonoBehaviour
{
    // Point in space where you can drop an item and where you are gonna carry it
    [SerializeField] Transform objGrabPoint, objDropPoint;
    // distance to activate the object in fron of the player
    [SerializeField] float rayDist = 2f;
    // definition of Player states: grabbing and dropping an object
    private string [] states = {"Grabbing","Dropping"};
    // the current state of the player
    public string currentState;
    //definition of the object that has been hit and the past hit object
    GameObject hitObj = null, currentHitObj = null;
    // definition of the current object/trash manager of the hit object
    TrashManager currenttrm = null;
    // object to keep tack of the trash
    Transform obj = null;
    // At Start set the current player state to drop and get the transform component of the grab point 
    void Start()
    {
        currentState = states[1];
        objGrabPoint = objGrabPoint.GetComponent<Transform>();
    }

    // at Update, create a raycasthit 2m long and check if it's hiting something
    // if hitting something, get the object that has been hit and set it to the hitObj
    // if the ray cast does not hit something then set the hit object to null 
    // keep calling the grabTitem method
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

    // Button executed function that controls the grab and drop logic
    // check if there is a hit object and call the CheckHitObj method
    // check if there is no current hit object and the player is carrying an object, then drop the item   
    public void GrabDrop(){        
        if (hitObj != null){
            CheckHitObj();
        }else if (currentState == states[0] && currentHitObj != null){
            DropItem();
        }
    }
    
    
    
    //Check the inmediate hit object
    //Check if the object has a StationManager or TrashManager component
    // if object has StationManager component and not TrashManager
    //  then check if the player is holding an item or not
    //      if the player is holding an iten then put it in the station if the statios has enough lots
    //      if the player is not holding an item then ckeck if the station has items
    //          if the station has items then grab the last inserted item
    //          if the station does not have an item then then do nothing
    // if object has TrashManager component and not StationManager
    //  then ckeck the current player and object state
    //      if the state is grabbed then drop it
    //      if the state is dropped then grab it
    //      if the state is process then grab it --> means that the object is being processed on a station
    void CheckHitObj(){
        if(hitObj.GetComponent<StationManager>() != null && hitObj.GetComponent<TrashManager>() == null){

            StationManager stm = hitObj.GetComponent<StationManager>();
            
            if(currentState == states[1] && stm.itemCount > stm.minItemCount){

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

    //Handles the grab of the object if there is an object and the player state is grabbing
    void GrabItem(){
        if (currentState == states[0] && obj != null){
            obj.position = objGrabPoint.position;
        }
    }
    // handles the drop of the object that is being carried
    void DropItem(){
        obj = null;
        currentState = states[1];
        currentHitObj.transform.position = objDropPoint.position;
        currentHitObj = null;        
        currenttrm.currentState = currenttrm.states[1];
        currenttrm = null;
    }
}
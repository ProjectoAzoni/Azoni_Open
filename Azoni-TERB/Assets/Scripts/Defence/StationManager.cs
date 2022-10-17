using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationManager : MonoBehaviour
{
    [SerializeField] ItemsManager im;
    //point where the object/trash will float on top of the station
    [SerializeField] Transform objPoint, dropPoint;
    [SerializeField] GameObject stationCanvas;
    int timer;
    //definition of the item counter variables
    public int itemCount, minItemCount = 0, maxItemCount;

    string myType;
    //list to store the items/trash
    [SerializeField]List<GameObject> items = new List<GameObject>();

    //definition of the station types and their maximum item capability
    [SerializeField] Dictionary <string, int> types = new Dictionary<string, int>() {
        {"Wash",1},
        {"Dry",1},
        {"Shred",1},
        {"Melt",1},
        {"Compress",1},
        {"Station",1}
    } ;
    //at the start of the scene set the number of items to the minimun value
    // get the transform component of the object point
    // check the current station type and set the maximum item capablity
    void Start()
    {
        itemCount = 0;
        objPoint = objPoint.GetComponent<Transform>();
        dropPoint = dropPoint.GetComponent<Transform>();
        if (stationCanvas != null) {stationCanvas.SetActive(false);}
        CheckMyType();
    }

    // set the floating item to the first item in the list
    void Update()
    {
        if(itemCount > 0 && items.Count > 0){
            if (items[0].activeInHierarchy){
                items[0].transform.position = objPoint.position;
            }
            for(int i = 0; i < items.Count; i++){
                if(!items[i].activeInHierarchy){
                    itemCount--;
                    items.RemoveAt(i);
                }
            }
            if(stationCanvas.activeInHierarchy){
                if(items[0].GetComponent<TrashManager>().myTimer == 0f)
                {
                    stationCanvas.GetComponentInChildren<Slider>().value += Time.unscaledDeltaTime;
                    items[0].GetComponent<TrashManager>().myTimer = stationCanvas.GetComponentInChildren<Slider>().value;
                    
                }else {
                    stationCanvas.GetComponentInChildren<Slider>().value = items[0].GetComponent<TrashManager>().myTimer;
                    stationCanvas.GetComponentInChildren<Slider>().value += Time.unscaledDeltaTime;
                    items[0].GetComponent<TrashManager>().myTimer = stationCanvas.GetComponentInChildren<Slider>().value;
                    if (stationCanvas.GetComponentInChildren<Slider>().value >= stationCanvas.GetComponentInChildren<Slider>().maxValue)
                    {
                        List<string> myList = new List<string>(items[0].GetComponent<TrashManager>().characteristics);
                        myList.RemoveAt(0);
                        items[0].GetComponent<TrashManager>().characteristics = myList.ToArray();
                        items[0].GetComponent<TrashManager>().myTimer = 0f;
                        stationCanvas.GetComponentInChildren<Slider>().value = stationCanvas.GetComponentInChildren<Slider>().minValue;
                        stationCanvas.SetActive(false);
                    }
                }
                
            }
            
        } 
        else{
            if(stationCanvas.activeInHierarchy)
            {
                stationCanvas.GetComponentInChildren<Slider>().value = stationCanvas.GetComponentInChildren<Slider>().minValue;
                stationCanvas.SetActive(false);
            }
        }       
    }
    //ckeck the if the name and type of the station conicide and set the max item capability
    void CheckMyType(){
        foreach(var key in types.Keys){
            if (gameObject.name == key){
                maxItemCount = types[key];
                myType = key;
            }
        }
    }
    // add an item to the storage list
    public void AddItem(GameObject obj){
        int count = itemCount + 1;
        if (count <= maxItemCount){       
            CheckObj(obj);
        }   
    }

    private void CheckObj(GameObject obj)
    {
        TrashManager tm = obj.GetComponent<TrashManager>();
        if(tm.characteristics != null){
            for(int i = 0;i < tm.characteristics.Length; i++){
                if(tm.characteristics[i] == gameObject.name)
                {
                    if(tm.characteristics[0] == gameObject.name && tm.currentState == tm.states[2]){
                        tm.currentState = tm.states[2];
                        itemCount++;
                        items.Add(obj);
                        //set timer count up
                        CheckTimeType(obj);
                        return;
                    }
                }
            }
            if (tm.currentState == tm.states[2] && myType == "Station"){
                items.Add(obj);
                itemCount++;
            }
            else
            {
                //drop item
                obj.GetComponentInChildren<Animator>().SetTrigger("Idle1");
                tm.currentState = tm.states[1];
                obj.transform.position = dropPoint.position;
            }
        }
        
    }

    // Return the last object of the list when called and then delete it
    public GameObject RestItem(){
        GameObject obj;
        if (items.Count -1 >= 0){  
            obj = items[items.Count - 1];      
            items.RemoveAt(items.Count - 1);
            itemCount--;
        }else{
            return null;
        }   
        return obj;
    }

    void CheckTimeType(GameObject obj){
        TrashManager tm = obj.GetComponent<TrashManager>();
        timer = 20;
        /*if (myType == "Wash" && tm.myType == im.categories0[0]){
            timer = 20;
        }else if (myType == "Wash" && tm.myType == tm.types[2]){
            timer = 20;
        }else if (myType == "Wash" && tm.myType == tm.types[3]){
            timer = 20;
        }else if (myType == "Dry" && tm.myType == tm.types[0]){
            timer = 40;
        }else if (myType == "Dry" && tm.myType == tm.types[2]){
            timer = 40;
        }else if (myType == "Dry" && tm.myType == tm.types[3]){
            timer = 40;
        }else if (myType == "Shred" && tm.myType == tm.types[1]){
            timer = 30;
        }else if (myType == "Shred" && tm.myType == tm.types[4]){
            timer = 30;
        }*/
        stationCanvas.GetComponentInChildren<Slider>().minValue = 0;
        stationCanvas.GetComponentInChildren<Slider>().maxValue = timer;
        stationCanvas.SetActive(true);
    }
}

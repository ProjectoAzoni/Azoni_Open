using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    //point where the object/trash will float on top of the station
    [SerializeField] Transform objPoint;
    //definition of the item counter variables
    public int itemCount, minItemCount = 0, maxItemCount;
    //list to store the items/trash
    List<GameObject> items = new List<GameObject>();

    //definition of the station types and their maximum item capability
    Dictionary <string, int> types = new Dictionary<string, int>() {
        {"T1",3},
        {"T2",1},
        {"T3",2},
        {"T4",4},
        {"T5",1}
    } ;
    //at the start of the scene set the number of items to the minimun value
    // get the transform component of the object point
    // check the current station type and set the maximum item capablity
    void Start()
    {
        itemCount = 0;
        objPoint = objPoint.GetComponent<Transform>();
        CheckMyType();
    }

    // set the floating item to the first item in the list
    void Update()
    {
        if(itemCount > 0 && items.Count > 0){
            items[0].transform.position = objPoint.position;
        }
        
    }
    //ckeck the if the name and type of the station conicide and set the max item capability
    void CheckMyType(){
        foreach(var key in types.Keys){
            if (gameObject.name == key){
                maxItemCount = types[key];

            }
        }
    }
    // add an item to the storage list
    public void AddItem(GameObject obj){
        int count = itemCount + 1;
        if (count <= maxItemCount){
            itemCount++;
            items.Add(obj);
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
}

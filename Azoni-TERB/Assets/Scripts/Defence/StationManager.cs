using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    [SerializeField] Transform objPoint;
    public int itemCount, minItemCount = 0, maxItemCount;

    List<GameObject> items = new List<GameObject>();
    Dictionary <string, int> types = new Dictionary<string, int>() {
        {"T1",3},
        {"T2",1},
        {"T3",2},
        {"T4",4},
        {"T5",1}
    } ;
    // Start is called before the first frame update
    void Start()
    {
        itemCount = 0;
        objPoint = objPoint.GetComponent<Transform>();
        CheckMyType();
    }

    // Update is called once per frame
    void Update()
    {
        if(itemCount > 0 && items.Count > 0){
            items[0].transform.position = objPoint.position;
        }
        
    }

    void CheckMyType(){
        foreach(var key in types.Keys){
            if (gameObject.name == key){
                maxItemCount = types[key];

            }
        }
    }

    public void AddItem(GameObject obj){
        int count = itemCount + 1;
        if (count <= maxItemCount){
            itemCount++;
            items.Add(obj);
        }   
    }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{
    //este se explica solo :v
    public string [] types = {"Plastic","Carton","Metal","Glass","Paper","Organic", "Other"};
    public string myType;   
    public string [] throwPlaces = {"Green", "White","Black"};
    public string myThrowPlace;
    [SerializeField] public string [] characteristics;
    public string [] states = {"Grabbed","Dropped","Process"};
    public bool show = false;
    public string currentState;

    public float myTimer=0f;  
    // Start is called before the first frame update
    void Start()
    {
        currentState = states[1];
        SetMyType();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void SetMyType(){
        int num = Random.Range(0, types.Length-1);
        myType = types[num];
        if (num>=0 && num < 5){
            myThrowPlace = throwPlaces[1];
        }
        if(num == 5){
            myThrowPlace = throwPlaces[0];
        } 
        if (num == 6){
            myThrowPlace = throwPlaces[2];
        }
    }
}

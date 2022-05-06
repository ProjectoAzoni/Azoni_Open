using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandeler : MonoBehaviour
{
    public HealtControler myHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int dmg){
        myHealth.RestHealth(dmg);
    }
    void OnCollisionEnter(Collision collision){
        switch(collision.gameObject.tag)
        {
            case "B10":
                TakeDamage(4);
                return;
            case "B11":
                TakeDamage(8);
                return;
            case "B12":
                TakeDamage(16);
                return;
            case "B13":
                TakeDamage(32);
                return;
            case "B14":
                TakeDamage(64);
                return;
            case "B15":
                TakeDamage(64*2);
                return;
            case "B16":
                TakeDamage((64*2)*2);
                return;
        }
    }
}

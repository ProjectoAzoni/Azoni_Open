using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemTimerHandeler : MonoBehaviour
{
    [SerializeField] PlayerGrabDropManager pgd;
    [SerializeField] GameObject showPanel;
    [SerializeField] GameObject itemPanelPrefab;
    [SerializeField]List<GameObject> items = new List<GameObject>(); 
    [SerializeField]List<GameObject> panelItems = new List<GameObject>();
    [SerializeField]List<float> timers = new List<float>(); 

    [Header("Time for green, white and black")]
    [SerializeField]public int [] trashTimers;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(panelItems.Count > 0 & timers.Count > 0 & items.Count > 0){
            
            for(int i = 0; i < timers.Count; i++){
                panelItems[i].GetComponentInChildren<Slider>().value -= Time.unscaledDeltaTime;
                if (panelItems[i].GetComponentInChildren<Slider>().value <= 0){
                    
                    if (pgd.currentState == pgd.states[0]) {
                        pgd.currentState = pgd.states[1];
                    }
                    items[i].SetActive(false);
                    panelItems[i].SetActive(false);
                    items.RemoveAt(i);
                    panelItems.RemoveAt(i);
                    timers.RemoveAt(i);                    
                } 
            }
        }
    }
    public void AddItem(GameObject item){
        TrashManager tm = item.GetComponent<TrashManager>();
        if(tm.show == false){
            tm.show = true;
            items.Add(item);
            ShowItems(item);
        }     
    }
    public void RestItem(GameObject item){
        if(items.Count > 0){
            for(int i =0; i < items.Count; i++){
                if (item == items[i]){        
                    items.RemoveAt(i);
                    panelItems[i].SetActive(false);
                    panelItems.RemoveAt(i);
                    timers.RemoveAt(i);
                }
            }
        }
        
          
    }
    public void ShowItems(GameObject item){
        if (items.Count > 0){
            TrashManager tm = item.GetComponent<TrashManager>();
            int nume = 0;
            GameObject obj = Instantiate(itemPanelPrefab, new Vector3(showPanel.transform.position.x,showPanel.transform.position.y,showPanel.transform.position.z), Quaternion.identity, showPanel.transform);
            if (tm.myThrowPlace == tm.throwPlaces[0]){
                nume = 0;
                obj.GetComponent<Image>().color = new Color (0,255,0, 0.4f);
            }else if (tm.myThrowPlace == tm.throwPlaces[1]){
                nume = 1;
                obj.GetComponent<Image>().color = new Color (255,255,255, 0.4f);
            } else if (tm.myThrowPlace == tm.throwPlaces[2]){
                nume = 2;
                obj.GetComponent<Image>().color = new Color (0,0,0, 0.4f);
            }
            timers.Add(trashTimers[nume]);
            obj.GetComponentInChildren<Slider>().maxValue = trashTimers[nume];
            obj.GetComponentInChildren<Slider>().value = trashTimers[nume];
            panelItems.Add(obj);
           // GameObject obj = Instantiate(itemPanelPrefab, new Vector3(showPanel.transform.position.x,showPanel.transform.position.y,showPanel.transform.position.z), Quaternion.identity);
            //obj.transform.parent = showPanel.transform;
        }
    }
}

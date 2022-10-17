using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemTimerHandeler : MonoBehaviour
{
    [SerializeField] PlayerGrabDropManager pgd;
    [SerializeField] GameObject showPanel;
    [SerializeField] GameObject itemPanelPrefab;
    [SerializeField]public List<GameObject> items = new List<GameObject>(); 
    [SerializeField]public List<GameObject> panelItems = new List<GameObject>();
    [SerializeField]List<float> timers = new List<float>(); 
    [SerializeField] Sprite [] characSprites;

    [Header("Time for green, white and black")]
    [SerializeField]public int [] trashTimers;
    

    // Start is called before the first frame update
    void Start()
    {
        
         
    }

    // Update is called once per frame
    void Update()
    {   
        if(Time.timeScale == 1.0f){
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
                for (int k = 0; k < items.Count; k++)
                {
                    TrashManager tm = items[k].GetComponent<TrashManager>();
                    Transform [] objs = panelItems[k].GetComponentsInChildren<Transform>(true);
                    List<Transform> objsChar= new List<Transform>();
                    for (int m = 0; m < objs.Length; m++)
                    {
                        if(objs[m].name == "Wash"||objs[m].name == "Dry"||objs[m].name == "Shred"||objs[m].name == "Melt"||objs[m].name == "Compress"){
                            objsChar.Add(objs[m]);
                        }
                    }
                    if(objsChar.Count > 0){
                        for (int j = 0; j < objsChar.Count; j++){
                            objsChar[j].gameObject.SetActive(false);
                            for (int i = 0; i < tm.characteristics.Length; i++){
                                for (int h = 0; h < characSprites.Length; h++){
                                    if(tm.characteristics.Length > 0){
                                        if(tm.characteristics[i] == characSprites[h].name){
                                            if(tm.characteristics[i] == objsChar[j].name){
                                                objsChar[j].gameObject.SetActive(true);
                                                Image sp =  objsChar[j].GetComponent<Image>();
                                                sp.sprite = characSprites[h];
                                            }
                                        }
                                    }  
                                }
                            }         
                        }     
                    }
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
    public IEnumerator RestItem(GameObject item){
        if(items.Count > 0){
            for(int i =0; i < items.Count; i++){
                if (item == items[i]){  
                    panelItems[i].GetComponent<Animator>().SetTrigger("Exit");
                    yield return new WaitForSeconds(1);      
                    items.RemoveAt(i);
                    panelItems[i].SetActive(false);
                    panelItems.RemoveAt(i);
                    timers.RemoveAt(i);
                }
            }
        }     
    }
    public GameObject GetPanel(GameObject item){
        GameObject panel = null;
        if(items.Count > 0){
            for(int i =0; i < items.Count; i++){
                if (item == items[i]){  
                    panel =  panelItems[i];      
                }
            }
        }
        return panel;
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

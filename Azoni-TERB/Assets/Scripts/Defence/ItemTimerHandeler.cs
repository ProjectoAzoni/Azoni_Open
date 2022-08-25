using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTimerHandeler : MonoBehaviour
{
    [SerializeField] GameObject showPanel;
    [SerializeField] GameObject itemPanelPrefab;
    [SerializeField]List<GameObject> items = new List<GameObject>(); 
    [SerializeField]List<GameObject> panelItems = new List<GameObject>(); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(GameObject item){
        print("ok");
        items.Add(item);
        ShowItems();
    }
    public void RestItem(GameObject item){
        if(items.Count > 0){
            for(int i =0; i < items.Count; i++){
                if (item == items[i]){        
                    items.RemoveAt(i);
                    panelItems[i].SetActive(false);
                    panelItems.RemoveAt(i);
                }
            }
        }
        
          
    }
    public void ShowItems(){
        if (items.Count > 0){
            print("ok2");
           GameObject obj = Instantiate(itemPanelPrefab, new Vector3(showPanel.transform.position.x,showPanel.transform.position.y,showPanel.transform.position.z), Quaternion.identity, showPanel.transform);
           panelItems.Add(obj);
           // GameObject obj = Instantiate(itemPanelPrefab, new Vector3(showPanel.transform.position.x,showPanel.transform.position.y,showPanel.transform.position.z), Quaternion.identity);
            //obj.transform.parent = showPanel.transform;
        }
    }
}

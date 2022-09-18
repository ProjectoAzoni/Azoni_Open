using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    /* --------- listas y tablas de items ------------------- */
    static string [] bins = {"Organicos","Aprovechable","No aprobechable"};
    static string [] characteristics = {"Wash","Dry","Shred","Melt","Compress"};
    static string [] categories = {
                                    "Carton"
                                    ,"Papel"
                                    ,"vidrio"
                                    ,"Plastico"
                                    ,"Metalicos"
                                    ,"Textiles"
                                    ,"Madera"
                                    ,"Cuero"
                                    ,"Empaques compuestos"
                                    ,"papel tissue"
                                    ,"papel encerado"
                                    ,"papel plastificado"
                                    ,"papel metalizado"
                                    ,"ceramicos"
                                    ,"vidrio plano"
                                    ,"huesos"
                                    ,"material barrido"
                                    ,"colillas"
                                    ,"empaques sucios"
                                    ,"residuos comida"
                                    ,"materiales vegetales"
                                    ,"hojarasca"
                                };
    static string [,] clasificacion = {{categories[0],bins[1]}
                               ,{categories[1],bins[1]}
                               ,{categories[2],bins[1]}
                               ,{categories[3],bins[1]}
                               ,{categories[4],bins[1]}
                               ,{categories[5],bins[1]}
                               ,{categories[6],bins[1]}
                               ,{categories[7],bins[1]}
                               ,{categories[8],bins[1]}
                               ,{categories[9],bins[2]}
                               ,{categories[10],bins[2]}
                               ,{categories[11],bins[2]}
                               ,{categories[12],bins[2]}
                               ,{categories[13],bins[2]}
                               ,{categories[14],bins[2]}
                               ,{categories[15],bins[2]}
                               ,{categories[16],bins[2]}
                               ,{categories[17],bins[2]}
                               ,{categories[18],bins[2]}
                               ,{categories[19],bins[0]}
                               ,{categories[20],bins[0]}
                               ,{categories[21],bins[0]}};
    static string [,] items1 = {
         {"Pitillo",        clasificacion[3,0],clasificacion[3,1],characteristics[0], characteristics[1]}
        ,{"Bolsa",          clasificacion[3,0],clasificacion[3,1],characteristics[0], characteristics[1]}
        ,{"Tapa",           clasificacion[3,0],clasificacion[3,1],characteristics[0], characteristics[1]}
        ,{"Botella",        clasificacion[3,0],clasificacion[3,1],characteristics[0], characteristics[1]}
        ,{"Garrafa",        clasificacion[3,0],clasificacion[3,1],characteristics[0], characteristics[1]}
        ,{"envase",         clasificacion[3,0],clasificacion[3,1],characteristics[0], characteristics[1]}
        ,{"jeringa",        clasificacion[3,0],clasificacion[3,1],characteristics[0], characteristics[1]}
        ,{"guantes",        clasificacion[3,0],clasificacion[3,1],characteristics[0], characteristics[1]}
        ,{"empaque huevo",  clasificacion[3,0],clasificacion[3,1],characteristics[0], characteristics[1]}
        ,{"sillas",         clasificacion[3,0],clasificacion[3,1],characteristics[0], characteristics[1]}
        ,{"Empaque huevo",  clasificacion[0,0],clasificacion[0,1],"",""}
        ,{"Periodico",      clasificacion[0,0],clasificacion[0,1],"",""}
        ,{"Hojas",          clasificacion[0,0],clasificacion[0,1],"",""}
        ,{"Carpetas",       clasificacion[0,0],clasificacion[0,1],"",""}
        ,{"Cajas",          clasificacion[0,0],clasificacion[0,1],"",""}
        ,{"Pedazos",        clasificacion[2,0],clasificacion[2,1],"",""}
        ,{"Botella",        clasificacion[2,0],clasificacion[2,1],characteristics[0], characteristics[1]}
        ,{"zapato cuero",   clasificacion[7,0],clasificacion[7,1],characteristics[0], characteristics[1]}
        ,{"correa cuero",   clasificacion[7,0],clasificacion[7,1],characteristics[0], characteristics[1]}
        ,{"guantes cuero",  clasificacion[7,0],clasificacion[7,1],characteristics[0], characteristics[1]}
        ,{"pantalon",       clasificacion[5,0],clasificacion[5,1],characteristics[0], characteristics[1]}
        ,{"camisa",         clasificacion[5,0],clasificacion[5,1],characteristics[0], characteristics[1]}
        ,{"saco",           clasificacion[5,0],clasificacion[5,1],characteristics[0], characteristics[1]}
        ,{"bufanda",        clasificacion[5,0],clasificacion[5,1],characteristics[0], characteristics[1]}
        ,{"medias",         clasificacion[5,0],clasificacion[5,1],characteristics[0], characteristics[1]}
        ,{"gorras"  ,       clasificacion[5,0],clasificacion[5,1],characteristics[0], characteristics[1]}
        ,{"cubiertos",      clasificacion[3,0],clasificacion[3,1],characteristics[0], characteristics[1]}
        ,{"cubiertos",      clasificacion[6,0],clasificacion[6,1],characteristics[0], characteristics[1]}
        ,{"cubiertos",      clasificacion[4,0],clasificacion[4,1],characteristics[0], characteristics[1]}
        ,{"platos",         clasificacion[6,0],clasificacion[6,1],characteristics[0], characteristics[1]}
        ,{"platos",         clasificacion[3,0],clasificacion[3,1],characteristics[0], characteristics[1]}
        
    };
    
    public string [] bins0 = bins, characteristics0 = characteristics, categories0 = categories;

    [SerializeField] int itemNum = 10;
    [SerializeField] public GameObject ItemPrefab;
    [SerializeField] Transform ItemsSpawnStartPos, ParentObj;

    public List<GameObject> ItemsObj = new List<GameObject>();

    // Start is called before the first frame update

    void Awake(){
        for (int i = 0; i < itemNum; i++)
        {
            GameObject obj = Instantiate(ItemPrefab, ItemsSpawnStartPos.position, Quaternion.identity, ParentObj);
            ItemsObj.Add(obj);
        }
        setItems();
    }
    void Start()
    {
        
    }

    private void setItems()
    {
        for (int i = 0; i < ItemsObj.Count; i++)
        {
            int itemName = UnityEngine.Random.Range(0, items1.GetLength(0)-1);
            TrashManager tm = ItemsObj[i].GetComponent<TrashManager>();
            ItemsObj[i].name = items1[itemName,0];
            tm.myType = items1[itemName, 1];
            tm.myThrowPlace = items1[itemName,2];
            if (items1[itemName, 3] != "" &&  items1[itemName, 4]!=""){
                List<string> myList = new List<string>(tm.characteristics);
                myList.Add(items1[itemName, 3]);
                myList.Add(items1[itemName, 4]);
                tm.characteristics = myList.ToArray(); 
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
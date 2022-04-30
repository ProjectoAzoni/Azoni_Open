using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealtControler : MonoBehaviour
{
    [SerializeField] int vidMax;
    [SerializeField] Slider healtbar;

    public int vidaActual;
    // Start is called before the first frame update
    void Start()
    {
        healtbar.maxValue = vidMax;
        healtbar.value = healtbar.maxValue;
        vidaActual = vidMax;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestHealth(int damage) {
        int var = vidaActual - damage;
        if (var > 0) {
            vidaActual -=  damage;
            healtbar.value = vidaActual;
        }
        else if (var <= 0) {
            //Add points
            healtbar.value = healtbar.minValue;
            gameObject.SetActive(false);
            
        }
    }

    public void AddHealth (int health) {
        int var = vidaActual + health;
        if (var < vidMax) {
            vidaActual = var;
            healtbar.value = vidaActual / vidMax;
        } else if (var >= vidMax) {
            vidaActual = vidMax;
            healtbar.value = vidaActual / vidMax;
        }
    }
}

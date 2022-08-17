using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    private bool ActiveCamera = true;
    [SerializeField] CinemachineVirtualCamera vcam1;
    [SerializeField] CinemachineVirtualCamera vcam2;
    public GameObject canva;
    public string Scene;
    public float tiempo;
    // Start is called before the first frame update
    void Start()
    {
        vcam1.Priority = 1;
        vcam2.Priority = 0;
    }

    public void CameraSwitcher()
    {
        canva.SetActive(false);
        if (ActiveCamera)
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
        }
        else
        {
            vcam1.Priority = 1;
            vcam2.Priority = 0;
        }
        ActiveCamera = !ActiveCamera;
        StartCoroutine(WaitFor(tiempo));
    }

    IEnumerator WaitFor(float time)
    {
        yield return new WaitForSeconds(time);
        SceneController.GoToScene(Scene);
    }
}


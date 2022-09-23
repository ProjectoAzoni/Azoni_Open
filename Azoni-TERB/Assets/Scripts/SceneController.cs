using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    //[SerializeField] BasicSaveManager bsm;
    [SerializeField] Animator transition;
    [SerializeField] GameObject sceneLoader;
    [SerializeField] Slider slider;
    [SerializeField] BasicSaveManager bsm;
    [SerializeField] SettingsManager sm;

    //go to an scene by its name
   public void GoToScene(string scene) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    //Go to a scene by its build index
    public void GoToScene(int scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
    //Load the scene with the loading screen and transition
    public void GoToSceneAs(int scene) {
        //call the IEnumerator function
        StartCoroutine(LoadAsynchronously(scene));
        //set the saved volume in the 3 volume sliders
        string[] volumeParameter = sm.volumeParameter;
        for (int i = 0; i < volumeParameter.Length; i++)
        {
            sm.SetSliderValue(bsm.GetVolumeData(volumeParameter[i]), volumeParameter[i]);

        }
        
    }

    //Reload the current scene
    public void ReloadCurrentScene() {
        //Get the current active scene
        UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        //Load again the active scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.buildIndex);
    }

    //Get all the saved Stars to show in the Main Menu
    public void LoadDataMainMenu(int scene) { 
        
         
    }
    //Load the next scene whlie in loading screen
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        //Start the CrossFade_Start animation with the "Start" trigger
        //transition.SetTrigger("Start");
        //Wait for 1 second for the animation to end
        //yield return new WaitForSeconds(1);
        //load all the content of the new scene witout going to the scene
        AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
        //set the loading screen active in the hierarchy 
        //sceneLoader.SetActive(true);

        // while the scene is not done loading
        while (!op.isDone)
        {
            // show the proces of the loading in the loading bar
            //float progress = Mathf.Clamp01(op.progress / .9f);
            //slider.value = progress;
            yield return null;
        }
    }
}

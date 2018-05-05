using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class Fading : MonoBehaviour
{

    // the texture that will overlay the screen
    public Texture2D fadeTexture;

    // the fading speed
    public float fadeSpeed = 0.8f;

    // the texture's order
    private int drawDepth = -1000;  

    private float alpha = 1.0f;  
    
    // fade direction
    private int fadeDir = -1;   
    

    private AsyncOperation Async;

    void OnGUI()
    {

        // fade out/in the alpha value 
        alpha += fadeDir * fadeSpeed * Time.fixedDeltaTime;

        // force (Clamp) the number
        alpha = Mathf.Clamp01(alpha);

        // set Texture
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);                
        GUI.depth = drawDepth;                                                              
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);       

    }


    // sets fadeDir to the direction 
    public float BeginFade(int direction)
    {

        fadeDir = direction;
        return fadeSpeed;      
    }

    void OnLevelWasLoaded()
    {
        BeginFade(-1);          
    }


 
	//Load scene 
	
    public void LoadScene(string SceneName, float WaitFor = 0.6f)
    {

        StartCoroutine(ChangeScene(SceneName, WaitFor));
    }


    
	//Load scene asynchronously

    public void LoadSceneAsync(string SceneName, float WaitFor = 0.6f)
    {
        BeginFade(1);
        StartCoroutine(ChangeSceneAsync(SceneName, WaitFor));
    }


    
	//Change scene
    IEnumerator ChangeScene(string SceneName, float WaitFor = 0.6f)
    {

        // wait 
        yield return StartCoroutine(WaitForRealSeconds(WaitFor));

        // fade and load
        float fadeTime = BeginFade(1);
        yield return StartCoroutine(WaitForRealSeconds(fadeTime));

        SceneManager.LoadScene(SceneName);
    }


    
    //Change scene asynchronously
	
    IEnumerator ChangeSceneAsync(string SceneName, float WaitFor = 0.6f)
    {

        // wait
        yield return StartCoroutine(WaitForRealSeconds(WaitFor));

        // fade and load
        BeginFade(1);

        Async = SceneManager.LoadSceneAsync(SceneName);
        yield return Async;
    }


    
	//This function does not break when Time.timeScale is equal zero
	
    public static IEnumerator WaitForRealSeconds(float time)
    {

        float start = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < start + time)
        {

            yield return null;
        }
    }
}
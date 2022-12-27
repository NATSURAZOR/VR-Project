using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadController : MonoBehaviour
{
    public FadeSceen fadeScreen;
    private float endTimer = 4.0f;
    private float time = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Damageable d = transform.gameObject.GetComponent<Damageable>();
        if(d.Health <= 0)
        {
           
    
            time += Time.deltaTime;
            if ( time >= endTimer)
            {
                GoToScene(0);
            }
        }
        
    }

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }
    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // Launch the new scene
        SceneManager.LoadScene(sceneIndex);
    }

}

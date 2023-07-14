using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnEnable()
    {
       
        SceneManager.sceneUnloaded += OnSceneUnLoaded;

    }

    private void OnDisable()
    {
        
        SceneManager.sceneUnloaded -= OnSceneUnLoaded;

    }

   
    public void OnSceneUnLoaded(Scene scene)
    {
        Debug.Log("This Level seved");
        GameManager.instance.activePlayer.transform.position = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Scene scene = SceneManager.GetActiveScene();
            int scene_number = scene.buildIndex;

            SceneManager.LoadScene(scene_number + 1);
            GameManager.instance.setTimer(60);
            
            GameManager.instance.Reset();
        }
    }
}
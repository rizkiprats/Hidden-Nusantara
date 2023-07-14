using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject PanelEnding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PanelEnding.SetActive(true);
            Time.timeScale = 0;
        }
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

    public void nextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        int scene_number = scene.buildIndex;
        Time.timeScale = 1;

        SceneManager.LoadScene(scene_number + 1);
        GameManager.instance.setTimer(60);

        GameManager.instance.Reset();
    }
}

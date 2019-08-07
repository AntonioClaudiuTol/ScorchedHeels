using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerProgress: MonoBehaviour
{
    public Text progressText;
    private int counter = 0;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
    private Coroutine progressCoroutine;
    private List<GameObject> encounters;

    IEnumerator Progress()
    {
        while (true)
        {
            if (counter % 2 == 0)
            {
                encounters[counter / 2].GetComponent<Renderer>().material.color = Color.red;
            }
            counter++;
            progressText.text = counter.ToString();
            
            if(counter == 10)
            {
                StopCoroutine(progressCoroutine);
                progressText.text = "You Win!";
            }
            yield return waitForSeconds;
        }
    }

    void Start()
    {
        encounters = new List<GameObject>();
        GameObject[] gos = (GameObject[]) Resources.FindObjectsOfTypeAll(typeof(GameObject));
        for(int i=0; i< gos.Length; i++)
        {
            if(gos[i].name == "Encounter")
            {
                encounters.Add(gos[i]);
            }
        } 
        progressText.text = "0";
        progressCoroutine = StartCoroutine(Progress());
    }

    void Update()
    {
        
    }

    void OnGUI()
    {
        GUI.Label(new Rect(50, 50, 100, 20), "Hello World!");
    }
}

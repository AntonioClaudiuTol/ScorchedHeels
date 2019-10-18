using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class PlayerProgress: MonoBehaviour
{
    public Text progressText;
    private int counter = 0;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
    private Coroutine progressCoroutine;
    private List<GameObject> encounters;
    private int energy = 5;
    private int branch = 0;
    private int berry = 0;
    private int rock = 0;
    private int sharpRock = 0;
    private int grass = 0;
    private int rope = 0;
    private int ironOre = 0;

    private Button button;
    private String plot1 = "You wake up with a skullsplitting headache. Your vision is blurry and you have no idea where you are. Wait a minute, you don't even know WHO you are.";
    private string plot2 = "You find yourself in a forest glade. It's morning and the sun bathes you in its warm light. Birds chirp nearby, insects buzz and your stomach growls. It's time to get your bearings!";
    private GUIStyle guiStyle;
    private bool created = false;
    private bool hasSeenIntro = false;
    private String location;
    private bool startedGame = false;

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
        guiStyle.wordWrap = true;
        //encounters = new list<gameobject>();
        //gameobject[] gos = (gameobject[])resources.findobjectsoftypeall(typeof(gameobject));
        //for (int i = 0; i < gos.length; i++)
        //{
        //    if (gos[i].name == "encounter")
        //    {
        //        encounters.add(gos[i]);
        //    }
        //}
        //progresstext.text = "0";
        //progresscoroutine = startcoroutine(progress());
    }

    void Update()
    {
        
    }

    void OnGUI()
    {
        if(!hasSeenIntro)
        {
            showIntro();
        }
        if(location == "Glade")
        {
            showGlade();
        }

        if(startedGame)
        {
            if(energy > 0)
            {
                GUI.Label(new Rect(15, 15, 100, 20), "Energy:" + energy);
            }
            if(branch > 0)
            {
                GUI.Label(new Rect(15, 30, 100, 20), "Branch:" + branch);
            }
            if (berry > 0)
            {
                GUI.Label(new Rect(15, 45, 100, 20), "Berry:" + berry);
            }
        }

        showEat();
        //GUI.Label(new Rect(15, 30, 100, 20), "Rock:" + rock);
        //GUI.Label(new Rect(15, 45, 100, 20), "Sharp Rock:" + sharpRock);
        //GUI.Label(new Rect(15, 60, 100, 20), "Grass:" + grass);
        //GUI.Label(new Rect(15, 75, 100, 20), "Rope:" + rope);
        //GUI.Label(new Rect(15, 90, 100, 20), "Iron Ore:" + ironOre);




        //if(GUI.Button(new Rect(100, 100, 100, 20), "Gather"))
        //{
        //    gather();
        //}
        //if (GUI.Button(new Rect(100, 150, 100, 20), "Start"))
        //{
        //    created = true;
        //    //firstPlot();
        //}
        //showBox();

    }

    private void showEat()
    {
        if (berry > 0)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 50, 100, 20), "Eat"))
            {
                eat();
            }
        }
    }

    private void showGlade()
    {
        GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 250, 200, 200), plot2, EditorStyles.wordWrappedLabel);
        if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 100, 20), "Gather"))
        {
            gather();
        }
    }

    private void showIntro()
    {
        GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 250, 200, 200), plot1, EditorStyles.wordWrappedLabel);
        if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 100, 20), "Start"))
        {
            hasSeenIntro = true;
            location = "Glade";
            startedGame = true;
        }
    }

    private void showBox()
    {
        if (created)
        {
            GUI.Box(new Rect(400, 400, 400, 100), plot1);
        }
    }

    private void firstPlot()
    {
        
    }

    

    private void gather()
    {
        if(energy > 0)
        {
            energy--;
            if(UnityEngine.Random.Range(0, 100) < 50)
            {
                branch++;
            }
            else
            {
                berry++;
            }
            
        }
    }

    private void eat()
    {
        if(berry > 0)
        {
            berry--;
            energy += UnityEngine.Random.Range(1, 4);
        }
    }
}

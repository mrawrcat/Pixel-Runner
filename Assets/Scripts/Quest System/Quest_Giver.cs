using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Giver : MonoBehaviour
{
    public GameObject[] list_of_quest_obj;
    [SerializeField]
    private GameObject quest;
    public bool Assigned_Quest;
    public bool Helped;
    //public int how_many_quests;
    [SerializeField]
    private string quest_script_name;
    [SerializeField]
    private Quest Quest;
    public Text descTxt;
    public Text isQuestComplete;
    public RectTransform descTransform;
    public bool showedQuest = false;
    //private int questNum;
    [Header("prevent dirty")]
    public GameObject prevent_dirty_canvas;


    public float timeOfTravel = 5; //time after object reach a target place 
    public float currentTime = 0; // actual floting time 
    float normalizedValue;
    public Vector2 rectTrans_hide;
    public Vector2 rectTrans_show;

    
    void Start()
    {
        //quest_script_name = new string[how_many_quests];
        //questNum = Random.Range(0, how_many_quests);
        //quest = list_of_quest_obj[Random.Range(0, list_of_quest_obj.Length)];
        //descTxt.text = quest.GetComponent<Quest>().Quest_Description;
        //Forfeit_Quest();
        Talk_to_Quest_Giver();
        //descTxt.text = quest.GetComponent<Quest>().Quest_Description;
        //descTransform.anchoredPosition = new Vector2(0, 300);

    }

    void Update()
    {
        

        
        if (!showedQuest)
        {
            //prevent_dirty_canvas.SetActive(true);

        }
        else
        {
            //prevent_dirty_canvas.SetActive(false);
        }
       
    }

    public void Talk_to_Quest_Giver()
    {
        if(!Assigned_Quest && !Helped)
        {
            Debug.Log("Got Quest");
            Assign_Quest();
            showedQuest = false;
            //StartCoroutine(Show_And_Hide_Quest(rectTrans_hide, rectTrans_show));

        }
        else if(Assigned_Quest && !Helped)
        {
            Debug.Log("already have quest");
            Giver_Checks_Quest();
        }
        
    }

    void Assign_Quest()
    {
        quest = list_of_quest_obj[Random.Range(0, list_of_quest_obj.Length)];
        Assigned_Quest = true;

        //this adds the quest for it to be able to check if it gets completed or not
        //quest_script_name = quest.GetComponent<Quest>().Quest_Description;
        //Quest = (Quest)quest.AddComponent(System.Type.GetType(quest_script_name));
        Quest = (Quest)quest.GetComponent<Quest>();
        isQuestComplete.text = "Quest Incomplete";
        descTxt.text = quest.GetComponent<Quest>().Quest_Description;
    }

    public void Giver_Checks_Quest()
    {
        if (Quest.Quest_Completed)
        {
            isQuestComplete.text = "Quest Complete";
            Quest.Give_Reward();
            Helped = true;
            Assigned_Quest = false;
        }
        else
        {
            Debug.Log("quest still incomplete");
        }
    }

    public void Forfeit_Quest()
    {
        Assigned_Quest = false;
        Helped = false;

    }
    IEnumerator Show_Quest()
    {
        //descTransform.anchoredPosition = new Vector2(0, 300);
        prevent_dirty_canvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        descTransform.anchoredPosition = Vector2.Lerp(descTransform.anchoredPosition, new Vector2(0, 400), 5f * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        showedQuest = true;

    }

    IEnumerator Hide_Quest()
    {
        yield return new WaitForSeconds(1f);
        descTransform.anchoredPosition = Vector2.Lerp(descTransform.anchoredPosition, new Vector2(0, 700), 5f * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        prevent_dirty_canvas.SetActive(false);
    }

    public void Move_Quest_Text()
    {
        descTransform.anchoredPosition = Vector2.Lerp(descTransform.anchoredPosition, new Vector2(0, 0), 5f * Time.deltaTime);
    }

    IEnumerator Show_And_Hide_Quest(Vector2 start_pos, Vector2 end_pos)
    {
        currentTime = 0;
        yield return new WaitForSeconds(1f);
        while (currentTime <= timeOfTravel)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time 
            descTransform.anchoredPosition = Vector3.Lerp(start_pos, end_pos, normalizedValue);
            yield return null;
        }
        if (descTransform.anchoredPosition == end_pos)
        {
            currentTime = 0;
            yield return new WaitForSeconds(1f);
            while (currentTime <= timeOfTravel)
            {
                currentTime += Time.deltaTime;
                normalizedValue = currentTime / timeOfTravel; // we normalize our time 
                descTransform.anchoredPosition = Vector3.Lerp(end_pos, start_pos, normalizedValue);
                yield return null;
            }
            if (descTransform.anchoredPosition == start_pos)
            {
                showedQuest = true;
            }
        }
    }

    IEnumerator LerpObject(Vector2 start_pos, Vector2 end_pos)
    {
        showedQuest = false;
        //yield return new WaitForSeconds(1f);
        while (currentTime <= timeOfTravel)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time 
            descTransform.anchoredPosition = Vector3.Lerp(start_pos, end_pos, normalizedValue);
            yield return null;
        }
        if(descTransform.anchoredPosition == end_pos)
        {
            showedQuest = true;
        }

    }

    
}

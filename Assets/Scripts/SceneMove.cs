using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void moveScene(string name)
    {
        SceneManager.LoadScene(name);
        GameManager.manager.dead = false;
        GameManager.manager.isUnder = false;
    }

    public void move_to_title()
    {
        SceneManager.LoadScene("Title");
    }

    public void move_scene(string scene_name)
    {
        StartCoroutine(Load_Level_With_Transition(scene_name));
        
    }


    IEnumerator Load_Level_With_Transition(string lvl_index)
    {
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1f); // 1 second is full duration of scene transition
        SceneManager.LoadScene(lvl_index);
        
    }

    public void Clear_Save()
    {
        PlayerPrefs.DeleteAll();
        
    }
    IEnumerator MoveTowards(Transform objectToMove, Vector3 toPosition, float duration)
    {
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            Vector3 currentPos = objectToMove.position;

            float time = Vector3.Distance(currentPos, toPosition) / (duration - counter) * Time.deltaTime;

            objectToMove.position = Vector3.MoveTowards(currentPos, toPosition, time);

            Debug.Log(counter + " / " + duration);
            yield return null;
        }
    }
}

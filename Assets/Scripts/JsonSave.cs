using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        var json = JsonUtility.ToJson(transform.position);
        PlayerPrefs.SetString("SavePosition", json);
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("SavePosition"))
        {
            var json = PlayerPrefs.GetString("SavePosition");
            transform.position = JsonUtility.FromJson<Vector3>(json);
        }
    }
}

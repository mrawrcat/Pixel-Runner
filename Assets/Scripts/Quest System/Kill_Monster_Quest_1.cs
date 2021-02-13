using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Monster_Quest_1 : Quest
{
    // Start is called before the first frame update
    void Awake()
    {
        Quest_Description = "Kill 10 Enemies";

        Goals.Add(new Kill_Goal(this, 0, "Kill 10 Enemies", false, 0, 10));

        Goals.ForEach(g => g.Init());
    }

   
}

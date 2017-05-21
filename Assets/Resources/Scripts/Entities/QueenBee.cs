using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBee : Bee {


    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        var worldState = getWorldState();
        var state = new HashSet<KeyValuePair<string, object>>();

        if (worldState.getBool("canLayEgg"))
            state.Add(new KeyValuePair<string, object>("putEgg", true));
        else
            state.Add(new KeyValuePair<string, object>("canLayEgg", true));
        return state;
    }
}

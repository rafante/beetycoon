using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumbleBee : Bee {

    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        var worldState = getWorldState();
        var state = new HashSet<KeyValuePair<string,object>>();
        if (worldState.getBool("canCopulate"))
            state.Add(new KeyValuePair<string, object>("copulate", true));
        else
            state.Add(new KeyValuePair<string, object>("canCopulate", true));
        return state;
    }
}

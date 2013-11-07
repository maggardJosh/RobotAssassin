using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Event
{
    List<EventAction> actions = new List<EventAction>();
    public bool finished = false;

    public Event(List<EventAction> actions)
    {
        this.actions = actions;
    }

    public void Update()
    {
      
        if (actions.Count > 0)
        {
            if (actions[0].isFinished)
                actions.RemoveAt(0);
            else
                actions[0].execute();
        }
        else
        {
            finished = true;
        }
    }
}

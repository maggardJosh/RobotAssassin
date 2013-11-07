using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Action_FollowNode : EventAction
{
    FNode target;
    float time;
    bool triggered = false;
    float count = 0;

    public Action_FollowNode(FNode followNode, float time)
        : base()
    {
        this.target = followNode;
        this.time = time;
    }
    public override void execute()
    {
        if (!triggered)
        {
            Ym90_GUI.getInstance().moveTo(target, time);
            triggered = true;
        }
        else
        {
            count += UnityEngine.Time.deltaTime;
            if (count > time)
            {
                this.isFinished = true;
                Ym90_GUI.getInstance().follow(target);
            }
        }
    }
}


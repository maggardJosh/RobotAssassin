using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Action_MoveCamera : EventAction
{
    FNode target;
    float time;
    bool triggered = false;
    float count = 0;
    public Action_MoveCamera(float x, float y, float time)
        : base()
    {
        this.target = new FNode();
        this.target.x = x;
        this.target.y = y;
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
                this.isFinished = true;
        }
    }
}


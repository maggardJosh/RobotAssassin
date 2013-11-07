using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Action_Wait : EventAction
{
    
    float time;
    bool triggered = false;
    float count = 0;

    public Action_Wait( float time)
        : base()
    {
        
        this.time = time;
    }
    public override void execute()
    {
        if (!triggered)
        {
           
            triggered = true;
        }
        else
        {
            count += UnityEngine.Time.deltaTime;
            if (count > time)
            {
                this.isFinished = true;
            }
        }
    }
}


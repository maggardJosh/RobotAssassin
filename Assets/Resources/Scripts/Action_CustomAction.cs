using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class Action_CustomAction : EventAction
    {
        Action a;
        public Action_CustomAction(Action a)
        {
            this.a = a;
        }
        public override void execute()
        {
            a.Invoke();
            this.isFinished = true;
        }
    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

	public class Action_ShowConvo :EventAction
	{
        private FConvo convo;
        public Action_ShowConvo(FConvo convo) : base()
        {
            this.convo = convo;
        }
        public override void execute()
        {
            if(convo.container == null)
                Ym90_GUI.getInstance().AddChild(convo);
            if (convo.isFinished)
                this.isFinished = true;
        }
	}


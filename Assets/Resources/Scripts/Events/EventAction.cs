using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

	public abstract class EventAction
	{
        public bool isFinished = false;
        public EventAction()
        {

        }

        public abstract void execute();

	}

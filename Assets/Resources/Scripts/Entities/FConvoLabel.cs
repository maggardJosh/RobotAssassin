using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

	public class FConvoLabel : FLabel
	{
        private string fullText;
        private float revealRate = .08f;
        private float count = 0;
        public bool finished = false;
        public FConvoLabel(string fontName, string text) : base(fontName, "")
        {
            this.fullText = text;
        }

        private void Update()
        {
            if (finished)
                return;
            if (count > revealRate)
            {
                if (text.Length == fullText.Length)
                    finished = true;
                else
                {
                    int lengthToAdd = 1;
                    while (fullText.Substring(text.Length+(lengthToAdd-1), 1).CompareTo(" ") == 0)
                        lengthToAdd += 1;
                    text = fullText.Substring(0, text.Length + lengthToAdd);
                    count = 0;
                }
            } else{
            count += UnityEngine.Time.deltaTime;
            
            }
        }

        public override void HandleAddedToStage()
        {
            Futile.instance.SignalUpdate += Update;
            base.HandleAddedToStage();
        }

        public override void HandleRemovedFromStage()
        {
            Futile.instance.SignalUpdate -= Update;
            base.HandleRemovedFromStage();
        }
	}

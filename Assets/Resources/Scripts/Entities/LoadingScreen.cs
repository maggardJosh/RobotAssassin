using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

	public class LoadingScreen : FContainer
	{
        FSprite loadingImage;
        public bool transitionOn = false;
        public bool finishedTransition = false;
        float revealSpeed = 300;
        public bool active = false;
        public LoadingScreen() : base()
        {
            loadingImage = new FSprite("loading");
            this.x = Futile.screen.width;
            this.AddChild(loadingImage);

            Futile.instance.SignalUpdate += Update;
        }
        public void startTransitionOn()
        {
            active = true;
            transitionOn = true;
            finishedTransition = false;
            this.x = Futile.screen.width;
        }

        public void startTransitionOff()
        {
            transitionOn = false;
            finishedTransition = false;
            this.x = 0;
        }

        public void Update()
        {
            if (!active)
                return;
            if (transitionOn)
            {
                if (x > 0)
                {
                    finishedTransition = false;
                    x -= revealSpeed * UnityEngine.Time.deltaTime;
                    x = Math.Max(x, 0);
                }
                else
                {
                    finishedTransition = true;
                }
            }
            else
            {
                if (x > -Futile.screen.width)
                {
                    x -= revealSpeed * UnityEngine.Time.deltaTime;
                    finishedTransition = false;
                }
                else
                {
                    x = -Futile.screen.width;
                    finishedTransition = true;
                    active = false;
                }
            }
        }
	}

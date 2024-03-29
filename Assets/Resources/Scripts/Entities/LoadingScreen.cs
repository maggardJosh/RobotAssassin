﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

	public class LoadingScreen : FContainer
	{
        FSprite loadingImage;
        public bool transitionOn = false;
        public bool finishedTransition = false;
        float revealSpeed = 600;
        public bool active = false;
        const float MIN_LOAD_TIME = .3f;
        float count = 0;
        public LoadingScreen() : base()
        {
            loadingImage = new FSprite("loading");
            this.x = Futile.screen.halfWidth + loadingImage.width/2;
            this.AddChild(loadingImage);

            Futile.instance.SignalUpdate += Update;
        }
        public void startTransitionOn()
        {
            active = true;
            transitionOn = true;
            finishedTransition = false;
            this.x = Futile.screen.halfWidth + loadingImage.width/2;
        }

        public void startTransitionOff()
        {
            transitionOn = false;
            finishedTransition = false;
            this.x = 0;
        }

        public void Update()
        {
            count += UnityEngine.Time.deltaTime;
            if (!active)
                return;
            if (transitionOn)
            {
                Main.controlsLocked = true;
                if (x > 0)
                {
                    finishedTransition = false;
                    x -= revealSpeed * UnityEngine.Time.deltaTime;
                    x = Math.Max(x, 0);
                }
                else
                {
                    finishedTransition = true;
                    count = 0;
                }
            }
            else
            {
                if (count < MIN_LOAD_TIME)
                    return;
                if (x > -Futile.screen.halfWidth - loadingImage.width/2)
                {
                    x -= revealSpeed * UnityEngine.Time.deltaTime;
                    finishedTransition = false;
                }
                else
                {
                    Main.controlsLocked = false;
                    x = -Futile.screen.halfWidth - loadingImage.width / 2;
                    finishedTransition = true;
                    active = false;
                }
            }
        }
	}

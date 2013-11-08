using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

	class GlitchManager
	{
        private float count = 0;
        private float nextCount = .5f;
        private float maxCount = .2f;
        private float minCount = .001f;
        private int minLevel = 0;
        private int maxLevel = 0;
        private int currentLevel = 0;
        private float glitchToNextLength = 3.0f;


        private Boolean isGlitchingToNextLevel = false;

        public int CurrentLevel { get { return currentLevel; } }

        private static GlitchManager glitchManager;
        public static GlitchManager getInstance()
        {
            if (glitchManager == null)
                glitchManager = new GlitchManager();
            return glitchManager;
        }

        private GlitchManager()
        {
            Futile.instance.SignalUpdate += Update;
        }

        public void glitchToNext()
        {
            this.isGlitchingToNextLevel = true;
            minLevel = currentLevel;
            maxLevel = currentLevel + 1;
            minLevel = Math.Min(3, minLevel);
            maxLevel = Math.Min(3, maxLevel);
        }

        private void Update()
        {
            if (isGlitchingToNextLevel)
            {
                count += UnityEngine.Time.deltaTime;
                if (Math.Abs(Math.Cos(count * (glitchToNextLength - count) * Math.PI * 10)) > .5f)
                {
                    currentLevel = maxLevel;
                }
                else
                {
                    currentLevel = minLevel;
                }
                if (count > glitchToNextLength)
                {
                    isGlitchingToNextLevel = false;
                    currentLevel = maxLevel;
                    count = 0;
                }
            }
            else
            {
                currentLevel = maxLevel;
            }
        }
	}


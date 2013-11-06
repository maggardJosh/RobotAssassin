using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

	class GlitchManager
	{
        private float count = 0;
        private float nextCount = .5f;
        private float maxCount = 1.5f;
        private int maxLevel = 3;
        private int currentLevel = 0;

        private Boolean isGlitching = false;

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

        private void Update()
        {
            if (isGlitching)
            {
                count += UnityEngine.Time.deltaTime;
                if (count > .05f)
                {
                    currentLevel = RXRandom.Int(maxLevel + 1);
                    count = 0;
                }
            }
            else
            {
                currentLevel = maxLevel;
            }
        }
	}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

	public class Ym90_GUI :FCamObject
	{
        private static Ym90_GUI instance;

        public static Ym90_GUI getInstance()
        {
            if (instance == null)
                instance = new Ym90_GUI();
            return instance;
        }
        FContainer guiLayer = new FContainer();
        FContainer overlay = new FContainer();
        public void setLoadingScreen(LoadingScreen loadingScreen)
        {
            overlay.AddChild(loadingScreen);
        }

        private Ym90_GUI() : base()
        {
            FSprite statusBar = new FSprite("statusBar_bg");
            statusBar.y = Futile.screen.halfHeight - statusBar.height/2;
            guiLayer.AddChild(statusBar);

            FSprite boxGPS = new FSprite("box_gps");
            boxGPS.x = -Futile.screen.halfWidth + boxGPS.width/2 + 7;
            boxGPS.y = statusBar.y;
            guiLayer.AddChild(boxGPS);

            FSprite centerGPS = new FSprite("gps_center");
            centerGPS.x = boxGPS.x - boxGPS.width / 4;
            centerGPS.y = boxGPS.y;
            guiLayer.AddChild(centerGPS);

            FSprite[] healthSprites = new FSprite[4];
            float healthPadding = 1;
            for (int x = 0; x < 4; x++)
            {
                healthSprites[x] = new FSprite("health_full");
                float startX = -(healthSprites[x].width + healthPadding) * 1.8f;
                healthSprites[x].x = startX + (healthSprites[x].width + healthPadding) * x;
                healthSprites[x].y = statusBar.y;
                guiLayer.AddChild(healthSprites[x]);
            }

            FSprite primaryWeaponBox = new FSprite("box_items");
            primaryWeaponBox.x = Futile.screen.halfWidth - primaryWeaponBox.width * 2 - 2;
            primaryWeaponBox.y = statusBar.y;
            guiLayer.AddChild(primaryWeaponBox);

            FSprite secondaryWeaponBox = new FSprite("box_items");
            secondaryWeaponBox.x = primaryWeaponBox.x + primaryWeaponBox.width + 5;
            secondaryWeaponBox.y = statusBar.y;
            guiLayer.AddChild(secondaryWeaponBox);

            FSprite primaryWeapon = new FSprite("weapon_swordArm");
            primaryWeapon.SetPosition(primaryWeaponBox.GetPosition());
            guiLayer.AddChild(primaryWeapon);

            this.AddChild(guiLayer);
            this.AddChild(overlay);
        }
	}


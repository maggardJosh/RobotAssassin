using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

	public class Ym90_GUI :FCamObject
	{
        public Ym90_GUI() : base()
        {
            FSprite statusBar = new FSprite("statusBar_bg");
            statusBar.y = Futile.screen.halfHeight - statusBar.height/2;
            this.AddChild(statusBar);

            FSprite boxGPS = new FSprite("box_gps");
            boxGPS.x = -Futile.screen.halfWidth + boxGPS.width/2 + 7;
            boxGPS.y = statusBar.y;
            this.AddChild(boxGPS);

            FSprite centerGPS = new FSprite("gps_center");
            centerGPS.x = boxGPS.x - boxGPS.width / 4;
            centerGPS.y = boxGPS.y;
            this.AddChild(centerGPS);

            FSprite[] healthSprites = new FSprite[4];
            float healthPadding = 1;
            for (int x = 0; x < 4; x++)
            {
                healthSprites[x] = new FSprite("health_full");
                float startX = -(healthSprites[x].width + healthPadding) * 1.8f;
                healthSprites[x].x = startX + (healthSprites[x].width + healthPadding) * x;
                healthSprites[x].y = statusBar.y;
                this.AddChild(healthSprites[x]);
            }

            FSprite primaryWeaponBox = new FSprite("box_items");
            primaryWeaponBox.x = Futile.screen.halfWidth - primaryWeaponBox.width * 2 - 2;
            primaryWeaponBox.y = statusBar.y;
            this.AddChild(primaryWeaponBox);

            FSprite secondaryWeaponBox = new FSprite("box_items");
            secondaryWeaponBox.x = primaryWeaponBox.x + primaryWeaponBox.width + 5;
            secondaryWeaponBox.y = statusBar.y;
            this.AddChild(secondaryWeaponBox);

            FSprite primaryWeapon = new FSprite("weapon_swordArm");
            primaryWeapon.SetPosition(primaryWeaponBox.GetPosition());
            this.AddChild(primaryWeapon);
        }
	}


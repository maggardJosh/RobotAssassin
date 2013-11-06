using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class WarpPoint : FNode
    {
        public int warpTileX;
        public int warpTileY;
        public string mapName;

        public WarpPoint(int warpTileX, int warpTileY, string mapName, int xPos, int yPos) : base()
        {
            this.warpTileX = warpTileX;
            this.warpTileY = warpTileY;
            this.mapName = mapName;
            this.x = xPos;
            this.y = yPos;
        }
    }


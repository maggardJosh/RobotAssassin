using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    public abstract class Weapon : BaseGameObject
    {
        protected Player player;
        protected Rect hitBox = new Rect();
        protected bool isHitBoxActive = false;

        public bool isInHitBox(Vector2 point)
        {
            if (!isHitBoxActive)
                return false;
            return hitBox.Contains(point);
        }

        public Weapon(string elementBase, Player player) : base (elementBase)
        {
            
            this.player = player;
        }

        public abstract void Attack(BaseWalkingAnimSprite.Direction direction);

        protected override void Update()
        {
            if (this._stopped)
                this.alpha = 0;
            else
                this.alpha = 1;
            base.Update();
        }

        internal bool ShouldTestAttack()
        {
            return isHitBoxActive;
        }

        public abstract void testAttack(Map currentMap);
    }

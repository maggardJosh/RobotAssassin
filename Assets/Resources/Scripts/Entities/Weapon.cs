using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public abstract class Weapon : BaseGameObject
    {
        protected Player player;

        public Weapon(string elementBase, Player player) : base (elementBase)
        {
            this.player = player;
        }

        public abstract void Attack();

        protected override void Update()
        {
            if (this._stopped)
                this.alpha = 0;
            else
                this.alpha = 1;
            base.Update();
        }
    }

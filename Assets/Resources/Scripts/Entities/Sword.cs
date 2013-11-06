using UnityEngine;
using System.Collections;

public class Sword : BaseGameObject
{
    public Sword() : base("sword")
    {
        addAnimation(new FAnimation("attack_down", new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 30, false));
    }

    protected override void Update()
    {
        if (this._stopped)
            this.alpha = 0;
        else
            this.alpha = 1;
        base.Update();
    }

}

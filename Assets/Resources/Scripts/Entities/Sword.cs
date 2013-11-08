using UnityEngine;
using System.Collections;

public class Sword : Weapon
{
    bool attackOne = false;
    bool attackTwo = false;
    public Sword(Player p)
        : base("sword", p)
    {
        addAnimation(new FAnimation("attack_down", new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 100, false));
    }

    public override void Attack()
    {
        if (!attackOne)
        {
            attackOne = true;
            this.play("attack_down", true);
            player.CurrentState = BaseWalkingAnimSprite.State.ATTACKING;
        }
        else if (attackTwo)
        {

        }
    }

    protected override void Update()
    {
        base.Update();
        if (player.CurrentState == BaseWalkingAnimSprite.State.ATTACKING)
        {
            if (_stopped)
            {
                attackOne = false;
                attackTwo = false;
                player.CurrentState = BaseWalkingAnimSprite.State.IDLE;
            }
            else
            {
                player.moveDown(30);
            }
        }
    }
}

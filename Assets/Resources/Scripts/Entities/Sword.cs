using UnityEngine;
using System.Collections;

public class Sword : Weapon
{
    bool attackOne = false;
    bool attackTwo = false;
    private float count = 0;
    private float maxLength = .8f;
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
            this.alpha = 1;
            this.count = 0;
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
            count += UnityEngine.Time.deltaTime;
            if (_stopped)
            {
                attackOne = false;
                attackTwo = false;
                player.CurrentState = BaseWalkingAnimSprite.State.IDLE;
            }
            else
            {
                player.moveDown(Mathf.Max(0,Mathf.Cos((((count)/maxLength)*2)*Mathf.PI))* 50);
                
            }
        }
    }
}

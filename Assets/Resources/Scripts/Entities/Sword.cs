using UnityEngine;
using System.Collections;

public class Sword : Weapon
{
    public static int[] integerArray(int start, int end)
    {
        int[] result = new int[end - start + 1];
        for (int x = start; x <= end; x++)
            result[x - start] = x;
        return result;
    }
    bool attackOne = false;
    bool attackTwo = false;
    private float count = 0;
    private float maxLength = .8f;
    public Sword(Player p)
        : base("sword", p)
    {
        addAnimation(new FAnimation("attack_down", integerArray(1, 8), 100, false));
        addAnimation(new FAnimation("attackTwo_down", integerArray(9, 19), 100, false));
        addAnimation(new FAnimation("attack_left", integerArray(20, 27), 100, false));
        addAnimation(new FAnimation("attackTwo_left", integerArray(28, 38), 100, false));
        addAnimation(new FAnimation("attack_up", integerArray(39, 46), 100, false));
        addAnimation(new FAnimation("attackTwo_up", integerArray(47, 57), 100, false));
        addAnimation(new FAnimation("attack_right", integerArray(58, 65), 100, false));
        addAnimation(new FAnimation("attackTwo_right", integerArray(66, 76), 100, false));

        /*        Sword Animation Frames
        1-8 : down attack 1
        9-19 : down attack 2
        20-27 : left attack 1
        28-38 : left attack 2
        29-46 : up attack 1
        47-57 : up attack 2
        58-65 : right attack 1
        66-76 : right attack 2
         * */

    }

    BaseWalkingAnimSprite.Direction currentDirection = BaseWalkingAnimSprite.Direction.DOWN;

    public override void Attack(BaseWalkingAnimSprite.Direction direction)
    {
        if (!attackOne && !attackTwo)
        {
            this.currentDirection = direction;
            attackOne = true;
            this.play("attack_" + direction.ToString().ToLower(), true);
            player.CurrentState = BaseWalkingAnimSprite.State.ATTACKING;
            this.alpha = 1;
            this.count = 0;
        }
        else if (!attackTwo && attackOne)   //In attack one but not started attack 2 yet
        {
            this.currentDirection = direction;
            attackOne = false;
            attackTwo = true;
            this.play("attackTwo_" + direction.ToString().ToLower(), true);
            player.CurrentState = BaseWalkingAnimSprite.State.ATTACKING;
            this.alpha = 1;
            this.count = 0;

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
                float speed = 50;
                if (attackOne)
                    speed = 50;
                else if (attackTwo)
                    speed = 200;

                switch (currentDirection)
                {
                    case BaseWalkingAnimSprite.Direction.DOWN:
                        player.moveDown(Mathf.Max(0, Mathf.Cos((((count) / maxLength) * 2) * Mathf.PI)) * speed);
                        break;
                    case BaseWalkingAnimSprite.Direction.RIGHT:
                        player.moveRight(Mathf.Max(0, Mathf.Cos((((count) / maxLength) * 2) * Mathf.PI)) * speed);
                        break;
                    case BaseWalkingAnimSprite.Direction.UP:
                        player.moveUp(Mathf.Max(0, Mathf.Cos((((count) / maxLength) * 2) * Mathf.PI)) * speed);
                        break;
                    case BaseWalkingAnimSprite.Direction.LEFT:
                        player.moveLeft(Mathf.Max(0, Mathf.Cos((((count) / maxLength) * 2) * Mathf.PI)) * speed);
                        break;
                }

            }
        }
    }
}


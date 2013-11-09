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

    }
    FSprite hitboxDebug = new FSprite(Futile.whiteElement);
    public override void HandleAddedToContainer(FContainer container)
    {
       // container.AddChild(hitboxDebug);
        base.HandleAddedToContainer(container);
    }

    BaseWalkingAnimSprite.Direction currentDirection = BaseWalkingAnimSprite.Direction.DOWN;

    public override void Attack(BaseWalkingAnimSprite.Direction direction)
    {
        if (!attackOne && !attackTwo)
        {
            hasCheckedHitBox = false;
            this.currentDirection = direction;
            attackOne = true;
            this.play("attack_" + direction.ToString().ToLower(), true);
            player.CurrentState = BaseWalkingAnimSprite.State.ATTACKING;
            this.alpha = 1;
            this.count = 0;
        }
        else if (!attackTwo && attackOne)   //In attack one but not started attack 2 yet
        {
            hasCheckedHitBox = false;
            this.currentDirection = direction;
            attackOne = false;
            attackTwo = true;
            this.play("attackTwo_" + direction.ToString().ToLower(), true);
            player.CurrentState = BaseWalkingAnimSprite.State.ATTACKING;
            this.alpha = 1;
            this.count = 0;

        }
    }

    public override void testAttack(Map map)
    {

        foreach (BaseGameObject enemy in map.enemyList)
        {
            if (hitBox.Contains(enemy.GetPosition()))
            {
                Vector2 hitVect = new Vector2();
                hitVect = enemy.GetPosition() - this.GetPosition();
                hitVect = hitVect.normalized * 100;
                ((BaseWalkingAnimSprite) enemy).takeHit(1, hitVect);
            }
        }
        hasCheckedHitBox = true;
    }
    private int lastFrame = 0;
    private bool hasCheckedHitBox = false;
    private bool isInAttackFrame(BaseWalkingAnimSprite.Direction dir)
    {
        switch (dir)
        {
            case BaseWalkingAnimSprite.Direction.UP:
                if (attackOne)
                    return (currentFrame >= 42 && lastFrame <= 41);
                else
                    return (currentFrame >= 52 && lastFrame <= 51);
            case BaseWalkingAnimSprite.Direction.DOWN:
                if (attackOne)
                    return (currentFrame >= 4 && lastFrame <= 5);
                else
                    return (currentFrame >= 15 && lastFrame <= 14);
            case BaseWalkingAnimSprite.Direction.LEFT:
                if (attackOne)
                    return (currentFrame >= 23 && lastFrame <= 24);
                else
                    return (currentFrame >= 34 && lastFrame <= 33);
            case BaseWalkingAnimSprite.Direction.RIGHT:
                if (attackOne)
                    return (currentFrame >= 62 && lastFrame <= 63);
                else
                    return (currentFrame >= 72 && lastFrame <= 71);
        }
        return false;
    }
    protected override void Update()
    {

        base.Update();
        if (player.CurrentState == BaseWalkingAnimSprite.State.ATTACKING)
        {
            count += UnityEngine.Time.deltaTime;

            checkHitbox();
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

        lastFrame = currentFrame;
    }

    private void checkHitbox()
    {
        checkAttackOneHitbox();
        checkAttackTwoHitbox();
        
    }

    private void checkAttackTwoHitbox()
    {
        if (attackTwo)
        {
            if (!hasCheckedHitBox && isInAttackFrame(currentDirection))
            {
                switch (currentDirection)
                {
                    case BaseWalkingAnimSprite.Direction.UP:
                    case BaseWalkingAnimSprite.Direction.DOWN:
                        hitBox.Set(0, 0, 30, 30);
                        break;
                    case BaseWalkingAnimSprite.Direction.LEFT:
                    case BaseWalkingAnimSprite.Direction.RIGHT:
                        hitBox.Set(0, 0, 30, 30);
                        break;
                }
                isHitBoxActive = true;
                switch (currentDirection)
                {
                    case BaseWalkingAnimSprite.Direction.UP:
                        hitBox.x = this.x - 15;
                        hitBox.y = this.y - 5;
                        break;
                    case BaseWalkingAnimSprite.Direction.DOWN:
                        hitBox.x = this.x - 15;
                        hitBox.y = this.y - 25;
                        break;
                    case BaseWalkingAnimSprite.Direction.LEFT:
                        hitBox.x = this.x - 20;
                        hitBox.y = this.y - 12.5f;
                        break;
                    case BaseWalkingAnimSprite.Direction.RIGHT:
                        hitBox.x = this.x - 5;
                        hitBox.y = this.y - 12.5f;
                        break;
                    default:
                        hitBox.x = 0;
                        hitBox.y = 0;
                        break;
                }
                updateHitBoxDebug();
            }
            else
            {
                isHitBoxActive = false;
            }
        }
    }
    private void updateHitBoxDebug()
    {
        hitboxDebug.width = hitBox.width;
        hitboxDebug.height = hitBox.height;
        hitboxDebug.SetPosition(hitBox.xMin + hitBox.width / 2, hitBox.yMax - hitBox.height / 2);
    }
    private void checkAttackOneHitbox()
    {
        if (attackOne)
        {
            if (!hasCheckedHitBox && isInAttackFrame(currentDirection))
            {
                switch (currentDirection)
                {
                    case BaseWalkingAnimSprite.Direction.UP:
                    case BaseWalkingAnimSprite.Direction.DOWN:
                        hitBox.Set(0, 0, 25, 20);
                        break;
                    case BaseWalkingAnimSprite.Direction.LEFT:
                    case BaseWalkingAnimSprite.Direction.RIGHT:
                        hitBox.Set(0, 0, 20, 25);
                        break;
                }
                isHitBoxActive = true;
                switch (currentDirection)
                {
                    case BaseWalkingAnimSprite.Direction.UP:
                        hitBox.x = this.x - 12.5f;
                        hitBox.y = this.y;
                        break;
                    case BaseWalkingAnimSprite.Direction.DOWN:
                        hitBox.x = this.x - 12.5f;
                        hitBox.y = this.y - 20;
                        break;
                    case BaseWalkingAnimSprite.Direction.LEFT:
                        hitBox.x = this.x - 20;
                        hitBox.y = this.y - 12.5f;
                        break;
                    case BaseWalkingAnimSprite.Direction.RIGHT:
                        hitBox.x = this.x;
                        hitBox.y = this.y - 12.5f;
                        break;
                    default:
                        hitBox.x = 0;
                        hitBox.y = 0;
                        break;
                }

                updateHitBoxDebug();
            }
            else
            {
                isHitBoxActive = false;
            }
        }
    }
}


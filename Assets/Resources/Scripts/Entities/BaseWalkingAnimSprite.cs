using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

	public abstract class BaseWalkingAnimSprite : FAnimatedSprite
	{
        protected float walkSpeed = 50.0f;
        public BaseWalkingAnimSprite(string baseElement) : base(baseElement)
        {
            addAnimation(new FAnimation("walk_down", new int[]{0}, 100, true));
            addAnimation(new FAnimation("walk_left", new int[]{1}, 100, true));
            addAnimation(new FAnimation("walk_up", new int[]{2}, 100, true));
            addAnimation(new FAnimation("walk_right", new int[]{3}, 100, true));
        }

        protected abstract void Update();

        public override void HandleAddedToStage()
        {
            Futile.instance.SignalUpdate += Update;
            base.HandleAddedToStage();
        }

        public override void HandleRemovedFromStage()
        {
            Futile.instance.SignalUpdate -= Update;
            base.HandleRemovedFromStage();
        }

        protected void moveUp()
        {
            y += walkSpeed * UnityEngine.Time.deltaTime;
            play("walk_up");
        }

        protected void moveDown()
        {
            y -= walkSpeed * UnityEngine.Time.deltaTime;
            play("walk_down");
        }

        protected void moveLeft()
        {
            x -= walkSpeed * UnityEngine.Time.deltaTime;
            play("walk_left");
        }

        protected void moveRight()
        {
            x += walkSpeed * UnityEngine.Time.deltaTime;
            play("walk_right");
        }

	}

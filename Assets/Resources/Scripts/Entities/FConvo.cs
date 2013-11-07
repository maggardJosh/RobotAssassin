using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FConvo : FContainer
{
    FConvoLabel convoLabel;
    FSprite character;
    FSprite convoBackground;

    const float revealSpeed = 70;
    bool active = true;
    List<string> convos;
    public bool isFinished = false;

    public FConvo(List<string> convos)
        : base()
    {
        convoBackground = new FSprite("textbox");
        character = new FSprite("player_textPortrait");
        character.x = Futile.screen.halfWidth + character.width / 2;
        character.y = convoBackground.height / 2 + character.height/2;
        this.AddChild(character);
        this.AddChild(convoBackground);
        this.convos = convos;
        this.y = -Futile.screen.halfHeight - convoBackground.height / 2;
    }

    public override void HandleAddedToStage()
    {
        Futile.instance.SignalUpdate += Update;
        base.HandleAddedToStage();
    }

    public override void HandleRemovedFromStage()
    {
        Futile.instance.SignalUpdate -= Update;
        isFinished = true;
        base.HandleRemovedFromStage();
    }

    private void hide()
    {
        active = false;
    }

    private void Update()
    {
        if (active)
        {
            Main.controlsLocked = true;
            if (this.y < -Futile.screen.halfHeight + convoBackground.height / 2)
            {
                this.y += revealSpeed * UnityEngine.Time.deltaTime;
            }
            else
            {
                this.y = -Futile.screen.halfHeight + convoBackground.height / 2;
                if (character.x > Futile.screen.halfWidth - character.width / 2)
                    character.x -= revealSpeed * UnityEngine.Time.deltaTime;
                else
                {
                    character.x = Futile.screen.halfWidth - character.width / 2;
                    if (convoLabel != null)
                    {
                        if (convoLabel.finished)
                        {
                            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Space))
                            {
                                convoLabel.RemoveFromContainer();
                                convoLabel = null;
                            }
                        }
                        else
                        {
                            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Space))
                            {
                                convoLabel.fastForward();
                            }
                        }
                    }
                    else
                    {
                        if (convos.Count > 0)
                        {
                            convoLabel = new FConvoLabel("gameFont", convos[0]);
                            convos.RemoveAt(0);
                            this.AddChild(convoLabel);
                        }
                        else
                        {
                            hide();
                            Main.controlsLocked = false;
                        }
                    }
                }

            }
        }
        else
        {
            if (character.x < Futile.screen.halfWidth + character.width / 2)
                character.x += revealSpeed*2 * UnityEngine.Time.deltaTime;
            else
            {
                character.x = Futile.screen.halfWidth + character.width / 2;
                if (this.y > -Futile.screen.halfHeight - convoBackground.height / 2)
                {
                    this.y -= revealSpeed*2 * UnityEngine.Time.deltaTime;
                }
                else
                {
                    this.RemoveFromContainer();
                }
            }
        }
    }
}

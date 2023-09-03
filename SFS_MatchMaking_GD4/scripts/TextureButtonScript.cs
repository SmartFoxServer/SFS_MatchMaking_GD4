using Godot;
using System;

public partial class TextureButtonScript : TextureButton
{
    [Export] public TextureButton button;
    [Export] public Control scriptNode;
    [Export] public String callbackName;
    public override void _Ready()
    {
        button.Pressed += _OnButtonPressed;
    }
    private void _OnButtonPressed()
    {
        scriptNode.Call(callbackName);
    }
}

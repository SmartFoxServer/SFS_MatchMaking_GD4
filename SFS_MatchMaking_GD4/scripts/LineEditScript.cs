using Godot;
using System;

public partial class LineEditScript : LineEdit
{
    [Export] public LineEdit lineEdit;
    [Export] public Control scriptNode;
    [Export] public String callbackName;
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_accept") && !Input.IsActionJustPressed("ui_select"))
                _OnEnterPressed();
    }
    private void _OnEnterPressed()
    {
        scriptNode.Call(callbackName);
    }
}

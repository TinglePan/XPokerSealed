﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Godot;

namespace XCardGame.Scripts.Ui;

public partial class NumberLineEdit: LineEdit, ISetup
{
    private Regex _regex;
    private string _oldText;
    private int _oldValue;
    private int _min;
    private int _max;

    public Action<int, int> ValueChanged;
    
    public int Value
    {
        get => int.Parse(Text);
        set
        {
            if (value != _oldValue)
            {
                Text = value.ToString();
                ValueChanged?.Invoke(_oldValue, value);
                _oldValue = value;
            }
        }
    }

    public override void _Ready()
    {
        _regex = new Regex("^[0-9]*$");
        _oldText = "0";
        _min = Int32.MinValue;
        _max = Int32.MaxValue;
        Text = "0";
        TextChanged += OnTextChanged;
        TextSubmitted += OnTextSubmitted;
    }

    public void Setup(Dictionary<string, object> args)
    {
        _min = (int)args["min"];
        _max = (int)args["max"];
    }
    
    private void OnTextChanged(string newText)
    {
        if (_regex.IsMatch(newText))
        {
            Text = newText;
            _oldText = newText;
        }
        else
        {
            Text = _oldText; 
        }
        CaretColumn = Text.Length; 
    }

    private void OnTextSubmitted(string newText)
    {
        var newValue = int.Parse(newText);
        if (newValue < _min)
        {
            Value = _min;
        }
        else if (newValue > _max)
        {
            Value = _max;
        }
        else
        {
            Value = newValue;
        }
        ReleaseFocus();
    }
}
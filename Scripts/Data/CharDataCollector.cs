﻿using System.Net.Sockets;
using Godot;
using RPG3D.General;

public partial class CharDataCollector : Node
{
    [Export] public string Path = "res://Data/Chars/";
    private DataManagement _dataManagment;
    
    public override void _Ready() 
    {
        base._Ready();
        _dataManagment = GetParent<DataManagement>();
        var files = DirAccess.GetFilesAt(Path);
        foreach (var file in files) {
            var scene = GD.Load<PackedScene>(Path + file);
            var loadedScene = scene.Instantiate();
            string realName = file.Left(file.Length - 5);
            loadedScene.Name = realName;
            AddChild(loadedScene);
            CharDataDefinition converted = (CharDataDefinition)loadedScene;
            _dataManagment.AddCharData(converted);
            loadedScene.QueueFree();
        }
    }
}
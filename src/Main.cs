// Copyright (c) 2017 Leacme (http://leac.me). View LICENSE.md for more information.
using Godot;
using System;

public class Main : Spatial {

	public AudioStreamPlayer Audio { get; } = new AudioStreamPlayer();

	private void InitSound() {
		if (!Lib.Node.SoundEnabled) {
			AudioServer.SetBusMute(AudioServer.GetBusIndex("Master"), true);
		}
	}

	public override void _Notification(int what) {
		if (what is MainLoop.NotificationWmGoBackRequest) {
			GetTree().ChangeScene("res://scenes/Menu.tscn");
		}
	}

	public override void _Ready() {
		GetNode<WorldEnvironment>("sky").Environment.BackgroundColor = new Color(Lib.Node.BackgroundColorHtmlCode);
		InitSound();
		AddChild(Audio);

	}

	public override void _Input(InputEvent @event) {
		if (@event is InputEventScreenTouch te && te.Pressed) {
			var flowerScale = GetNode<Spatial>("Flower").Scale;
			if (flowerScale.y < 0.32) {
				flowerScale = new Vector3(flowerScale.x + 0.01f, flowerScale.y + 0.01f, flowerScale.z + 0.01f);
				GetNode<Spatial>("Flower").Scale = flowerScale;
			}
		}
	}

	public override void _Process(float delta) {
		var flowerScale = GetNode<Spatial>("Flower").Scale;
		if (flowerScale.y > 0.1) {
			flowerScale = new Vector3(flowerScale.x - delta / 100, flowerScale.y - delta / 100, flowerScale.z - delta / 100);
			GetNode<Spatial>("Flower").Scale = flowerScale;
		}

	}
}

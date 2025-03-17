using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 100.0f;
	private AnimatedSprite2D animation;
	private Vector2 lastDirection = Vector2.Down; // Hướng mặc định khi bắt đầu

	public override void _Ready()
	{
		animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		if (direction != Vector2.Zero)
		{
			Velocity = direction * Speed;
			UpdateAnimation(direction, true);
			lastDirection = direction; // Lưu hướng cuối cùng
		}
		else
		{
			Velocity = Vector2.Zero;
			UpdateAnimation(lastDirection, false); // Chuyển sang idle theo hướng cuối
		}

		MoveAndSlide();
	}

	private void UpdateAnimation(Vector2 direction, bool isMoving)
	{
		string newAnimation = "";

		if (direction.X > 0)
			newAnimation = isMoving ? "run_right" : "idle_right";
		else if (direction.X < 0)
			newAnimation = isMoving ? "run_left" : "idle_left";
		else if (direction.Y < 0)
			newAnimation = isMoving ? "run_up" : "idle_up";
		else if (direction.Y > 0)
			newAnimation = isMoving ? "run_down" : "idle_down";

		if (animation.Animation != newAnimation) // Chỉ cập nhật nếu khác animation hiện tại
		{
			animation.Play(newAnimation);
		}
	}
}

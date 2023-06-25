using Godot;
using System;

public partial class UITimer : Timer
{
	public override void _Process(double delta)
	{
		double actualDelta = delta / Engine.TimeScale;
		double timeDelta = actualDelta - delta;
		if (timeDelta < TimeLeft)
		{
			WaitTime = TimeLeft - timeDelta;
			Start();
		}
		else
		{
			WaitTime = 0.5 * delta;
			Start();
		}
	}
}

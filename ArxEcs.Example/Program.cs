namespace ArxEcs.Example
{
	using System;

	class Program
	{
		static void Main(string[] args)
		{
			var entity = new Entity();
			entity.AddComponent(new Position(0, 0));
			entity.AddComponent(new Velocity(1, 0));
			entity.ProcessingSystem.AddSystem(new MovementProcessingSystem("Example"));

			for (int i = 0; i < 10; i++)
			{
				entity.ProcessingSystem.ProcessSystems();
				System.Threading.Thread.Sleep(100);
			}

			Console.WriteLine("Done, press any key to exit..");
			Console.ReadKey();
		}
	}

	public class MovementProcessingSystem : IProcessingSystem
	{

		public string SystemId { get; set; }
		public Entity Entity { get; set; }

		public MovementProcessingSystem(string id)
		{
			SystemId = id;
		}

		public void Process()
		{
			var pos = Entity.GetComponent<Position>();
			var vel = Entity.GetComponent<Velocity>();

			pos.X += vel.X;
			pos.Y += vel.Y;

			Console.WriteLine($"Position: {pos}");
		}
	}

	public class Position : IComponent
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override string ToString() => $"X: {X}, Y: {Y}";
	}

	public class Velocity : IComponent
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Velocity(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override string ToString() => $"X: {X}, Y: {Y}";
	}
}

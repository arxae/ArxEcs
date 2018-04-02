namespace ArxEcs.Tests
{
	class TestComponent : IComponent
	{
		public string Message { get; }

		public TestComponent(string m)
		{
			Message = m;
		}

		public override string ToString() => Message;
	}

	class EmptyTestComponent : IComponent { }

	class MessageComponent : TestComponent
	{
		public MessageComponent(string m) : base(m) { }

		public void Show(bool newLine)
		{
			if (newLine)
			{
			System.Console.WriteLine(Message);
			}
			else
			{
				System.Console.Write(Message);
			}
		}
	}
}

namespace ArxEcs.Tests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class ProcessingSystemTests
	{
		const string TEST_PROCESSING_SYSTEM_ID = "Test Processing System";

		[TestMethod]
		public void AddSystemTest()
		{
			var ent = new Entity();
			ent.ProcessingSystem.AddSystem(new TestProcessingSystem());
			var c = ent.ProcessingSystem.ContainsSystem(TEST_PROCESSING_SYSTEM_ID);
			Assert.IsTrue(c, "Proc System should contain the testing processing system");
		}

		[TestMethod]
		public void AddExistingSystem()
		{
			var ent = new Entity();
			ent.ProcessingSystem.AddSystem(new TestProcessingSystem());

			Assert.ThrowsException<System.ArgumentException>(() =>
					ent.ProcessingSystem.AddSystem(new TestProcessingSystem()),
				"Should throw a argument exception");
		}

		[TestMethod]
		public void ProcessingSystemTest()
		{
			var ent = new Entity();
			ent.AddComponent(new EmptyTestComponent());
			var message = "Test";
			ent.AddComponent(new MessageComponent(message));
			ent.ProcessingSystem.AddSystem(new TestProcessingSystem());

			using (var sw = new System.IO.StringWriter())
			{
				System.Console.SetOut(sw);

				ent.ProcessingSystem.ProcessSystems();
				Assert.AreEqual(message, sw.ToString());
			}
		}

		public class TestProcessingSystem : IProcessingSystem
		{
			public string SystemId { get; set; } = TEST_PROCESSING_SYSTEM_ID;
			public Entity Entity { get; set; }

			public void Process()
			{
				Entity.GetComponent<MessageComponent>().Show(false);
			}
		}
	}
}

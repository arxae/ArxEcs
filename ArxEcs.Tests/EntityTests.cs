namespace ArxEcs.Tests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class EntityTests
	{
		[TestMethod]
		public void AddComponent()
		{
			string message = "Test Message";
			var entity = new Entity().AddComponent(new TestComponent(message));
			var comp = entity.GetComponent<TestComponent>();

			Assert.IsNotNull(comp, "Component should be return (not null)");
			Assert.AreEqual(message, comp.Message, "Component should retain data");
		}

		[TestMethod]
		public void AddComponentAllReadyExists()
		{
			Assert.ThrowsException<System.ArgumentException>(() =>
			{
				new Entity()
					.AddComponent(new EmptyTestComponent())
					.AddComponent(new EmptyTestComponent());
			}, "No double components should exist");
		}

		[TestMethod]
		public void HasComponent()
		{
			var entity = new Entity().AddComponent(new TestComponent(string.Empty));
			var containsComponent = entity.HasComponent<TestComponent>();
			var doesNotContainComponent = entity.HasComponent(typeof(EmptyTestComponent));

			Assert.IsTrue(containsComponent, "Entity should contain TestComponent");
			Assert.IsFalse(doesNotContainComponent, "Entity should not contain EmptyTestComponent");
		}

		[TestMethod]
		public void RemoveComponent()
		{
			var entity = new Entity()
				.AddComponent(new EmptyTestComponent())
				.RemoveComponent<EmptyTestComponent>();
			var contains = entity.HasComponent<EmptyTestComponent>();

			Assert.IsFalse(contains, "Component should have been removed");
		}

		[TestMethod]
		public void ReplaceComponent()
		{
			var m1 = "Message 1";
			var m2 = "Message 2";
			var entity = new Entity()
				.AddComponent(new TestComponent(m1))
				.ReplaceComponent(new TestComponent(m2));
			var comp = entity.GetComponent<TestComponent>();

			Assert.AreEqual(m2, comp.Message, "Component should contain message 2");
		}

		[TestMethod]
		public void ComponentAddedEvent()
		{
			var ent = new Entity();
			int compCounter = 0;
			ent.ComponentAdded += (e, c) => compCounter++;
			ent.AddComponent(new TestComponent("Test"))
				.AddComponent(new EmptyTestComponent());

			Assert.AreEqual(2, compCounter, "2 components where added");
		}

		[TestMethod]
		public void ComponentRemovedEvent()
		{
			var ent = new Entity();
			int counter = 10;
			ent.AddComponent(new EmptyTestComponent());
			ent.ComponentRemoved += (e, c) => counter--;
			ent.RemoveComponent<EmptyTestComponent>();

			Assert.AreEqual(9, counter, "1 component has been removed");
		}
	}
}

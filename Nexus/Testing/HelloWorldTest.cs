using System;

namespace Nexus
{
    using NUnit.Framework;

    [TestFixture]
    public class HelloWorldTest
    {
        [Test]
        public void HelloWorldTextTest()
        {
            HelloWorld world = new HelloWorld();
            world.putWorld();

            Assert.That(world.text, Is.Not.Null.And.EqualTo("Hello World."));
        } 
    }
}

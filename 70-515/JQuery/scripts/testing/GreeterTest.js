GreeterTest = TestCase("GreeterTest");

GreeterTest.prototype.testGreet = function () {
	var greeter = new mynamespace.Greeter();
	assertEquals("Hello, World!", greeter.Greet("World"));
}
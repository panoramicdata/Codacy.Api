using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Test methods should be instance methods for test framework")]
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test method names use underscores for readability")]
[assembly: SuppressMessage("Design", "S2360:Optional parameters should not be used", Justification = "Optional parameters in test helpers improve test readability")]
[assembly: SuppressMessage("Design", "S2339:Public constant members should not be used", Justification = "Test constants are acceptable for test configuration")]
[assembly: SuppressMessage("Reliability", "S1172:Unused method parameters should be removed", Justification = "Parameters may be required by delegate signatures")]

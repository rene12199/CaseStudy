using System.Reflection;
using CaseStudy.Api;
using CaseStudy.Core.Interfaces;

namespace Api.Tests.ArchitectureTests;

[TestFixture]
public class ConverterComplianceTests
{
    //todo Improve Test to Gurantee that every Converter has a Test
    [Test]
    public void ConverterComplianceTests_EveryConverterHasATest()
    {
        // Arrange
        var assembly = Assembly.GetAssembly(typeof(Program));
        var converterTestDir =
            Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", "ConverterTests");
        var fullPath = Path.GetFullPath(converterTestDir);

        var converterImplementations = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConverter<,>)))
            .ToList();
        var fileCount = Directory.GetFiles(fullPath, "*.cs", SearchOption.AllDirectories).Length;

        // Assert
        Assert.That(converterImplementations.Count, Is.EqualTo(fileCount));
    }
}
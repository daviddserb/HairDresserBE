using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace hairDresser.ArchitectureTests
{
    public class GuardDependencyTests : ArchUnitBaseTest
    {
        [Fact]
        public void DomainLayer_ShouldNotDependOn_EntityFramework()
        {
            Types().That().ResideInAssembly(DomainAssembly).Should()
                .NotDependOnAnyTypesThat()
                .ResideInNamespace("Microsoft.EntityFrameworkCore")
                .Check(Architecture);
        }

        [Fact]
        public void ApplicationLayer_ShouldNotDependOn_EntityFramework()
        {
            Types().That().ResideInAssembly(ApplicationAssembly).Should()
                .NotDependOnAnyTypesThat()
                .ResideInNamespace("Microsoft.EntityFrameworkCore")
                .Check(Architecture);
        }
    }
}
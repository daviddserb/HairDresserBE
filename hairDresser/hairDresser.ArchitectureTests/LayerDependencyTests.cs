using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace hairDresser.ArchitectureTests
{
    public class LayerDependencyTests : ArchUnitBaseTest
    {
        [Fact]
        public void DomainLayer_ShouldNotDependOn_ApplicationLayer()
        {
            Types().That().Are(DomainLayer).Should()
                .NotDependOnAny(ApplicationLayer)
                .Check(Architecture);
        }

        [Fact]
        public void DomainLayer_ShouldNotDependOn_InfrastructureLayer()
        {
            Types().That().Are(DomainLayer).Should()
                .NotDependOnAny(InfrastructureLayer)
                .Check(Architecture);
        }

        [Fact]
        public void DomainLayer_ShouldNotDependOn_PresentationLayer()
        {
            Types().That().Are(DomainLayer).Should()
                .NotDependOnAny(PresentationLayer)
                .Check(Architecture);
        }

        [Fact]
        public void ApplicationLayer_ShouldNotDependOn_InfrastructureLayer()
        {
            Types().That().Are(ApplicationLayer).Should()
                .NotDependOnAny(InfrastructureLayer)
                .Check(Architecture);
        }

        [Fact]
        public void ApplicationLayer_ShouldNotDependOn_PresentationLayer()
        {
            Types().That().Are(ApplicationLayer).Should()
                .NotDependOnAny(PresentationLayer)
                .Check(Architecture);
        }

        [Fact]
        public void PresentationLayer_ShouldNotDependOn_InfrastructureLayer()
        {
            Types().That().Are(PresentationLayer).Should()
                .NotDependOnAny(InfrastructureLayer)
                .Check(Architecture);
        }
    }
}
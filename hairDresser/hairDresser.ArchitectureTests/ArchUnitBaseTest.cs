using ArchUnitNET.Domain;
using ArchUnitNET.Loader;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace hairDresser.ArchitectureTests
{
    public abstract class ArchUnitBaseTest : BaseTest
    {
        protected static readonly Architecture Architecture = new ArchLoader()
            .LoadAssemblies(
                DomainAssembly,
                ApplicationAssembly,
                InfrastructureAssembly,
                PresentationAssembly)
            .Build();

        protected static readonly IObjectProvider<IType> DomainLayer =
            Types().That().ResideInAssembly(DomainAssembly).As("Domain layer");

        protected static readonly IObjectProvider<IType> ApplicationLayer =
            Types().That().ResideInAssembly(ApplicationAssembly).As("Application layer");

        protected static readonly IObjectProvider<IType> InfrastructureLayer =
            Types().That().ResideInAssembly(InfrastructureAssembly).As("Infrastructure Layer");

        protected static readonly IObjectProvider<IType> PresentationLayer =
            Types().That().ResideInAssembly(PresentationAssembly).As("Presentation Layer");
    }
}
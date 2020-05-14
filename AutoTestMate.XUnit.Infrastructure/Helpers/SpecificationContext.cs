using System.Diagnostics.CodeAnalysis;

namespace AutoTestMate.XUnit.Infrastructure.Helpers
{
    [ExcludeFromCodeCoverage]
    //[TestFixture]
    public abstract class SpecificationContext
    {
        //[SetUp]
        public virtual void Init()
        {
            Given();
            When();
        }
        public abstract void Given();
        public abstract void When();
    }
}

namespace Optimg.Tests
{
    public class OptimizerStub : Optimizer
    {
        public override string Optimize(string imageUrl, string destDirectory)
        {
            return $"{imageUrl} <> {destDirectory}";
        }
    }
}
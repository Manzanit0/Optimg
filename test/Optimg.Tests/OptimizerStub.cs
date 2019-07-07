namespace Optimg.Tests
{
    public class OptimizerStub : IOptimizer
    {
        public string Optimize(string imageUrl, string destDirectory)
        {
            return $"{imageUrl} <> {destDirectory}";
        }
    }
}
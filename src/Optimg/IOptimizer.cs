namespace Optimg
{
    public interface IOptimizer
    {
        string Optimize(string imageUrl, string destDirectory);
    }
}
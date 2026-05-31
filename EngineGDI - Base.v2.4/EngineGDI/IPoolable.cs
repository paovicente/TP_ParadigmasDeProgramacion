namespace EngineGDI
{
    public interface IPoolable
    {
        bool IsActive { get; }

        void Deactivate();
    }
}
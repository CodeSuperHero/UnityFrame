//CodeSuperHero 20150602

namespace CodeSuperHero.UF
{
    public interface ILog
    {
        string logName { get; }
        bool output { get; set; }

        void LogNormal(object info);
        void LogNormal(string info, params object[] args);

        void LogWarning(object info);
        void LogWarning(string info, params object[] args);

        void LogError(object info);
        void LogError(string info, params object[] args);
    }
}
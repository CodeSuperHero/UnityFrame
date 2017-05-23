//CodeSuperHero 20150602

using System.Collections.Generic;

namespace CodeSuperHero.UF
{
    public class LogFactory
    {
        private static Dictionary<string, ILog> _logDic = new Dictionary<string, ILog>();

        public static ILog GetLog(string logName, string logColor = "")
        {
            if (!DebugLog.instance.output)
            {
                return DebugLog.instance;
            }

            ILog retLog = null;

            if (!_logDic.TryGetValue(logName, out retLog))
            {
                retLog = new Log(logName, logColor);
                _logDic.Add(logName, retLog);
            }

            return retLog;
        }

        public static void Clear()
        {
            _logDic.Clear();
        }
    }
}


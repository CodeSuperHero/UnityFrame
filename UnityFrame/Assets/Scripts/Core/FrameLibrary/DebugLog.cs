//CodeSuperHero 20150602

namespace CodeSuperHero.UF
{
    public class DebugLog : Singleton<DebugLog>, ILog
    {
        public static Log GetLog(string logName = "", string logColor = "")
        {
            return new Log(logName, logColor);
        }

        private bool _output = true;

        public bool output
        {
            get
            {
                return _output;
            }
            set
            {
                _output = value;
            }
        }

        private Log _log;

        private Log log
        {
            get
            {
                if (_log == null)
                {
                    _log = GetLog("DebugLog",Log.LOG_COLOR_BLACK);
                }
                return _log;
            }
        }

        public void LogError(object info)
        {
            if (output)
                log.LogError(info);
        }

        public void LogError(string info, params object[] args)
        {
            if (output)
                log.LogError(info, args);
        }

        public void LogNormal(object info)
        {
            if (output)
                log.LogNormal(info);
        }

        public void LogNormal(string info, params object[] args)
        {
            if (output)
                log.LogNormal(info, args);
        }

        public void LogWarning(object info)
        {
            if (output)
                log.LogNormal(info);
        }

        public void LogWarning(string info, params object[] args)
        {
            if (output)
                log.LogWarning(info, args);
        }
    }
}


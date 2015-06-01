//CodeSuperHero 20150602

using System.Collections.Generic;

namespace CodeSuperHero.UF
{
    public class DebugLog : Singleton<DebugLog>, ILog
    {
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

        public string logName
        {
            get
            {
                return _log.logName;
            }
        }

        private ILog _log;

        private ILog log
        {
            get
            {
                return _log;
            }
        }

        public DebugLog()
        {
            _log = new Log("DebugLog", Log.LOG_COLOR_BLACK);
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


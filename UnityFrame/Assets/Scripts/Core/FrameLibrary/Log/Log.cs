//CodeSuperHero 20150602

using UnityEngine;

namespace CodeSuperHero.UF
{
    public class Log : ILog
    {
        #region color string
        public const string LOG_COLOR_BLACK = "black";
        public const string LOG_COLOR_BLUE = "blue";
        public const string LOG_COLOR_CYAN = "cyan";
        public const string LOG_COLOR_GRAY = "gray";
        public const string LOG_COLOR_GREEN = "green";
        public const string LOG_COLOR_GREY = "grey";
        public const string LOG_COLOR_MAGENTA = "magenta";
        public const string LOG_COLOR_RED = "red";
        public const string LOG_COLOR_WHITE = "white";
        public const string LOG_COLOR_YELLOW = "yellow";
        #endregion

        private string _logName;
        private string _logColor;
        private bool _logOutput = true;
        private ILog _globalLog;

        public string logName {
            get
            {
                return _logName;
            }
        }

        public bool output
        {
            get
            {
                return _logOutput;
            }

            set
            {
                _logOutput = value;
            }
        }

        public Log(string name = "", string color = "")
        {
            this._logName = name;
            if (color == "")
                this._logColor = LOG_COLOR_BLACK;
            else
                this._logColor = color;
            _globalLog = DebugLog.instance;
            this._logOutput = true;
        }

        private bool CheckNotOutput()
        {
            return !_logOutput || !_globalLog.output;
        }

        private string GenerateOutput(object info, string _logColor)
        {
            return "<color=" + _logColor + ">" + _logName + " : " + info.ToString() + ".</color>";
        }

        private string GenerateArgsOutput(string info, string color, params object[] args)
        {
            string outputInfo = string.Format(info, args);
            return GenerateOutput(outputInfo, color);
        }

        public void LogNormal(object info)
        {
            if (CheckNotOutput())
                return;
            string logColor = _logColor == "" ? LOG_COLOR_BLACK : _logColor;
            string content = GenerateOutput(info, logColor);
            Debug.Log(content);
        }

        public void LogNormal(string info, params object[] args)
        {
            if (CheckNotOutput())
                return;
            string logColor = _logColor == "" ? LOG_COLOR_BLACK : _logColor;
            string content = GenerateArgsOutput(info, logColor, args);
            Debug.Log(content);
        }

        public void LogWarning(object info)
        {
            if (CheckNotOutput())
                return;
            string logColor = _logColor == "" ? LOG_COLOR_YELLOW : _logColor;
            string content = GenerateOutput(info, logColor);
            Debug.LogWarning(content);
        }

        public void LogWarning(string info, params object[] args)
        {
            if (CheckNotOutput())
                return;
            string logColor = _logColor == "" ? LOG_COLOR_YELLOW : _logColor;
            string content = GenerateArgsOutput(info, logColor, args);
            Debug.LogWarning(content);
        }

        public void LogError(object info)
        {
            if (CheckNotOutput())
                return;
            string logColor = _logColor == "" ? LOG_COLOR_RED : _logColor;
            string content = GenerateOutput(info, logColor);
            Debug.LogError(content);
        }

        public void LogError(string info, params object[] args)
        {
            if (CheckNotOutput())
                return;
            string logColor = _logColor == "" ? LOG_COLOR_RED : _logColor;
            string content = GenerateArgsOutput(info, logColor, args);
            Debug.LogError(content);
        }
    }
}


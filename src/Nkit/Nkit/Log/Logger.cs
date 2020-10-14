using System;
namespace Nkit.Log
{
    internal class Logger : ILogger
    {
        private string _Name;
        private string _Module;
        public Logger()
        {
            _Name = "Log";
            _Module = "";
        }
        public Logger(string name)
        {
            _Name = name;
            _Module = "";
        }
        public Logger(string name,string module)
        {
            _Name = name;
            _Module = module;
        }
        public void Debug(string text)
        {
            LoggerUtil.Append(text, _Name, _Module, LoggerLevel.Debug);
        }

        public void Debug(string format, params object[] args)
        {
            var text = string.Format(format, args);
            Debug(text);
        }

        public void Error(string text)
        {
            LoggerUtil.Append(text, _Name, _Module, LoggerLevel.Error);
        }

        public void Error(string format, params object[] args)
        {
            var text = string.Format(format, args);
            Error(text);
        }

        public void Fatal(string text)
        {
            LoggerUtil.Append(text, _Name, _Module, LoggerLevel.Fatal);
        }

        public void Fatal(string format, params object[] args)
        {
            var text = string.Format(format, args);
            Fatal(text);
        }

        public void Info(string text)
        {
            LoggerUtil.Append(text, _Name, _Module, LoggerLevel.Info);
        }

        public void Info(string format, params object[] args)
        {
            var text = string.Format(format, args);
            Info(text);
        }

        public void Warn(string text)
        {
            LoggerUtil.Append(text, _Name, _Module, LoggerLevel.Warning);
        }

        public void Warn(string format, params object[] args)
        {
            var text = string.Format(format, args);
            Warn(text);
        }
    }
}

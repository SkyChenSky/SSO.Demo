using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository;
using SSO.Demo.Toolkits.Extension;

namespace SSO.Demo.Toolkits.Helper
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public static class LoggerHelper
    {
        private static readonly ILoggerRepository Repository = LogManager.CreateRepository("NETCoreRepository");
        private static readonly ILog Log = LogManager.GetLogger(Repository.Name, typeof(LoggerHelper));

        static LoggerHelper()
        {
            XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));
        }

        #region 文本日志
        /// <summary>
        /// 文本日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="dir"></param>
        public static void WriteToFile(string message, string dir = "")
        {
            WriteToFileSetting(() => Log.Info(message), "info", dir);
        }

        /// <summary>
        /// 文本日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="dir"></param>
        public static void WriteToFile(string message, Exception ex, string dir = "")
        {
            WriteToFileSetting(() => Log.Error(message, ex), "error", dir);
        }

        private static void WriteToFileSetting(Action action, string appendersName, string dir = "")
        {
            var appenders = LogManager.GetRepository(Repository.Name).GetAppenders();
            if (appenders.FirstOrDefault(i => i.Name == appendersName) is RollingFileAppender appender)
            {
                appender.File = string.Format(dir.IsNullOrEmpty()
                    ? "log4net/"
                    : "log4net/{2}/{1}/", DateTime.Now, dir, appendersName);
                appender.ActivateOptions();
                action();
            }
        }
        #endregion
    }
}

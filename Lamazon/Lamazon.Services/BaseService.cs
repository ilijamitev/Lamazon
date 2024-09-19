using Serilog;

namespace Lamazon.Services
{
    public abstract class BaseService
    {
        private readonly ILogger _logger;

        protected BaseService()
        {
            _logger = Log.Logger;
        }

        protected void LogInfo(string message)
        {
            _logger.Information(message);
        }

        protected void LogWarn(string message)
        {
            _logger.Warning(message);
        }

        protected void LogError(string message, Exception? ex = null)
        {
            if (ex == null)
            {
                _logger.Error(message);
            }
            else
            {
                _logger.Error(ex, message);
            }
        }
    }
}

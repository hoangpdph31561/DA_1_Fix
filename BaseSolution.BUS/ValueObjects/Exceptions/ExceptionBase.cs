using BaseSolution.Application.ValueObjects.Common;

namespace BaseSolution.Application.ValueObjects.Exceptions
{
    [Serializable]
    public class ExceptionBase : Exception
    {
        private static string BuildMessage(string message, Tracker? tracker = null)
        {
            if (tracker == null)
            {
                throw new ArgumentNullException("tracker must not be null");
            }

            return "Request Id: " + tracker?.RequestId + " Message: " + message;
        }

        public ExceptionBase(string context, string key, string message, Tracker? tracker = null)
            : base(message)
        {
            base.Data["Context"] = context;
            base.Data["Key"] = key;
            base.Data["Tracker"] = tracker;
        }

        public ExceptionBase(string context, string key, string message, Exception exception, Tracker? tracker = null)
            : base(message, exception)
        {
            base.Data["Context"] = context;
            base.Data["Key"] = key;
            base.Data["Tracker"] = tracker;
        }

        public ExceptionBase WithData(string name, object value)
        {
            Data[name] = value;
            return this;
        }
    }
}

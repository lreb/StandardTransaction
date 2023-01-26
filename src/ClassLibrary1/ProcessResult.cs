using StandardTransaction;
using System;
using System.Collections.Generic;
using static StandardTransaction.Enum;

namespace ProcessResultMessage
{

    public class ProcessResults 
    {
        /// <summary>
        /// ERROR, SUCCESS, WARNING
        /// </summary>
        public Result Code { get; set; }
        /// <summary>
        /// Is true if transaction execution succesds
        /// </summary>
        public bool Success { get; set; } = false;
        /// <summary>
        /// Is true if transaction execution fails
        /// </summary>
        public bool Fail { get; set; } = false;
        /// <summary>
        /// Is true if transaction execution partial succeeds
        /// </summary>
        public bool Warning { get; set; } = false;
        /// <summary>
        /// Contains the message result of the transaction execution
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// Stores the data (object) result of the transaction execution
        /// </summary>
        public object Data { get; set; } = null;
        /// <summary>
        /// Stores an ID/Reference for the event that was executed
        /// </summary>
        public string EventReference { get; set; } = string.Empty;

        /// <summary>
        /// Collection of detailed messages <seealso cref="ProcessDetail"/>
        /// </summary>
        public List<ProcessDetail> MessageDetail { get; set; } = new List<ProcessDetail>();
    }

    public interface IProcessResult 
    {
        //TODO: INCLUDE LOGGING 

        ProcessResults CreateSuccessStatus(string message);
        ProcessResults CreateSuccessStatus(string message, object data);
        ProcessResults CreateSuccessStatus(string message, string eventReference);
        ProcessResults CreateSuccessStatus(string message, string eventReference, object data);


        ProcessResults CreateErrorStatus(string message);
        ProcessResults CreateErrorStatus(string message, List<ProcessDetail> details);
        ProcessResults CreateErrorStatus(string message, string eventReference);
        ProcessResults CreateErrorStatus(string message, string eventReference, List<ProcessDetail> details);


        ProcessResults CreateWarningStatus(string message);
        ProcessResults CreateWarningStatus(string message, List<ProcessDetail> details);
        ProcessResults CreateWarningStatus(string message, string eventReference);
        ProcessResults CreateWarningStatus(string message, string eventReference, List<ProcessDetail> details);
    }

    public class ProcessResultsLogic : IProcessResult
    {
        #region SUCCESS    

        public ProcessResults CreateSuccessStatus(string message)
        {
            var result = new ProcessResults();
            InitializeResult(ref result, Result.SUCCESS);
            result.Message = message;
            return result;
        }

        public ProcessResults CreateSuccessStatus(string message, object data)
        {
            var result = new ProcessResults();
            InitializeResult(ref result, Result.SUCCESS);
            result.Message = message;
            result.Data = data;
            return result;
        }

        public ProcessResults CreateSuccessStatus(string message, string eventReference)
        {
            var result = new ProcessResults();
            InitializeResult(ref result, Result.SUCCESS);
            result.Message = message;
            result.EventReference = eventReference;
            return result;
        }

        public ProcessResults CreateSuccessStatus(string message, string eventReference, object data)
        {
            var result = new ProcessResults();
            InitializeResult(ref result, Result.SUCCESS);
            result.Message = message;
            result.EventReference = eventReference;
            result.Data = data;
            return result;
        }

        #endregion

        #region FAIL

        public ProcessResults CreateErrorStatus(string message)
        {
            var result = new ProcessResults();
            InitializeResult(ref result, Result.ERROR);
            result.Message = message;
            return result;
        }

        public ProcessResults CreateErrorStatus(string message, List<ProcessDetail> details)
        {
            var result = new ProcessResults();
            InitializeResult(ref result, Result.ERROR);
            result.Message = message;
            result.MessageDetail.AddRange(details);
            return result;
        }

        public ProcessResults CreateErrorStatus(string message, string eventReference)
        {
            var result = new ProcessResults();
            InitializeResult(ref result, Result.ERROR);
            result.Message = message;
            result.EventReference = eventReference;
            return result;
        }

        public ProcessResults CreateErrorStatus(string message, string eventReference, List<ProcessDetail> details)
        {
            var result = new ProcessResults();
            InitializeResult(ref result, Result.ERROR);
            result.Message = message;
            result.EventReference = eventReference;
            result.MessageDetail.AddRange(details);
            return result;
        }

        #endregion


        #region WARNING

        public ProcessResults CreateWarningStatus(string message)
        {
            var result = new ProcessResults();
            InitializeResult(ref result, Result.ERROR);
            result.Message = message;
            return result;
        }

        public ProcessResults CreateWarningStatus(string message, List<ProcessDetail> details)
        {
            var result = new ProcessResults();
            InitializeResult(ref result, Result.WARNING);
            result.Message = message;
            result.MessageDetail.AddRange(details);
            return result;
        }

        public ProcessResults CreateWarningStatus(string message, string eventReference)
        {
            var result = new ProcessResults();
            InitializeResult(ref result, Result.WARNING);
            result.Message = message;
            result.EventReference = eventReference;
            return result;
        }

        public ProcessResults CreateWarningStatus(string message, string eventReference, List<ProcessDetail> details)
        {
            var result = new ProcessResults();
            InitializeResult(ref result, Result.WARNING);
            result.Message = message;
            result.EventReference = eventReference;
            result.MessageDetail.AddRange(details);
            return result;
        }

        #endregion

        private void InitializeResult(ref ProcessResults result, Result status)
        {
            result.Code = status;
            switch (result.Code)
            {
                case Result.ERROR:
                    result.Fail = true;
                    result.Success = false;
                    result.Warning = false;
                    break;
                case Result.SUCCESS:
                    result.Fail = false;
                    result.Success = true;
                    result.Warning = false;
                    break;
                case Result.WARNING:
                    result.Fail = false;
                    result.Success = false;
                    result.Warning = true;
                    break;
            }
        }
    }

    public class ProcessResult
    {
        /// <summary>
        /// ERROR, SUCCESS, WARNING
        /// </summary>
        public Result _code { get; set; }
        /// <summary>
        /// Contains the message result of the transaction execution
        /// </summary>
        public string _message { get; set; }
        /// <summary>
        /// Stores an ID for the transaction that was executed
        /// </summary>
        public string _eventId { get; set; }
        /// <summary>
        /// Stores the data (object) result of the transaction execution
        /// </summary>
        public object _data { get; set; }
        /// <summary>
        /// Is true if transaction execution succeed
        /// </summary>
        public bool _success { get; set; }
        /// <summary>
        /// Is true if transaction execution fails
        /// </summary>
        public bool _fail { get; set; }
        /// <summary>
        /// Is true if transaction execution partial succeeds
        /// </summary>
        public bool _warning { get; set; }
        /// <summary>
        /// Collection of detailed messages
        /// </summary>
        public ICollection<ProcessDetail> _messageDetail { get; set; }


        /// <summary>
        /// Constructor with code and message. It is private to enforce the factory method usage
        /// </summary>
        /// <param name="code">Service call code. It can be SUCCESS/WARNING/FAIL</param>
        /// <param name="message">Service call message</param>
        public ProcessResult(Result code, String message)
        {
            this._code = code;
            this._message = message;

            switch (this._code)
            {
                case Result.ERROR:
                    this._fail = true;
                    this._success = false;
                    this._warning = false;
                    break;
                case Result.SUCCESS:
                    this._fail = false;
                    this._success = true;
                    this._warning = false;
                    break;
                case Result.WARNING:
                    this._fail = false;
                    this._success = false;
                    this._warning = true;
                    break;
            }
        }

        /// <summary>
        /// Constructor with code and message. It is private to enforce the factory method usage
        /// </summary>
        /// <param name="code">Service call code. It can be SUCCESS/WARNING/FAIL</param>
        /// <param name="message">Service call message</param>
        /// <param name="data"></param>
        private ProcessResult(Result code, String message, Object data)
        {
            this._code = code;
            this._message = message;
            this._data = data;

            switch (this._code)
            {
                case Result.ERROR:
                    this._fail = true;
                    this._success = false;
                    this._warning = false;
                    break;
                case Result.SUCCESS:
                    this._fail = false;
                    this._success = true;
                    this._warning = false;
                    break;
                case Result.WARNING:
                    this._fail = false;
                    this._success = false;
                    this._warning = true;
                    break;
            }
        }

        /// <summary>
        /// Constructor with code, message and event id. It is private to enforce the factory method usage
        /// </summary>
        /// <param name="code">Service call code. It can be SUCCESS/WARNING/FAIL</param>
        /// <param name="message">Service call message</param>
        /// <param name="eventId"></param>
        private ProcessResult(Result code, String message, String eventId)
        {
            this._code = code;
            this._message = message;
            this._eventId = eventId;

            switch (this._code)
            {
                case Result.ERROR:
                    this._fail = true;
                    this._success = false;
                    this._warning = false;
                    break;
                case Result.SUCCESS:
                    this._fail = false;
                    this._success = true;
                    this._warning = false;
                    break;
                case Result.WARNING:
                    this._fail = false;
                    this._success = false;
                    this._warning = true;
                    break;
            }
        }

        /// <summary>
        /// Constructor with code, message, event id and resultData. It is private to enforce the factory method usage 
        /// </summary>
        /// <param name="code">Service call code. It can be SUCCESS/WARNING/FAIL</param>
        /// <param name="message">Service call message</param>
        /// <param name="eventId">Service call id</param>
        /// <param name="data">Service call resultData</param>
        private ProcessResult(Result code, String message, String eventId, Object data)
        {
            this._code = code;
            this._message = message;
            this._eventId = eventId;
            this._data = data;

            switch (this._code)
            {
                case Result.ERROR:
                    this._fail = true;
                    this._success = false;
                    this._warning = false;
                    break;
                case Result.SUCCESS:
                    this._fail = false;
                    this._success = true;
                    this._warning = false;
                    break;
                case Result.WARNING:
                    this._fail = false;
                    this._success = false;
                    this._warning = true;
                    break;
            }
        }

        //public ProcessResult CreateSuccessStatus(String message)
        //{
        //    return new ProcessResult(Result.SUCCESS, message);
        //}


        /**
         * Creates a new service call instance with SUCCESS code
         *
         * @param message
         *            Successful message to return
         * @return New instance
         */
        public static ProcessResult CreateSuccessStatus(String message)
        {
            return new ProcessResult(Result.SUCCESS, message);
        }
        /**
         * Creates a new service call instance with SUCCESS code
         *
         * @param message
         *            Successful message to return
         * @return New instance
         */
        public static ProcessResult CreateSuccessStatus(String message, Object data)
        {
            return new ProcessResult(Result.SUCCESS, message, data);
        }
        /**
         * Creates a new service call instance with FAIL code
         *
         * @param message
         *            Fail message to return
         * @return New instance
         */
        public static ProcessResult CreateFailStatus(String message)
        {
            return new ProcessResult(Result.ERROR, message);
        }
        /**
         * @param message Fail message to return
         * @param messageDetails List of details
         * @return New Instance
         */
        public static ProcessResult CreateFailStatus(String message, List<ProcessDetail> messageDetails)
        {
            return new ProcessResult(Result.ERROR, message);
        }
        /**
         * Creates a new service call instance with WARNING code
         *
         * @param message
         *            Warning message to return
         * @return New instance
         */
        public static ProcessResult CreateWarningStatus(String message)
        {
            return new ProcessResult(Result.WARNING, message);
        }
        /**
         * Creates a new service call instance with WARNING code
         *
         * @param message
         *            Warning message to return
         * @return New instance
         */
        public static ProcessResult CreateWarningStatus(String message, Object resultData)
        {
            return new ProcessResult(Result.WARNING, message, resultData);
        }
        /**
         * Creates a new service call instance using a Result Type class to define
         * its code
         *
         * @param result
         *            Result Type instance
         * @param message
         *            Service call message to return
         * @return New instance
         */
        public static ProcessResult CreateStatus(ResultType result, String message)
        {
            return CreateStatus(result, message, "");
        }
        /**
         * Creates a new service call instance using a Result Type class to define
         * its code
         *
         * @param result
         *            Result Type instance
         * @param message
         *            Service call message to return
         * @return New instance
         */
        public static ProcessResult CreateStatus(ResultType result, String message, Object resultData)
        {
            return CreateStatus(result, message, "", resultData);
        }
        /**
         * Creates a new service call instance using a Result Type class to define
         * its code
         *
         * @param result
         *            Result Type instance
         * @param message
         *            Service call message to return
         * @param eventId
         *            Service result event id
         * @return New instance
         */
        public static ProcessResult CreateStatus(ResultType result, String message, String eventId)
        {
            Result code = 0;
            if (result == ResultType.SUCCESS)
            {
                code = Result.SUCCESS;
            }
            else if (result == ResultType.WARNING)
            {
                code = Result.WARNING;
            }
            else if (result == ResultType.ERROR)
            {
                code = Result.ERROR;
            }
            return new ProcessResult(code, message, eventId);
        }
        
        /**
         * Creates a new service call instance using a Result Type class to define
         * its code
         *
         * @param result
         *            Result Type instance
         * @param message
         *            Service call message to return
         * @param eventId
         *            Service result event id
         * @return New instance
         */
        public static ProcessResult CreateStatus(ResultType result, String message, String eventId, Object resultData)
        {
            Result code = 0;
            if (result == ResultType.SUCCESS)
            {
                code = Result.SUCCESS;
            }
            else if (result == ResultType.WARNING)
            {
                code = Result.WARNING;
            }
            else if (result == ResultType.ERROR)
            {
                code = Result.ERROR;
            }
            return new ProcessResult(code, message, eventId, resultData);
        }
        /**
         * Gets the service call code
         *
         * @return the code
         */
        public Result GetCode()
        {
            return _code;
        }
        /**
         * Sets the service call code
         *
         * <p>
         * FAIL - 0
         * </p>
         * <p>
         * SUCCESS - 1
         * </p>
         * <p>
         * WARNING - 2
         * </p>
         *
         * @param code
         *            value to set
         */
        public void SetCode(Result code)
        {
            this._code = code;
        }
        /**
         * Gets the service call message
         *
         * @return the message
         */
        public String GetMessage()
        {
            return this._message;
        }
        /**
         * sets the service call message
         *
         * @param message
         *            value to set
         */
        public void SetMessage(String message)
        {
            this._message = message;
        }
        /// <summary>
        /// Gets the service call detail message list
        /// </summary>
        /// <returns>the detailed message list</returns>
        public ICollection<ProcessDetail> GetMessageDetail()
        {
            return this._messageDetail;
        }
        /// <summary>
        /// Sets the service call detailed message list
        /// </summary>
        /// <param name="messageDetail">Value to set</param>
        public void SetMessageDetail(List<ProcessDetail> messageDetail)
        {
            this._messageDetail = messageDetail;
        }

        /// <summary>
        /// Allows to add a Message Detail to a ProcessResult
        /// </summary>
        /// <param name="messageDetail">MessageDetail object</param>
        public void AddMessageDetail(ProcessDetail messageDetail)
        {
            if (null == this._messageDetail)
            {
                this._messageDetail = new List<ProcessDetail>();

            }
            this._messageDetail.Add(messageDetail);
        }

        /// <summary>
        /// Allows to add a Message Detail to a ProcessResult
        /// </summary>
        /// <param name="messageDetail">MessageDetail object</param>
        public void AddMessageDetailList(ICollection<ProcessDetail> messageDetailList)
        {
            if (null == this._messageDetail)
            {
                this._messageDetail = new List<ProcessDetail>();

            }
            foreach (var messageDetail in messageDetailList)
            {
                this._messageDetail.Add(messageDetail);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>eventId</returns>
        public String GetEventId()
        {
            return this._eventId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventId">EventId to set</param>
        public void SetEventId(String eventId)
        {
            this._eventId = eventId;
        }

        /// <summary>
        /// Method to return the data
        /// </summary>
        /// <returns>The result Data</returns>
        public Object GetData()
        {
            return this._data;
        }
    }
}

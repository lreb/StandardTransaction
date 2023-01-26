using ProcessResultMessage;
using StandardTransaction;

namespace Console.Trigger.StandardTransaction.Process
{
    public interface ITestTransaction
    {
        ProcessResults EventA();
    }

    public class TestTransaction : ITestTransaction
    {
        public IProcessResult _result { get; set; }
        public TestTransaction(IProcessResult iResult)
        {
            _result = iResult;
        }

        public ProcessResults EventA()
        {
            try
            {
                // uncoment to generate exception
                //string name = "person";
                //System.Console.WriteLine(name[8]);


                // some validations, maybe some was not as expected, but we can continue (WARNING)
                // set as true
                var forceWarning = false;
                if (forceWarning)
                {
                    // register some warning messages
                    var detail = new ProcessDetail("INTERNAL_CODE", "message", "some detail");
                    var detailList = new List<ProcessDetail>();
                    detailList.Add(detail);

                    var warning = _result.CreateWarningStatus("WARNING", "12", detailList);
                    return warning;
                }


                // all steps were seccessed  (SUCCESS)

                var objectEvent = new { Id = 1, Value = "some" };

                var r = _result.CreateSuccessStatus("DI", "10", objectEvent);

                return r;
            }
            catch (Exception ex)
            {
                // event fails (ERROR)
                var detail = new ProcessDetail("INTERNAL_CODE", ex.Message, "some detail");

                var detailList = new List<ProcessDetail>();
                detailList.Add(detail);

                var r = _result.CreateErrorStatus("ERROR", "12", detailList);

                return r;
            }
        }
    }
}

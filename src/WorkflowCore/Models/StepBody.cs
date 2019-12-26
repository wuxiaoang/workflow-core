using System;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace WorkflowCore.Models
{
    public abstract class StepBody : IStepBody
    {
        public abstract ExecutionResult Run(IStepExecutionContext context);

        public Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            return Task.FromResult(Run(context));
        }        

        /// <summary>
        /// 返回带输出的执行结果
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected ExecutionResult OutcomeResult(object value)
        {
            return new ExecutionResult()
            {
                Proceed = true,
                OutcomeValue = value
            };
        }

        /// <summary>
        /// 返回一个持久性的执行结果
        /// </summary>
        /// <param name="persistenceData">持久性数据</param>
        /// <returns></returns>
        protected ExecutionResult PersistResult(object persistenceData)
        {
            return new ExecutionResult()
            {
                Proceed = false,
                PersistenceData = persistenceData
            };
        }

        /// <summary>
        /// 返回一个休眠的执行结果
        /// </summary>
        /// <param name="persistenceData"></param>
        /// <param name="sleep"></param>
        /// <returns></returns>
        protected ExecutionResult SleepResult(object persistenceData, TimeSpan sleep)
        {
            return new ExecutionResult()
            {
                Proceed = false,
                PersistenceData = persistenceData,
                SleepFor = sleep
            };
        }
    }
}

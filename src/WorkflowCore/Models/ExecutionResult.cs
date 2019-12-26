﻿using System;
using System.Collections.Generic;

namespace WorkflowCore.Models
{
    /// <summary>
    /// 执行结果，
    /// </summary>
    public class ExecutionResult
    {

        /// <summary>
        /// 是否继续前进
        /// </summary>
        public bool Proceed { get; set; }

        /// <summary>
        /// 输出的值
        /// </summary>
        public object OutcomeValue { get; set; }

        public TimeSpan? SleepFor { get; set; }

        public object PersistenceData { get; set; }

        public string EventName { get; set; }

        public string EventKey { get; set; }

        public DateTime EventAsOf { get; set; }
        
        public object SubscriptionData { get; set; }

        public List<object> BranchValues { get; set; } = new List<object>();

        public ExecutionResult()
        {
        }

        public ExecutionResult(object outcome)
        {
            Proceed = true;
            OutcomeValue = outcome;
        }

        /// <summary>
        /// 得到一个执行结果：value 为输出值，继续执行为真
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ExecutionResult Outcome(object value)
        {
            return new ExecutionResult()
            {
                Proceed = true,
                OutcomeValue = value
            };
        }

        /// <summary>
        /// 返回一个执行结果：输出值为空，继续执行为真
        /// </summary>
        /// <returns></returns>
        public static ExecutionResult Next()
        {
            return new ExecutionResult()
            {
                Proceed = true,
                OutcomeValue = null
            };
        }

        public static ExecutionResult Persist(object persistenceData)
        {
            return new ExecutionResult()
            {
                Proceed = false,
                PersistenceData = persistenceData
            };
        }

        public static ExecutionResult Branch(List<object> branches, object persistenceData)
        {
            return new ExecutionResult()
            {
                Proceed = false,
                PersistenceData = persistenceData,
                BranchValues = branches
            };
        }

        public static ExecutionResult Sleep(TimeSpan duration, object persistenceData)
        {
            return new ExecutionResult()
            {
                Proceed = false,
                SleepFor = duration,
                PersistenceData = persistenceData
            };
        }

        public static ExecutionResult WaitForEvent(string eventName, string eventKey, DateTime effectiveDate)
        {
            return new ExecutionResult()
            {
                Proceed = false,
                EventName = eventName,
                EventKey = eventKey,
                EventAsOf = effectiveDate.ToUniversalTime()
            };
        }
        
        public static ExecutionResult WaitForActivity(string activityName, object subscriptionData, DateTime effectiveDate)
        {
            return new ExecutionResult()
            {
                Proceed = false,
                EventName = Event.EventTypeActivity,
                EventKey = activityName,
                SubscriptionData = subscriptionData,
                EventAsOf = effectiveDate.ToUniversalTime()
            };
        }
    }
}

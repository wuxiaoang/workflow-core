using System;
using System.Linq.Expressions;

namespace WorkflowCore.Models
{
    /// <summary>
    /// 步骤结果
    /// 步骤结果的Value是一个Expression, 所以对这个Expression的理解就至关重要
    /// </summary>
    public class StepOutcome
    {
        private Expression<Func<object, object>> _value;

        public Expression<Func<object, object>> Value
        {
            set { _value = value; }
        }
        
        public int NextStep { get; set; }

        public string Label { get; set; }

        public string ExternalNextStepId { get; set; }

        public object GetValue(object data)
        {
            if (_value == null)
                return null;

            return _value.Compile().Invoke(data);
        }
    }
}

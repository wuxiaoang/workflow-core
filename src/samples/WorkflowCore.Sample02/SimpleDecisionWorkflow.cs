using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Sample02.Steps;

namespace WorkflowCore.Sample02
{
    /// <summary>
    /// 简单决策流程,说明如何创建循环的工作流程。
    /// </summary>
    public class SimpleDecisionWorkflow : IWorkflow
    {
        public string Id => "Simple Decision Workflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<HelloWorld>()
                .Then<RandomOutput>(randomOutput =>
                {
                    // ?? When已被标为过时, 但是应该用那个方法来代替 ?
                    randomOutput
                        .When(0)
                            .Then<CustomMessage>(cm =>
                            {
                                cm.Name("Print custom message");
                                cm.Input(step => step.Message, data => "Looping back....");
                            })
                            .Then(randomOutput);  //loop back to randomOutput

                    // ?? When已被标为过时, 但是应该用那个方法来代替 ?
                    randomOutput
                        .When(1)
                            .Then<GoodbyeWorld>();
                });
        }
    }
}

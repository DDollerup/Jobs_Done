using Jobs_Done.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jobs_Done.Factories
{
    public class VMFactory
    {
        private static volatile VMFactory INSTANCE;
        public static VMFactory Instance
        {
            get
            {
                if (INSTANCE == null)
                {
                    INSTANCE = new VMFactory();
                }
                return INSTANCE;
            }
        }

        private DBContext context = DBContext.Instance;

        public CaseVM CreateCaseVM(int caseID)
        {
            CaseVM @case = new CaseVM()
            {
                Case = context.CaseFactory.Get(caseID),
                Tasks = CreateTaskVMs(caseID)
            };

            return @case;
        }

        // caseID = 2
        public List<TaskVM> CreateTaskVMs(int caseID)
        {
            List<TaskVM> tasks = new List<TaskVM>();

            foreach (Relation relation in context.RelationFactory.GetAllBy("CaseID", caseID))
            {
                TaskVM taskVM = new TaskVM()
                {
                    Task = context.TaskFactory.Get(relation.TaskID),
                    Relation = relation
                };

                tasks.Add(taskVM);
            }

            return tasks;
        }
    }
}
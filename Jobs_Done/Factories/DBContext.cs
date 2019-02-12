using Jobs_Done.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jobs_Done.Factories
{
    public class DBContext
    {
        private static volatile DBContext INSTANCE;
        public static DBContext Instance
        {
            get
            {
                if (INSTANCE == null)
                {
                    INSTANCE = new DBContext();
                }
                return INSTANCE;
            }
        }

        private AutoFactory<Case> caseFactory;
        private AutoFactory<Task> taskFactory;
        private AutoFactory<Relation> relationFactory;

        public AutoFactory<Case> CaseFactory
        {
            get
            {
                if (caseFactory == null)
                {
                    caseFactory = new AutoFactory<Case>();
                }
                return caseFactory;
            }
        }

        public AutoFactory<Task> TaskFactory
        {
            get
            {
                if (taskFactory == null)
                {
                    taskFactory = new AutoFactory<Task>();
                }
                return taskFactory;
            }
        }

        public AutoFactory<Relation> RelationFactory
        {
            get
            {
                if (relationFactory == null)
                {
                    relationFactory = new AutoFactory<Relation>();
                }
                return relationFactory;
            }
        }
    }
}
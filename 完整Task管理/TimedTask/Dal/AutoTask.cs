using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimedTask.Common;

namespace TimedTask.Dal
{
    public class AutoTask : DalBase<Entity.AutoTask>
    {
        public AutoTask()
            : base("AutoTask", "Id")
        {

        }
        public AutoTask(string connString)
            : base(connString, "AutoTask", "Id")
        {

        }
    }
}

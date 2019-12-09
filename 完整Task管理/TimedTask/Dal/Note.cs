using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimedTask.Dal
{
    /// <summary>
    /// 
    /// </summary>
    public class Note : DalBase<Entity.Note>
    {
        public Note()
            : base("Note", "Id")
        {

        }
        public Note(string connString)
            : base(connString, "Note", "Id")
        {

        }
    }
}

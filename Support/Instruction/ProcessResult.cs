using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Instruction
{
    public class ProcessResult
    {
        private bool _Done;
        public bool Done
        {
            get
            {
                return this._Done;
            }
            set
            {
                this._Done = value;
            }
        }

        private string _Message;
        public string Message
        {
            get
            {
                return this._Message;
            }
            set
            {
                this._Message = value;
            }
        }
    }
}

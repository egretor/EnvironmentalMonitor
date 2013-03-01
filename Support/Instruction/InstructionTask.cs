using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Instruction
{
    /// <summary>
    /// 指令任务类
    /// </summary>
    public class InstructionTask
    {
        private string _Ip;
        /// <summary>
        /// IP
        /// </summary>
        public string Ip
        {
            get
            {
                return this._Ip;
            }
            set
            {
                this._Ip = value;
            }
        }

        private string _Type;
        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }

        private List<AbstractInstruction> _Instructions;
        /// <summary>
        /// 指令集合
        /// </summary>
        public List<AbstractInstruction> Instructions
        {
            get
            {
                return this._Instructions;
            }
            set
            {
                this._Instructions = value;
            }
        }

        private bool _Done;
        /// <summary>
        /// 全局标识
        /// </summary>
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
    }
}

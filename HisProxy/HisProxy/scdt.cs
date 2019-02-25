
using System;
using System.Runtime.Serialization;

namespace HisService.DrSchedule
{
    /// <summary>
    /// 
    /// </summary>
    public class VisitScheduleDt
    {
        /// <summary>
        /// 
        /// </summary>

        public System.Int32 ScheduleNo { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public System.DateTime VisitDate { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public System.Byte VisitMark { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public System.String CurrDrNo { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public System.String NumberMark { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public System.Byte MaxSeqNo { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public System.String UpdateUser { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public System.DateTime? UpdateTime { get; set; }
    }
}
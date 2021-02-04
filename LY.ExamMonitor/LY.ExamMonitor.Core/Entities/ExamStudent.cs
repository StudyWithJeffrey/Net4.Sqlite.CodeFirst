 

namespace LY.ExamMonitor.Core.Entities
{
    /// <summary>
    /// 考生信息
    /// </summary>
    public class ExamStudent : BaseEntity
    { 

        public string StudentName { get; set; }

        public int? Age { get; set; }
         
        public bool? Gender { get; set; }
    }
}

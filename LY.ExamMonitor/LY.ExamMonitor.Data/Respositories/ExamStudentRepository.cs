using LY.ExamMonitor.Core.Entities;
using LY.ExamMonitor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LY.ExamMonitor.Data.Respositories
{
    public class ExamStudentRepository : EfRepository<ExamStudent>, IExamStudentRepository
    {

        public ExamStudentRepository(ExamMonitorContext context) : base(context)
        {

        }
    }
}

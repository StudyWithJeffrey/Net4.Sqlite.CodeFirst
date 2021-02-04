using LY.ExamMonitor.Core.Entities;
using LY.ExamMonitor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LY.ExamMonitor.Services
{
    /// <summary>
    /// 学生信息业务类
    /// </summary>
    public class ExamStudentService: IExamStudentService
    {

        private readonly IExamStudentRepository _examStudentRepository;

        public ExamStudentService(IExamStudentRepository examStudentRepository)
        {
            _examStudentRepository = examStudentRepository;
        }

        public void Init()
        {
            _examStudentRepository.Add(new ExamStudent
            {
                StudentName = "Jeffrey",
                Age = 18,
                Gender = true,
            });
        }


    }
}

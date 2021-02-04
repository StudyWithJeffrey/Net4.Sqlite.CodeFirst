using LY.ExamMonitor.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace LY.ExamMonitor.Data.Configuration
{
    public class Mappings
    {

        //public class ExamTaskMap : EntityTypeConfiguration<ExamTask>
        //{
        //    public ExamTaskMap() { }
        //}

        //public class ArrangePeriodMap : EntityTypeConfiguration<ArrangePeriod>
        //{
        //    public ArrangePeriodMap() { }
        //}

        //public class CenterMap : EntityTypeConfiguration<Center>
        //{
        //    public CenterMap() { }
        //}

        //public class DataConfigurationMap : EntityTypeConfiguration<DataConfiguration>
        //{
        //    public DataConfigurationMap() { }
        //}

        //public class ExamPlanMap : EntityTypeConfiguration<ExamPlan>
        //{
        //    public ExamPlanMap() { }
        //}

        //public class ExamRoomMap : EntityTypeConfiguration<ExamRoom>
        //{
        //    public ExamRoomMap() { }
        //}

        public class ExamStudentMap : EntityTypeConfiguration<ExamStudent>
        {
            public ExamStudentMap() { }
        }

        //public class PeriodExaminationMap : EntityTypeConfiguration<PeriodExamination>
        //{
        //    public PeriodExaminationMap() { }
        //}
    }
}

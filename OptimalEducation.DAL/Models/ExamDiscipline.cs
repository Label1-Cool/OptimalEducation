namespace OptimalEducation.DAL.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class ExamDiscipline
    {

        public ExamDiscipline()
        {

            this.UnitedStateExams = new HashSet<UnitedStateExam>();

            this.EducationLineRequirements = new HashSet<EducationLineRequirement>();

            this.Weights = new HashSet<Weight>();

        }


        public int Id { get; set; }
        [Display(Name = "����������")]
        public string Name { get; set; }



        public virtual ICollection<UnitedStateExam> UnitedStateExams { get; set; }

        /// <summary>
        /// ��� ��������
        /// </summary>
        public ExamType ExamType { get; set; }
        
        public virtual ICollection<EducationLineRequirement> EducationLineRequirements { get; set; }

        public virtual ICollection<Weight> Weights { get; set; }

    }

    /// <summary>
    /// ���� ���������
    /// </summary>
    public enum ExamType
    {
        UnitedStateExam,
        CustomWrittenExam,
        CustomOralExam,
        CustomExamOther
    }

}

namespace OptimalEducation.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// ����������  ��� ��� ����������� �������� � ������������
    /// </summary>
    public partial class UnitedStateExam
    {

        public int Id { get; set; }
        [Range(0,100)]
        [Display(Name = "���������")]
        public int Result { get; set; }

        public int ExamDisciplineId { get; set; }
        
        public int EntrantId { get; set; }


        [Display(Name = "����������")]
        public virtual ExamDiscipline Discipline { get; set; }
        [Display(Name = "�����������")]
        public virtual Entrant Entrant { get; set; }

    }

}

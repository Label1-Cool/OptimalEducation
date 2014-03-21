namespace OptimalEducation.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// ����������� �������� �� ����������
    /// </summary>
    public partial class EducationLine
    {

        public EducationLine()
        {

            this.EducationLinesRequirements = new HashSet<EducationLineRequirement>();

        }


        public int Id { get; set; }
        public Nullable<int> GeneralEducationLineId { get; set; }
        public int FacultyId { get; set; }
        /// <summary>
        /// ��� ����������� (����. 0105001)
        /// </summary>
        [Display(Name = "��� �����������")]
        public string Code { get; set; }
        /// <summary>
        /// ����� �������� (�������, ��������, �������)
        /// </summary>
        [Display(Name = "����� ��������")]
        public string EducationForm { get; set; }
        /// <summary>
        /// �������� �����������
        /// </summary>
        [Display(Name = "�������� �����������")]
        public string Name { get; set; }
        /// <summary>
        /// ����������� ����� �� ������ ���
        /// </summary>
        [Display(Name = "���. ����� ���")]
        public Nullable<int> RequiredSum { get; set; }
        /// <summary>
        /// ��������� �� �����������(��� �������� � ������ �� ������������)
        /// </summary>
        [Display(Name = "������������")]
        public bool Actual { get; set; }
        /// <summary>
        /// ��������� ��������
        /// </summary>
        [Display(Name = "��������� ��������"),DataType(DataType.Currency)]
        public int Price { get; set; }
        /// <summary>
        /// ���������� ������� ����
        /// </summary>
        [Display(Name = "���������� ������� ����")]
        public int PaidPlacesNumber { get; set; }
        /// <summary>
        /// ���������� ��������� ����
        /// </summary>
        [Display(Name = "���������� ��������� ����")]
        public int FreePlacesNumber { get; set; }

        public virtual GeneralEducationLine GeneralEducationLine { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual ICollection<EducationLineRequirement> EducationLinesRequirements { get; set; }

    }

}

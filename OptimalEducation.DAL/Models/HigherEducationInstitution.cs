namespace OptimalEducation.DAL.Models
{

    using System;
    using System.Collections.Generic;
    /// <summary>
    /// ��� ����
    /// </summary>
    public enum HigherEducationInstitutionType
    {
        University,
        Institute,
        Academy
    }    
 
    /// <summary>
    /// ���
    /// </summary>
    public partial class HigherEducationInstitution
    {

        public HigherEducationInstitution()
        {

            this.Faculties = new HashSet<Faculty>();

        }


        public int Id { get; set; }

        public string Name { get; set; }

        public int Prestige { get; set; }

        public int CityId { get; set; }

        public HigherEducationInstitutionType Type { get; set; }


        public virtual City City { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }

    }

}

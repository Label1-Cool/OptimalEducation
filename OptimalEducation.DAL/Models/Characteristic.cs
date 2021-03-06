namespace OptimalEducation.DAL.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// �������������� ��� ����� (������ ��� ������ ���� ����� � ������ �������������)
    /// </summary>
    public partial class Characteristic
    {

        public Characteristic()
        {

            this.Weights = new HashSet<Weight>();

        }


        public int Id { get; set; }
        
        public string Name { get; set; }

        public CharacteristicType Type { get; set; }

        public virtual ICollection<Weight> Weights { get; set; }

    }
    /// <summary>
    /// ���������� � ������ ���� ��������� �������� ��������������: ���������������, ����������, �����
    /// </summary>
    public enum CharacteristicType
	{
        Education,
        Physical,
        Etc
	}

}

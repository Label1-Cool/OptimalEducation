namespace OptimalEducation.DAL.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum OlypmpiadResult
    {
        [Display(Name = "������ �����")]
        FirstPlace,
        [Display(Name = "������ �����")]
        SecondPlace,
        [Display(Name = "������ �����")]
        ThirdPlace
    }

    public partial class ParticipationInOlympiad
    {

        public int Id { get; set; }
        [Display(Name = "���������")]
        public OlypmpiadResult Result { get; set; }
        [Display(Name = "����������")]
        public int EntrantId { get; set; }
        [Display(Name = "���������")]
        public int OlympiadId { get; set; }



        public virtual Entrant Entrant { get; set; }

        public virtual Olympiad Olympiad { get; set; }

    }

}

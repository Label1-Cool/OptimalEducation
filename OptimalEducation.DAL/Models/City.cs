namespace OptimalEducation.DAL.Models
{

using System;
    using System.Collections.Generic;
using System.Data.Entity.Spatial;
    
public partial class City
{

    public City()
    {

        this.Preferences = new HashSet<Preference>();

        this.HigherEducationInstitutions = new HashSet<HigherEducationInstitution>();

        this.Schools = new HashSet<School>();

    }


    public int Id { get; set; }

    public string Name { get; set; }
    
    public int Prestige { get; set; }

    public DbGeography Location { get; set; }

    public virtual ICollection<Preference> Preferences { get; set; }

    public virtual ICollection<HigherEducationInstitution> HigherEducationInstitutions { get; set; }

    public virtual ICollection<School> Schools { get; set; }

}

}

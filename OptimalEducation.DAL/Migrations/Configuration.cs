namespace OptimalEducation.DAL.Migrations
{
    using OptimalEducation.DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OptimalEducationDbContext>
    {
        OptimalEducationDbContext db = new OptimalEducationDbContext();
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OptimalEducationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            BaseEntitiesInit(context);
            //TODO: �������� ���������� ���� � ����(����������� �������� � ��)+ ��������� �� ������

            CreateEntrant();
        }

        private void CreateEntrant()
        {
            if(db.Entrants.SingleOrDefault(p=>p.Id==1)==null)
            {
                //������� ����������� � ������� �����������
                var entrant = new Entrant()
                {
                    Id = 1,
                    FirstName = "Alice",
                    Gender = "Female",
                };
                //��������� ��� ���������� �� ���
                foreach (var discipline in db.ExamDisciplines)
                {
                    entrant.UnitedStateExams.Add(
                        new UnitedStateExam
                        {
                            Discipline = discipline,
                            Entrant = entrant,
                            Result = 50,
                        });
                }
                //��������� ��� ���������� �� �������� ���������
                foreach (var schoolDisc in db.SchoolDisciplines)
                {
                    entrant.SchoolMarks.Add(
                        new SchoolMark
                        {
                            SchoolDiscipline = schoolDisc,
                            Entrant = entrant,
                            Result = 4,
                        });
                }
                db.Entrants.Add(entrant);
                db.SaveChanges();
            }
        }

        private static void BaseEntitiesInit(OptimalEducation.DAL.Models.OptimalEducationDbContext context)
        {
            var cities = new List<City>
            {
                new City { Id=1, Name = "������", Prestige = 90 },
                new City { Id=2, Name = "�����-���������", Prestige = 80 },
                new City { Id=3, Name = "������������", Prestige = 60 }
            };
            cities.ForEach(s => context.Cities.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var higherEducationInstitutions = new List<HigherEducationInstitution>
            {
                new HigherEducationInstitution { Id=1, Name = "���", Prestige = 90, CityId=cities.Single(p=>p.Name=="������").Id, Type=HigherEducationInstitutionType.University},
                new HigherEducationInstitution { Id=2, Name = "�����", Prestige = 80, CityId=cities.Single(p=>p.Name=="�����-���������").Id, Type=HigherEducationInstitutionType.University  },
                new HigherEducationInstitution { Id=3, Name = "����", Prestige = 60, CityId=cities.Single(p=>p.Name=="������������").Id, Type=HigherEducationInstitutionType.University }
            };
            higherEducationInstitutions.ForEach(s => context.HigherEducationInstitutions.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var faculties = new List<Faculty>
            {
                new Faculty {Id=1, Name = "������� ���1", Prestige = 90, HigherEducationInstitutionId=higherEducationInstitutions.Single(p=>p.Name=="���").Id},
                new Faculty {Id=2, Name = "������� �����1", Prestige = 80, HigherEducationInstitutionId=higherEducationInstitutions.Single(p=>p.Name=="�����").Id },
                new Faculty {Id=3, Name = "������� ����1", Prestige = 60, HigherEducationInstitutionId=higherEducationInstitutions.Single(p=>p.Name=="����").Id}
            };
            faculties.ForEach(s => context.Faculties.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var generalEducationLines = new List<GeneralEducationLine>
            {
                new GeneralEducationLine {Id=1, Name = "� ���������� � �����������", Code="1"},
                new GeneralEducationLine {Id=2, Name = "� �����������", Code="2" },
                new GeneralEducationLine {Id=3, Name = "� ������", Code="3"}
            };
            generalEducationLines.ForEach(s => context.GeneralEducationLines.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var educationLines = new List<EducationLine>
            {
                new EducationLine {Id=1, Name = "���������� � �����������", Actual=true,RequiredSum=250, Code="1122",
                    FacultyId=context.Faculties.First().Id,
                    GeneralEducationLine=context.GeneralEducationLines.Single(p=>p.Code=="1")},
                new EducationLine {Id=2, Name = "�����������", Actual=true,RequiredSum=260, Code="1122",
                    FacultyId=context.Faculties.First().Id,
                    GeneralEducationLine=context.GeneralEducationLines.Single(p=>p.Code=="2")},
                new EducationLine {Id=3, Name = "������", Actual=true,RequiredSum=220, Code="1122",
                    FacultyId=context.Faculties.First().Id,
                    GeneralEducationLine=context.GeneralEducationLines.Single(p=>p.Code=="3")}
            };
            educationLines.ForEach(s => context.EducationLines.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var clusters = new List<Characteristic>
            {
                new Characteristic { Id=1, Name = "������� ����"},
                new Characteristic { Id=2, Name = "����������"},
                new Characteristic { Id=3, Name = "�����������"},
                new Characteristic { Id=4, Name = "������"},
                new Characteristic { Id=5, Name = "�����"},
                new Characteristic { Id=6, Name = "���������� ����"},
            };
            clusters.ForEach(s => context.Characteristics.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var disciplines = new List<ExamDiscipline>
            {
                new ExamDiscipline { Id=1, Name = "������� ����"},
                new ExamDiscipline { Id=2, Name = "����������"},
                new ExamDiscipline { Id=3, Name = "�����������"},
                new ExamDiscipline { Id=4, Name = "������"},
                new ExamDiscipline { Id=5, Name = "�����"},
                new ExamDiscipline { Id=6, Name = "���������� ����"},
            };
            disciplines.ForEach(s => context.ExamDisciplines.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
            var schoolDiscipline = new List<SchoolDiscipline>
            {
                new SchoolDiscipline {Id=1, Name = "������� ����"},
                new SchoolDiscipline {Id=2, Name = "����������"},
                new SchoolDiscipline {Id=3, Name = "�����������"},
                new SchoolDiscipline {Id=4, Name = "������"},
                new SchoolDiscipline {Id=5, Name = "�����"},
                new SchoolDiscipline {Id=6, Name = "���������� ����"},
            };
            schoolDiscipline.ForEach(s => context.SchoolDisciplines.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
            var olympiads = new List<Olympiad>
            {
                new Olympiad {Id=1, Name = "������� ����"},
                new Olympiad {Id=2, Name = "����������"},
                new Olympiad {Id=3, Name = "�����������"},
                new Olympiad {Id=4, Name = "������"},
                new Olympiad {Id=5, Name = "�����"},
                new Olympiad {Id=6, Name = "���������� ����"},
            };
            olympiads.ForEach(s => context.Olympiads.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
            var sections = new List<Section>
            {
                new Section {Id=1, Name = "���"},
                new Section {Id=2, Name = "�������"},
                new Section {Id=3, Name = "����������"},
                new Section {Id=4, Name = "����"},
                new Section {Id=5, Name = "����"},
                new Section {Id=6, Name = "���������"},
            };
            sections.ForEach(s => context.Sections.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var schools = new List<School>
            {
                new School { Id=1, Name = "����� ��������",EducationQuality=3},
                new School { Id=2, Name = "����� ������", EducationQuality= 2},
                new School { Id=3, Name = "����� �����", EducationQuality=1}
            };
            schools.ForEach(s => context.Schools.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var hobbies = new List<Hobbie>
            {
                new Hobbie {Id=1, Name = "����� ������� ����"},
                new Hobbie {Id=2, Name = "����� ����������"},
                new Hobbie {Id=3, Name = "����� �����������"},
                new Hobbie {Id=4, Name = "����� ������"},
                new Hobbie {Id=5, Name = "����� �����"},
                new Hobbie {Id=6, Name = "����� ���������� ����"},
            };
            hobbies.ForEach(s => context.Hobbies.AddOrUpdate(s));
            context.SaveChanges();



            var educationLineRequirement = new List<EducationLineRequirement>
            {
                new EducationLineRequirement 
                {
                    Id=1, 
                    Requirement=50, 
                    EducationLineId=context.EducationLines.Single(p=>p.Name=="���������� � �����������").Id, 
                    ExamDisciplineId=context.ExamDisciplines.Single(p=>p.Name=="������� ����").Id
                },
                new EducationLineRequirement 
                {
                    Id=2, 
                    Requirement=70, 
                    EducationLineId=context.EducationLines.Single(p=>p.Name=="���������� � �����������").Id, 
                    ExamDisciplineId=context.ExamDisciplines.Single(p=>p.Name=="����������").Id
                },
                new EducationLineRequirement 
                {
                    Id=3, 
                    Requirement=70, 
                    EducationLineId=context.EducationLines.Single(p=>p.Name=="���������� � �����������").Id, 
                    ExamDisciplineId=context.ExamDisciplines.Single(p=>p.Name=="�����������").Id
                },
            };
            educationLineRequirement.ForEach(s => context.EducationLineRequirements.AddOrUpdate(s));
            context.SaveChanges();

            var weights = new List<Weight>
            {
                new Weight {Id=1, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="������� ����").Id,ExamDisciplineId=context.ExamDisciplines.Single(p=>p.Name=="������� ����").Id},
                new Weight {Id=2, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="����������").Id,ExamDisciplineId=context.ExamDisciplines.Single(p=>p.Name=="����������").Id},
                new Weight {Id=3, Coefficient = 0.7, ClusterId = context.Characteristics.Single(p=>p.Name=="�����������").Id,ExamDisciplineId=context.ExamDisciplines.Single(p=>p.Name=="����������").Id},
                new Weight {Id=4, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="�����������").Id,ExamDisciplineId=context.ExamDisciplines.Single(p=>p.Name=="�����������").Id},
                new Weight {Id=5, Coefficient = 0.7, ClusterId = context.Characteristics.Single(p=>p.Name=="����������").Id,ExamDisciplineId=context.ExamDisciplines.Single(p=>p.Name=="�����������").Id},

                new Weight {Id=6, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="������� ����").Id,SchoolDisciplineId=context.SchoolDisciplines.Single(p=>p.Name=="������� ����").Id},
                new Weight {Id=7, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="����������").Id,SchoolDisciplineId=context.SchoolDisciplines.Single(p=>p.Name=="����������").Id},
                new Weight {Id=8, Coefficient = 0.7, ClusterId = context.Characteristics.Single(p=>p.Name=="�����������").Id,SchoolDisciplineId=context.SchoolDisciplines.Single(p=>p.Name=="����������").Id},
                new Weight {Id=9, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="�����������").Id,SchoolDisciplineId=context.SchoolDisciplines.Single(p=>p.Name=="�����������").Id},
                new Weight {Id=10, Coefficient = 0.7, ClusterId = context.Characteristics.Single(p=>p.Name=="����������").Id,SchoolDisciplineId=context.SchoolDisciplines.Single(p=>p.Name=="�����������").Id},

                new Weight {Id=11, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="������� ����").Id,OlympiadId=context.Olympiads.Single(p=>p.Name=="������� ����").Id},
                new Weight {Id=12, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="����������").Id,OlympiadId=context.Olympiads.Single(p=>p.Name=="����������").Id},
                new Weight {Id=13, Coefficient = 0.7, ClusterId = context.Characteristics.Single(p=>p.Name=="�����������").Id,OlympiadId=context.Olympiads.Single(p=>p.Name=="����������").Id},
                new Weight {Id=14, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="�����������").Id,OlympiadId=context.Olympiads.Single(p=>p.Name=="�����������").Id},
                new Weight {Id=15, Coefficient = 0.7, ClusterId = context.Characteristics.Single(p=>p.Name=="����������").Id,OlympiadId=context.Olympiads.Single(p=>p.Name=="�����������").Id},

                new Weight {Id=16, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="������� ����").Id,SectionId=context.Sections.Single(p=>p.Name=="���").Id},
                new Weight {Id=17, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="����������").Id,SectionId=context.Sections.Single(p=>p.Name=="�������").Id},
                new Weight {Id=18, Coefficient = 0.7, ClusterId = context.Characteristics.Single(p=>p.Name=="�����������").Id,SectionId=context.Sections.Single(p=>p.Name=="�������").Id},
                new Weight {Id=19, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="�����������").Id,SectionId=context.Sections.Single(p=>p.Name=="����������").Id},
                new Weight {Id=20, Coefficient = 0.7, ClusterId = context.Characteristics.Single(p=>p.Name=="����������").Id,SectionId=context.Sections.Single(p=>p.Name=="����������").Id},

                new Weight {Id=21, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="������� ����").Id,SchoolId=context.Schools.Single(p=>p.Name=="����� ��������").Id},
                new Weight {Id=22, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="����������").Id,SchoolId=context.Schools.Single(p=>p.Name=="����� ������").Id},
                new Weight {Id=23, Coefficient = 0.7, ClusterId = context.Characteristics.Single(p=>p.Name=="�����������").Id,SchoolId=context.Schools.Single(p=>p.Name=="����� ������").Id},
                new Weight {Id=24, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="�����������").Id,SchoolId=context.Schools.Single(p=>p.Name=="����� �����").Id},
                new Weight {Id=25, Coefficient = 0.7, ClusterId = context.Characteristics.Single(p=>p.Name=="����������").Id,SchoolId=context.Schools.Single(p=>p.Name=="����� �����").Id},

                new Weight {Id=26, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="������� ����").Id,HobbieId=context.Hobbies.Single(p=>p.Name=="����� ������� ����").Id},
                new Weight {Id=27, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="����������").Id,HobbieId=context.Hobbies.Single(p=>p.Name=="����� ����������").Id},
                new Weight {Id=28, Coefficient = 0.7, ClusterId = context.Characteristics.Single(p=>p.Name=="�����������").Id,HobbieId=context.Hobbies.Single(p=>p.Name=="����� ����������").Id},
                new Weight {Id=29, Coefficient = 1, ClusterId = context.Characteristics.Single(p=>p.Name=="�����������").Id,HobbieId=context.Hobbies.Single(p=>p.Name=="����� �����������").Id},
                new Weight {Id=30, Coefficient = 0.7, ClusterId = context.Characteristics.Single(p=>p.Name=="����������").Id,HobbieId=context.Hobbies.Single(p=>p.Name=="����� �����������").Id},
            };
            weights.ForEach(s => context.Weights.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}

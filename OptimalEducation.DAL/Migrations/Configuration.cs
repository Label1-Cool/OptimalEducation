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
                new City { Id=1, Name = "������", Prestige = 90},
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
            //�� ������ ������ ����������� �� ������ ��� ���������.
            //� ���������� ���������� �������� � ����������� ����� �������
            var Characterisics = new List<Characteristic>
            {
                new Characteristic {Name = "������� ����"},
                new Characteristic {Name = "����������"},
                new Characteristic {Name = "�����������"},
                new Characteristic {Name = "������"},
                new Characteristic {Name = "�����"},
                new Characteristic {Name = "���������� ����"},
                new Characteristic {Name = "����������"},
                new Characteristic {Name = "�������"},
                new Characteristic {Name = "��������������"},
                new Characteristic {Name = "��������"},
                new Characteristic {Name = "���������"},
                new Characteristic {Name = "�������� ����"},
                new Characteristic {Name = "����������� ����"},
                new Characteristic {Name = "��������� ����"},
            };
            Characterisics.ForEach(s => context.Characteristics.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            Exams_Weight(context, Characterisics);
            SchoolDisciplines_Weight(context, Characterisics);
            Olympiads_Weight(context, Characterisics);
            var sections = new List<Section>
            {
                new Section {Name = "���"},
                new Section {Name = "�������"},
                new Section {Name = "����������"},
                new Section {Name = "����"},
                new Section {Name = "����"},
                new Section {Name = "���������"},
            };
            sections.ForEach(s => context.Sections.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var schools = new List<School>
            {
                new School {Name = "����� ��������",EducationQuality=3},
                new School {Name = "����� ������", EducationQuality= 2},
                new School {Name = "����� �����", EducationQuality=1}
            };
            schools.ForEach(s => context.Schools.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var hobbies = new List<Hobbie>
            {
                new Hobbie {Name = "����� ������� ����"},
                new Hobbie {Name = "����� ����������"},
                new Hobbie {Name = "����� �����������"},
                new Hobbie {Name = "����� ������"},
                new Hobbie {Name = "����� �����"},
                new Hobbie {Name = "����� ���������� ����"},
            };
            hobbies.ForEach(s => context.Hobbies.AddOrUpdate(p => p.Name, s));
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
        }

        private static void Olympiads_Weight(OptimalEducation.DAL.Models.OptimalEducationDbContext context, List<Characteristic> Characterisics)
        {
            //��� ���� ������, ���� ���� ������������(� ��� ����� ��� ����������� � �������������� ���)
            //���� �������� ����� �����(� �� �������, � �����)
            var olympiads = new List<Olympiad>
            {
                new Olympiad {Name = "������� ����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������� ����"),Coefficient=0.8},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.1},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������"),Coefficient=0.1},
                }},
                new Olympiad {Name = "����������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������"),Coefficient=0.2}
                }},
                new Olympiad {Name = "�����������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.3},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������� ����"),Coefficient=0.1}
                }},
                new Olympiad {Name = "������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����"),Coefficient=0.2}
                }},
                new Olympiad {Name = "�����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.2},
                }},
                new Olympiad {Name = "���������� ����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������� ����"),Coefficient=0.8},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������� ����"),Coefficient=0.1},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������� ����"),Coefficient=0.1},
                }},
            };
            olympiads.ForEach(s => context.Olympiads.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
        }

        private static void SchoolDisciplines_Weight(OptimalEducation.DAL.Models.OptimalEducationDbContext context, List<Characteristic> Characterisics)
        {
            //�� ������ ������ ������ �� 11 �����
            var schoolDiscipline = new List<SchoolDiscipline>
            {
                new SchoolDiscipline {Name = "�������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������"),Coefficient=0.2}
                }},
                new SchoolDiscipline {Name = "���������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.7},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������"),Coefficient=0.1}
                }},
                new SchoolDiscipline {Name = "�����������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������� ����"),Coefficient=0.2}
                }},
                new SchoolDiscipline {Name = "������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����"),Coefficient=0.2}
                }},
                new SchoolDiscipline {Name = "�����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������"),Coefficient=0.2}
                }},
                new SchoolDiscipline {Name = "��������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="��������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������"),Coefficient=0.2}
                }},
                new SchoolDiscipline {Name = "���������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="��������"),Coefficient=0.2}
                }},

                new SchoolDiscipline {Name = "�������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������"),Coefficient=0.2}
                }},
                new SchoolDiscipline {Name = "��������������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="��������������"),Coefficient=0.8},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������"),Coefficient=0.2},
                }},
                new SchoolDiscipline {Name = "����������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������� ����"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������"),Coefficient=0.2}
                }},
                
                new SchoolDiscipline {Name = "������� ����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������� ����"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������"),Coefficient=0.2}
                }},
                new SchoolDiscipline {Name = "���������� ����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������� ����"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������� ����"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������� ����"),Coefficient=0.2}
                }},
                new SchoolDiscipline {Name = "�������� ����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������� ����"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������� ����"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������� ����"),Coefficient=0.2}
                }},
                new SchoolDiscipline {Name = "����������� ����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������� ����"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������� ����"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������� ����"),Coefficient=0.2}
                }},
                new SchoolDiscipline {Name = "��������� ����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="��������� ����"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������� ����"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������� ����"),Coefficient=0.2}
                }},
            };
            schoolDiscipline.ForEach(s => context.SchoolDisciplines.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
        }

        private static void Exams_Weight(OptimalEducation.DAL.Models.OptimalEducationDbContext context, List<Characteristic> Characterisics)
        {
            var examDisciplines = new List<ExamDiscipline>
            {
                new ExamDiscipline {Name = "������� ����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������� ����"),Coefficient=0.8},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.1},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������"),Coefficient=0.1},
                }},
                new ExamDiscipline {Name = "����������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������"),Coefficient=0.2}
                }},
                new ExamDiscipline {Name = "�����������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.3},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������� ����"),Coefficient=0.1}
                }},
                new ExamDiscipline {Name = "������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����"),Coefficient=0.2}
                }},
                new ExamDiscipline {Name = "�����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������"),Coefficient=0.2}
                }},
                new ExamDiscipline {Name = "��������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="��������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�����"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������"),Coefficient=0.2}
                }},
                new ExamDiscipline {Name = "���������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="��������"),Coefficient=0.2}
                }},

                new ExamDiscipline {Name = "��������������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="��������������"),Coefficient=0.8},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������"),Coefficient=0.2}
                }},
                new ExamDiscipline {Name = "�������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="��������������"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������"),Coefficient=0.2}
                }},
                new ExamDiscipline {Name = "����������", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������� ����"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������"),Coefficient=0.2}
                }},

                new ExamDiscipline {Name = "���������� ����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������� ����"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������� ����"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������� ����"),Coefficient=0.2}
                }},
                new ExamDiscipline {Name = "�������� ����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="�������� ����"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������� ����"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������� ����"),Coefficient=0.2}
                }},
                new ExamDiscipline {Name = "����������� ����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="����������� ����"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������� ����"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������� ����"),Coefficient=0.2}
                }},
                new ExamDiscipline {Name = "��������� ����", Weights=new List<Weight>()
                {
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="��������� ����"),Coefficient=0.6},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="���������� ����"),Coefficient=0.2},
                    new Weight(){Characterisic=Characterisics.Single(p=>p.Name=="������� ����"),Coefficient=0.2}
                }},
            };
            examDisciplines.ForEach(s => context.ExamDisciplines.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
        }
    }
}

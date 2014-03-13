namespace OptimalEducation.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using OptimalEducation.Models;

    public struct Role
    {
        public const string Admin = "Admin";
        public const string Entrant = "Entrant";
        public const string Faculty = "Faculty"; 
    }

    internal sealed class Configuration : DbMigrationsConfiguration<OptimalEducation.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OptimalEducation.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
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

            var clusters = new List<Cluster>
            {
                new Cluster { Id=1, Name = "������� ����"},
                new Cluster { Id=2, Name = "����������"},
                new Cluster { Id=3, Name = "�����������"},
                new Cluster { Id=4, Name = "������"},
                new Cluster { Id=5, Name = "�����"},
                new Cluster { Id=6, Name = "���������� ����"},
            };
            clusters.ForEach(s => context.Clusters.AddOrUpdate(p => p.Name, s));
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
            var schoolTypes = new List<SchoolType>
            {
                new SchoolType {Id=1, Name = "������� ����"},
                new SchoolType {Id=2, Name = "����������"},
                new SchoolType {Id=3, Name = "�����������"},
                new SchoolType {Id=4, Name = "������"},
                new SchoolType {Id=5, Name = "�����"},
                new SchoolType {Id=6, Name = "���������� ����"},
            };
            schoolTypes.ForEach(s => context.SchoolTypes.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var hobbies = new List<Hobbie>
            {
                new Hobbie {Id=1, Name = "������� ����"},
                new Hobbie {Id=2, Name = "����������"},
                new Hobbie {Id=3, Name = "�����������"},
                new Hobbie {Id=4, Name = "������"},
                new Hobbie {Id=5, Name = "�����"},
                new Hobbie {Id=6, Name = "���������� ����"},
            };
            hobbies.ForEach(s => context.Hobbies.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var weights = new List<Weight>
            {
                new Weight {Id=1, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="������� ����").Id,ExamDisciplineId=context.ExamDisciplines.Single(p=>p.Name=="������� ����").Id},
                new Weight {Id=2, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="����������").Id,ExamDisciplineId=context.ExamDisciplines.Single(p=>p.Name=="����������").Id},
                new Weight {Id=3, Coefficient = 0.7, ClusterId = context.Clusters.Single(p=>p.Name=="�����������").Id,ExamDisciplineId=context.ExamDisciplines.Single(p=>p.Name=="����������").Id},
                new Weight {Id=4, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="�����������").Id,ExamDisciplineId=context.ExamDisciplines.Single(p=>p.Name=="�����������").Id},
                new Weight {Id=5, Coefficient = 0.7, ClusterId = context.Clusters.Single(p=>p.Name=="����������").Id,ExamDisciplineId=context.ExamDisciplines.Single(p=>p.Name=="�����������").Id},

                new Weight {Id=6, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="������� ����").Id,SchoolDisciplineId=context.SchoolDisciplines.Single(p=>p.Name=="������� ����").Id},
                new Weight {Id=7, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="����������").Id,SchoolDisciplineId=context.SchoolDisciplines.Single(p=>p.Name=="����������").Id},
                new Weight {Id=8, Coefficient = 0.7, ClusterId = context.Clusters.Single(p=>p.Name=="�����������").Id,SchoolDisciplineId=context.SchoolDisciplines.Single(p=>p.Name=="����������").Id},
                new Weight {Id=9, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="�����������").Id,SchoolDisciplineId=context.SchoolDisciplines.Single(p=>p.Name=="�����������").Id},
                new Weight {Id=10, Coefficient = 0.7, ClusterId = context.Clusters.Single(p=>p.Name=="����������").Id,SchoolDisciplineId=context.SchoolDisciplines.Single(p=>p.Name=="�����������").Id},

                new Weight {Id=11, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="������� ����").Id,SchoolDisciplineId=context.Olympiads.Single(p=>p.Name=="������� ����").Id},
                new Weight {Id=12, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="����������").Id,SchoolDisciplineId=context.Olympiads.Single(p=>p.Name=="����������").Id},
                new Weight {Id=13, Coefficient = 0.7, ClusterId = context.Clusters.Single(p=>p.Name=="�����������").Id,SchoolDisciplineId=context.Olympiads.Single(p=>p.Name=="����������").Id},
                new Weight {Id=14, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="�����������").Id,SchoolDisciplineId=context.Olympiads.Single(p=>p.Name=="�����������").Id},
                new Weight {Id=15, Coefficient = 0.7, ClusterId = context.Clusters.Single(p=>p.Name=="����������").Id,SchoolDisciplineId=context.Olympiads.Single(p=>p.Name=="�����������").Id},

                new Weight {Id=16, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="������� ����").Id,SchoolDisciplineId=context.Sections.Single(p=>p.Name=="���").Id},
                new Weight {Id=17, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="����������").Id,SchoolDisciplineId=context.Sections.Single(p=>p.Name=="�������").Id},
                new Weight {Id=18, Coefficient = 0.7, ClusterId = context.Clusters.Single(p=>p.Name=="�����������").Id,SchoolDisciplineId=context.Sections.Single(p=>p.Name=="�������").Id},
                new Weight {Id=19, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="�����������").Id,SchoolDisciplineId=context.Sections.Single(p=>p.Name=="����������").Id},
                new Weight {Id=20, Coefficient = 0.7, ClusterId = context.Clusters.Single(p=>p.Name=="����������").Id,SchoolDisciplineId=context.Sections.Single(p=>p.Name=="����������").Id},

                new Weight {Id=21, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="������� ����").Id,SchoolDisciplineId=context.SchoolTypes.Single(p=>p.Name=="������� ����").Id},
                new Weight {Id=22, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="����������").Id,SchoolDisciplineId=context.SchoolTypes.Single(p=>p.Name=="����������").Id},
                new Weight {Id=23, Coefficient = 0.7, ClusterId = context.Clusters.Single(p=>p.Name=="�����������").Id,SchoolDisciplineId=context.SchoolTypes.Single(p=>p.Name=="����������").Id},
                new Weight {Id=24, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="�����������").Id,SchoolDisciplineId=context.SchoolTypes.Single(p=>p.Name=="�����������").Id},
                new Weight {Id=25, Coefficient = 0.7, ClusterId = context.Clusters.Single(p=>p.Name=="����������").Id,SchoolDisciplineId=context.SchoolTypes.Single(p=>p.Name=="�����������").Id},

                new Weight {Id=26, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="������� ����").Id,SchoolDisciplineId=context.Hobbies.Single(p=>p.Name=="������� ����").Id},
                new Weight {Id=27, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="����������").Id,SchoolDisciplineId=context.Hobbies.Single(p=>p.Name=="����������").Id},
                new Weight {Id=28, Coefficient = 0.7, ClusterId = context.Clusters.Single(p=>p.Name=="�����������").Id,SchoolDisciplineId=context.Hobbies.Single(p=>p.Name=="����������").Id},
                new Weight {Id=29, Coefficient = 1, ClusterId = context.Clusters.Single(p=>p.Name=="�����������").Id,SchoolDisciplineId=context.Hobbies.Single(p=>p.Name=="�����������").Id},
                new Weight {Id=30, Coefficient = 0.7, ClusterId = context.Clusters.Single(p=>p.Name=="����������").Id,SchoolDisciplineId=context.Hobbies.Single(p=>p.Name=="�����������").Id},
            };
            weights.ForEach(s => context.Weights.AddOrUpdate(s));
            context.SaveChanges();

            CreateRoles(context);

            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            CreateAdminUser(UserManager);
            
            CreateEntrantUsers(UserManager);
            var user =UserManager.FindByName("Entrant1");
            foreach (var disc in context.ExamDisciplines)
            {
                if (!user.Entrant.UnitedStateExams.Any(p => p.Discipline == disc && p.EntrantId == user.Entrant.Id))
                user.Entrant.UnitedStateExams.Add(
                    new UnitedStateExam
                       {
                           Discipline = disc,
                           Entrant = user.Entrant,
                           Result=50,
                       });
            }
            foreach (var schoolDisc in context.SchoolDisciplines)
            {
                if (!user.Entrant.SchoolMarks.Any(p => p.SchoolDiscipline == schoolDisc && p.EntrantId == user.Entrant.Id))
                    user.Entrant.SchoolMarks.Add(
                        new SchoolMark
                        {
                            SchoolDiscipline = schoolDisc,
                            Entrant = user.Entrant,
                            Result = 4,
                        });
            }
            context.SaveChanges();

            CreateFacultyUsers(UserManager, faculties);
        }

        private void CreateRoles(OptimalEducation.Models.ApplicationDbContext context)
        {
            RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new
        RoleStore<IdentityRole>(context));
            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(Role.Admin))
            {
                var roleresult = RoleManager.Create(new IdentityRole(Role.Admin));
            }
            if (!RoleManager.RoleExists(Role.Entrant))
            {
                var roleresult = RoleManager.Create(new IdentityRole(Role.Entrant));
            }
            if (!RoleManager.RoleExists(Role.Faculty))
            {
                var roleresult = RoleManager.Create(new IdentityRole(Role.Faculty));
            }
        }

        private void CreateAdminUser(UserManager<ApplicationUser> UserManager)
        {
            var user = new ApplicationUser();
            user.UserName = "Administrator";
            const string password = "password";

            if (UserManager.FindByName(user.UserName) == null)
            {
                var identityResult = UserManager.Create(user, password);

                if (identityResult.Succeeded)
                {
                    UserManager.AddToRole(user.Id, Role.Admin);
                }
            }
        }
        private void CreateEntrantUsers(UserManager<ApplicationUser> UserManager)
        {
            var user = new ApplicationUser();
            user.UserName = "Entrant1";
            const string password = "password";

            user.Entrant = new Entrant();
            if (UserManager.FindByName(user.UserName) == null)
            {
                var identityResult = UserManager.Create(user, password);

                if (identityResult.Succeeded)
                {
                    UserManager.AddToRole(user.Id, Role.Entrant);
                }
            }
        }
        private void CreateFacultyUsers(UserManager<ApplicationUser> UserManager, List<Faculty> higherEducationInstitutions)
        {
            var user = new ApplicationUser();
            user.UserName = "Faculty1";
            user.FacultyId = higherEducationInstitutions.First().Id;
            const string password = "password";

            if (UserManager.FindByName(user.UserName) == null)
            {
                var identityResult = UserManager.Create(user, password);

                if (identityResult.Succeeded)
                {
                    UserManager.AddToRole(user.Id, Role.Faculty);
                }
            }
        }
        private void CreateUniqueUserAndAddRole(UserManager<ApplicationUser> UserManager, ApplicationUser user, string password)
        {
            if (UserManager.FindByName(user.UserName) == null)
            {
                var identityResult = UserManager.Create(user, password);

                if (identityResult.Succeeded)
                {
                    UserManager.AddToRole(user.Id, Role.Admin);
                }
            }

        }
    }
}

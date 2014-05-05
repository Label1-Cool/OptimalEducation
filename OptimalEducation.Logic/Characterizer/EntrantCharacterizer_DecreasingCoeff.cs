﻿using OptimalEducation.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalEducation.Logic.Characterizer
{
    /// <summary>
    /// Результаты характеристикиизации для пользователя по методу уменьш коэф-в:
    /// Вычисляются значения для каждой характеристики(например, потенциал в математике, информатике, русском)
    /// Эти значения не суммируются сразу, а складываются в списки, соотносящиеся с данной характеристикой
    /// Позже эти значения сортируются и домножаются на убывающий коэффициент
    /// </summary>
    public class EntrantCharacterizer_DecreasingCoeff
    {
        Entrant _entrant;
        List<string> educationCharacterisiticNames;

        //Dictionary<string, double> _totalCharacteristics = new Dictionary<string, double>();
        //public Dictionary<string, double> Characteristics { get { return _totalCharacteristics; } }

        public EntrantCharacterizer_DecreasingCoeff(Entrant entrant)
        {
            _entrant = entrant;
            InitCharacterisitcs();
            CalculateSum();
        }
        private void InitCharacterisitcs()
        {
            //Заполняем словарь всеми ключами по возможным весам
            OptimalEducationDbContext context = new OptimalEducationDbContext();
            educationCharacterisiticNames = context.Characteristics
                .Where(p=>p.Type==CharacteristicType.Education)
                .Select(p => p.Name)
                .ToList();
        }
        #region Добавляем в списки слагаемые для сложени(для каждой характеристики)
        private Dictionary<string, double> Characterising(Action<Dictionary<string, List<double>>> partSumsMethod)
        {
            var characteristicAddItems = new Dictionary<string, List<double>>();
            var resultCharacteristics = new Dictionary<string, double>();
            foreach (var name in educationCharacterisiticNames)
            {
                characteristicAddItems.Add(name, new List<double>());
                resultCharacteristics.Add(name, 0);
            }

            //Т.к. олимпиад может быть очень много, результаты будет складывать в отдельный список(по хар-кам).
            partSumsMethod(characteristicAddItems);

            //Логика суммирования
            foreach (var item in characteristicAddItems)
            {
                item.Value.Sort();
                var itemList = item.Value;
                //Используем идею геометрической прогрессии
                //Наибольший вклад вносит первое(наибольшее) значение. 
                //чем больше результатов, тем меньше их вклад
                //Пример: 4,3,3,2: 4*1+3/2 + 3/4 +2/8
                double b = 1;
                double sum = 0;
                for (int i = 0; i < itemList.Count; i++)
                {
                    sum += itemList[i] * b;
                    b = b / 2;
                }

                resultCharacteristics[item.Key] = sum;
            }
            return resultCharacteristics;
        }

        private void CreateUnatedStateExamPartSums(Dictionary<string, List<double>> unatedStateExamCharactericAddItems)
        {
            //Вычисляем пары "название кластера"+"спискок частичных сумм"
            foreach (var exam in _entrant.UnitedStateExams)
            {
                if (exam.Result.HasValue)
                {
                    double result = exam.Result.Value / 100.0;//нормализованный результат(100б=1.00, 70,=0.7)
                    var discipline = exam.Discipline;
                    foreach (var weight in discipline.Weights)
                    {
                        var coeff = weight.Coefficient;
                        var characteristicName = weight.Characterisic.Name;

                        var characteristicResult = result * coeff;
                        unatedStateExamCharactericAddItems[characteristicName].Add(characteristicResult);
                    }
                }
            }
        }

        private void CreateSchoolMarkPartSums(Dictionary<string, List<double>> schoolMarkCharactericAddItems)
        {
            foreach (var shoolMark in _entrant.SchoolMarks)
            {
                if (shoolMark.Result.HasValue)
                {
                    //нормализованный результат(5=1.00)
                    double result = shoolMark.Result.Value / 5.0;
                    var discipline = shoolMark.SchoolDiscipline;
                    foreach (var weight in discipline.Weights)
                    {
                        var coeff = weight.Coefficient;
                        var characteristicName = weight.Characterisic.Name;

                        var characteristicResult = result * coeff;
                        schoolMarkCharactericAddItems[characteristicName].Add(characteristicResult);
                    }
                }
            }
        }

        private void CreateOlympiadPartSums(Dictionary<string, List<double>> olympiadCharacteristicAddItems)
        {
            foreach (var olympResult in _entrant.ParticipationInOlympiads)
            {
                var result = olympResult.Result;
                var olympiad = olympResult.Olympiad;
                foreach (var weight in olympiad.Weights)
                {
                    var coeff = weight.Coefficient;
                    var characteristicName = weight.Characterisic.Name;

                    double characteristicResult = 0;
                    switch (result)
                    {
                        case OlypmpiadResult.FirstPlace: characteristicResult = ((double)result / 100) * coeff;
                            break;
                        case OlypmpiadResult.SecondPlace: characteristicResult = ((double)result / 100) * coeff;
                            break;
                        case OlypmpiadResult.ThirdPlace: characteristicResult = ((double)result / 100) * coeff;
                            break;
                    }

                    olympiadCharacteristicAddItems[characteristicName].Add(characteristicResult);
                }
            }
        }

        private Dictionary<string, double> SectionCharacterising()
        {
            Dictionary<string, List<double>> sectionCharactericAddItems = new Dictionary<string, List<double>>();
            Dictionary<string, double> resultCharacteristics = new Dictionary<string, double>();
            foreach (var name in educationCharacterisiticNames)
            {
                sectionCharactericAddItems.Add(name, new List<double>());
                resultCharacteristics.Add(name, 0);
            }

            foreach (var sectionResult in _entrant.ParticipationInSections)
            {
                double result = 0;
                //По правилу 80/20?
                if (sectionResult.YearPeriod >= 10) result = 1.00;
                else if (sectionResult.YearPeriod>5) result = 0.90;
                else if (sectionResult.YearPeriod > 2) result = 0.80;
                else if (sectionResult.YearPeriod > 1) result = 0.40;
                else if (sectionResult.YearPeriod > 0.5) result = 0.20;

                var section = sectionResult.Section;
                foreach (var weight in section.Weights)
                {
                    var coeff = weight.Coefficient;
                    var characteristicName = weight.Characterisic.Name;

                    //TODO: Реализовать особую логику учета данных?
                    double characteristicResult = result * coeff;

                    sectionCharactericAddItems[characteristicName].Add(characteristicResult);
                }
            }

            //TODO: Логика суммирования
            foreach (var item in sectionCharactericAddItems)
            {
                item.Value.Sort();
                //Складываем
                double sum = 0;
                //TODO: сложить по правилу
                resultCharacteristics[item.Key] = sum;
            }
            return resultCharacteristics;
        }

        private Dictionary<string, double> HobbieCharacterising()
        {
            Dictionary<string, List<double>> hobbieCharactericAddItems = new Dictionary<string, List<double>>();
            Dictionary<string, double> resultCharacteristics = new Dictionary<string, double>();
            foreach (var name in educationCharacterisiticNames)
            {
                hobbieCharactericAddItems.Add(name, new List<double>());
                resultCharacteristics.Add(name, 0);
            }

            foreach (var hobbieResult in _entrant.Hobbies)
            {
                foreach (var weight in hobbieResult.Weights)
                {
                    var coeff = weight.Coefficient;
                    var characteristicName = weight.Characterisic.Name;

                    //TODO: Реализовать особую логику учета данных?
                    //пока просто учитывается наличие хобби как факт
                    double someValue = 1;
                    double characteristicResult = someValue * coeff;

                    hobbieCharactericAddItems[characteristicName].Add(characteristicResult);
                }
            }

            //TODO: Логика суммирования
            foreach (var item in hobbieCharactericAddItems)
            {
                item.Value.Sort();
                //Складываем
                double sum = 0;
                //TODO: сложить по правилу
                resultCharacteristics[item.Key] = sum;
            }
            return resultCharacteristics;
        }

        private Dictionary<string, double> SchoolTypeCharacterising()
        {
            Dictionary<string, List<double>> schoolTypeCharactericAddItems = new Dictionary<string, List<double>>();
            Dictionary<string, double> resultCharacteristics = new Dictionary<string, double>();
            foreach (var name in educationCharacterisiticNames)
            {
                schoolTypeCharactericAddItems.Add(name, new List<double>());
                resultCharacteristics.Add(name, 0);
            }

            //для простоты будем брать последнюю школу, где абитуриент учился
            //(возможно стоит рассмотреть более сложный вариант в будущем)
            var lastParticipationInSchool = _entrant.ParticipationInSchools.LastOrDefault();
            if(lastParticipationInSchool!=null)
            {
                //Или еще учитывать кол-во лет обучения?
                var quality = lastParticipationInSchool.School.EducationQuality/100.0;
                var schoolWeights = lastParticipationInSchool.School.Weights;
                foreach (var weight in schoolWeights)
                {
                    var coeff = weight.Coefficient;
                    var characteristicName = weight.Characterisic.Name;

                    //TODO: Реализовать особую логику учета данных?
                    double characteristicResult = quality * coeff;

                    schoolTypeCharactericAddItems[characteristicName].Add(characteristicResult);
                }
            }

            //TODO: Логика суммирования
            foreach (var item in schoolTypeCharactericAddItems)
            {
                item.Value.Sort();
                //Складываем
                double sum = 0;
                //TODO: сложить по правилу
                resultCharacteristics[item.Key] = sum;
            }
            return resultCharacteristics;
        }
        #endregion

        /// <summary>
        /// Складывает результаты каждого частного характеристикиа по определенному правилу
        /// </summary>
        private void CalculateSum()
        {
            //Вычисляем частичные характеристики
            //в результатер работы каждой функции получается новая таблица характеристик
            var unatedStateExamCharacteristics = Characterising(CreateUnatedStateExamPartSums);
            var schoolMarkCharacteristics = Characterising(CreateSchoolMarkPartSums);
            var olympiadCharacteristics = Characterising(CreateOlympiadPartSums);
            //TODO: Остальные методы(хобби, секции и пр)

            //Просто складываем и делим на норм. число
            //(Или складываем по аналогии с геом. прогрессией и делим на норм число?)

        }
    }
}

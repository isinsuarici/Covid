using System;
using System.Collections.Generic;
using System.Linq;

namespace Covid
{
   
    class People
    {
        private string name;
        private int age;
        private string gender;
        
        public People(string name, int age, string gender) {
            this.name = name;
            this.age = age;
            this.gender = gender;
         }
        public virtual void Display() {
            Console.WriteLine($"Name:{this.name} Gender: {this.gender} Age: {this.age}");
        }

        public string GetName() { return name; } 
        public void SetName(string name) { this.name = name; } 
        public string GetGender() { return gender; }
        public void SetGender(string gender) { this.gender = gender; }
        public int GetAge() { return age; }
        public void SetAge(int age) { this.age = age; } 
    }
    class HighRiskCovidPatient: People
    {
        public HighRiskCovidPatient(string name, int age, string gender) : base(name, age, gender) { }
        
        public override void Display() {
            Console.WriteLine($"High risk COVID-19 patient:\n Name:{this.GetName()} Gender: {this.GetGender()} Age: {this.GetAge()}");
        }
    }
    class LowRiskCovidPatient: People
    {
        public LowRiskCovidPatient(string name, int age, string gender) : base(name, age, gender) { }

        public override void Display() {
            Console.WriteLine($"Low risk COVID-19 patient:\n Name:{this.GetName()} Gender: {this.GetGender()} Age: {this.GetAge()}");
        }
    }
    class CovidSelfIzolatePatient:People
    {
        public CovidSelfIzolatePatient(string name, int age, string gender) : base(name, age, gender) { }

        public override void Display() {
            Console.WriteLine($"COVID-19 self-isolating patient:\n Name:{this.GetName()} Gender: {this.GetGender()} Age: {this.GetAge()}");

        }
    }
    class CovidPositivePeople : People
    {
        public CovidPositivePeople(string name, int age, string gender) : base(name, age, gender) { }

        public override void Display() {
            Console.WriteLine($"COVID-19 positive people:\n Name:{this.GetName()} Gender: {this.GetGender()} Age: {this.GetAge()}");

        }
    }
    class CovidNegativePeople : People
    {
        public CovidNegativePeople(string name, int age, string gender) : base(name, age, gender) { }

        public override void Display() {
            Console.WriteLine($"COVID-19 negative or inconclusive people:\n Name:{this.GetName()} Gender: {this.GetGender()} Age: {this.GetAge()}");

        }
    }
    class Program
    {
        static List<People> GeneratePatientList()
        {
            #region Generate People list
            List<People> patientPeopleList = new List<People>();
            string name;
            int age, nameIndex, surnameIndex;
            string[] maleNameArr = { "James", "John", "Robert", "Michael", "William", "David", "Richard", "Charles", "Joseph","Thomas" };
            string[] femaleNameArr = { "Mary", "Patricia", "Linda", "Barbara", "Elizabeth", "Jennifer", "Susan", "Margaret","Lisa", "Nancy" };
            string[] surnameArr = { "Adams", "Allen", "Anderson", "Atkins", "Baker", "Barnes", "Bell", "Bennet", "Cooper","Forester","Foster", "Fox", "Gardener", "Hamilton", "Harris", "Marshall", "Murphy", "Parker", "Richardson", "Simpson"};
            //Generate Females
            age = 10;
            for (int i = 0; i < 100; i++)
            {
                nameIndex = i % femaleNameArr.Count();
                surnameIndex = i % surnameArr.Count();
                name = femaleNameArr[nameIndex] + " " + surnameArr[surnameIndex];
                age++;
                if (age > 80)
                {
                    age = 10;
                }
                People newPeople = new People(name, age, "female");
                patientPeopleList.Add(newPeople);
            }
            //Generate Males
            age = 80;
            for (int i = 0; i < 100; i++)
            {
                nameIndex = i % maleNameArr.Count();
                surnameIndex = i % surnameArr.Count();
                name = maleNameArr[nameIndex] + " " + surnameArr[surnameIndex];
                age--;
                if (age < 10)
                {
                    age = 80;
                }
                People newPeople = new People(name, age, "male");
                patientPeopleList.Add(newPeople);
            }
            //Order people by age
            patientPeopleList = patientPeopleList.OrderBy(p => p.GetAge()).ToList();
            return patientPeopleList;
            #endregion
        }
        static void Main(string[] args)
        {
            List<People> patientPeopleList = GeneratePatientList(); //Generate patients
            List<HighRiskCovidPatient> highRiskPatientPeopleList = new List<HighRiskCovidPatient>(); //High risk exposure patients
            List<LowRiskCovidPatient> lowRiskPatientPeopleList = new List<LowRiskCovidPatient>(); //Low risk exposure patients
            List<CovidSelfIzolatePatient> covidSelfIzolatePeopleList = new List<CovidSelfIzolatePatient>(); //COVID-19 self isolate people
            List<CovidNegativePeople> covidNegativePeopleList = new List<CovidNegativePeople>(); //COVID-19 negative or inconclusive people
            List<CovidPositivePeople> covidPossitivePeopleList = new List<CovidPositivePeople>(); //COVID-19 positive people
            int high_risk_exposure_rate = 30;
            int high_risk_exposure_symptoms_rate = 60;
            int low_risk_exposure_symptoms_rate = 25;
            int laboratory_testing_positive_rate = 30;

            int HighRiskCovidPatientc = (patientPeopleList.Count() * high_risk_exposure_rate / 100);
            int LowRiskCovidPatientc = (patientPeopleList.Count() * (100-high_risk_exposure_rate) / 100);
            int CovidSelfIzolatePatientforhigh = (HighRiskCovidPatientc* high_risk_exposure_symptoms_rate/100);
            int CovidSelfIzolatePatientforlow = (LowRiskCovidPatientc * low_risk_exposure_symptoms_rate/100);
            int CovidPositivePeoplec =((CovidSelfIzolatePatientforhigh + CovidSelfIzolatePatientforlow) * laboratory_testing_positive_rate /100);
            int CovidNegativePeoplec = CovidSelfIzolatePatientforhigh+ CovidSelfIzolatePatientforlow - CovidPositivePeoplec; 

            Random random = new Random();
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            List<int> list3 = new List<int>();
            for (int i = 0; i < HighRiskCovidPatientc; i++)
            {
                int index = random.Next(0, patientPeopleList.Count);
                if (list1.Contains(index))
                    i--;

                else
                {
                    list1.Add(index);
                    HighRiskCovidPatient hrcp = new HighRiskCovidPatient(patientPeopleList[index].GetName(), patientPeopleList[index].GetAge(), patientPeopleList[index].GetGender());
                    highRiskPatientPeopleList.Add(hrcp);
                }
            }
            Console.WriteLine(highRiskPatientPeopleList.Count());
            highRiskPatientPeopleList = highRiskPatientPeopleList.OrderBy(p => p.GetAge()).ToList();
            highRiskPatientPeopleList.ForEach(p => p.Display());
            Console.WriteLine("*************************************************************************");

            for (int i = 0; i < LowRiskCovidPatientc; i++)
            {
                int index = random.Next(0, patientPeopleList.Count);
                if (list1.Contains(index))
                    i--;

                else
                {
                    LowRiskCovidPatient lrcp = new LowRiskCovidPatient(patientPeopleList[index].GetName(), patientPeopleList[index].GetAge(), patientPeopleList[index].GetGender());
                    lowRiskPatientPeopleList.Add(lrcp);
                }
            }
            Console.WriteLine(lowRiskPatientPeopleList.Count());
            lowRiskPatientPeopleList = lowRiskPatientPeopleList.OrderBy(p => p.GetAge()).ToList();
            lowRiskPatientPeopleList.ForEach(p => p.Display());
            Console.WriteLine("*************************************************************************");


            for (int i=0; i< CovidSelfIzolatePatientforhigh; i++)
            {
                int index = random.Next(0, patientPeopleList.Count);
                if (list1.Contains(index))
                {
                    if (list2.Contains(index))
                        i--;

                    else
                    {

                        list2.Add(index);
                        CovidSelfIzolatePatient csip = new CovidSelfIzolatePatient(patientPeopleList[index].GetName(), patientPeopleList[index].GetAge(), patientPeopleList[index].GetGender());
                        covidSelfIzolatePeopleList.Add(csip);

                    }
                }
                else
                    i--;
            }

            for (int i = 0; i < CovidSelfIzolatePatientforlow; i++)
            {
                int index = random.Next(0, patientPeopleList.Count);
                if (!list1.Contains(index))
                {
                    if (list2.Contains(index))
                        i--;

                    else
                    {

                        list2.Add(index);
                        CovidSelfIzolatePatient csip = new CovidSelfIzolatePatient(patientPeopleList[index].GetName(), patientPeopleList[index].GetAge(), patientPeopleList[index].GetGender());
                        covidSelfIzolatePeopleList.Add(csip);

                    }
                }
                else
                    i--;
            }

            Console.WriteLine(covidSelfIzolatePeopleList.Count());
            covidSelfIzolatePeopleList = covidSelfIzolatePeopleList.OrderBy(p => p.GetAge()).ToList();
            covidSelfIzolatePeopleList.ForEach(p => p.Display());
            Console.WriteLine("*************************************************************************");

            for (int i=0; i< CovidPositivePeoplec; i++)
            {
                int index = random.Next(0, patientPeopleList.Count);
                if (list1.Contains(index))
                {

                    if (list2.Contains(index))
                        i--;

                    else
                    {
                        list2.Add(index);
                        list3.Add(index);
                        CovidPositivePeople cpp = new CovidPositivePeople(patientPeopleList[index].GetName(), patientPeopleList[index].GetAge(), patientPeopleList[index].GetGender());
                        covidPossitivePeopleList.Add(cpp);
                    }
                }
                else
                    i--;

            }
            Console.WriteLine(covidPossitivePeopleList.Count());
            covidPossitivePeopleList = covidPossitivePeopleList.OrderBy(p => p.GetAge()).ToList();
            covidPossitivePeopleList.ForEach(p => p.Display());
            Console.WriteLine("*************************************************************************");

            for (int i = 0; i < patientPeopleList.Count; i++)
            {
                //int index = random.Next(0, patientPeopleList.Count);

                if (!list3.Contains(i))
                {
                    CovidNegativePeople cnp = new CovidNegativePeople(patientPeopleList[i].GetName(), patientPeopleList[i].GetAge(), patientPeopleList[i].GetGender());
                    covidNegativePeopleList.Add(cnp);
                }
                    
                

            }
            Console.WriteLine(covidNegativePeopleList.Count());
            covidNegativePeopleList = covidNegativePeopleList.OrderBy(p => p.GetAge()).ToList();
            covidNegativePeopleList.ForEach(p => p.Display());
            Console.WriteLine("*************************************************************************");
            Console.ReadLine();
        }
      
    }
}

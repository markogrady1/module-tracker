using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OOP._0._1._1
{
    class Prediction
    {
        private string[] _completeModuleArray;
        private int _assessmentAmount;
        private int[] _moduleGrades;
        private float _avgOne;
        private float _avgTwo;
        private float _avgThree;
        private float _avgFour;
        private string _moduleName;
        private string _moduleCode;
        private string _courseId;
        private int minimum;

        public int Minimum
        {
            get { return minimum; }
        }

        public float AvgOne
        {
            get { return _avgOne; }
        }

        public float AvgTwo
        {
            get { return _avgTwo; }
        }

        public float AvgThree
        {
            get { return _avgThree; }
        }

        public float AvgFour
        {
            get { return _avgFour; }
        }

        public string ModuleName
        {
            get { return _moduleName; }
        }


        public string ModuleCode
        {
            get { return _moduleCode; }
        }

        public string CourseId
        {
            get { return _courseId; }
        }


        public int AssessmentAmount
        {
            get { return _assessmentAmount; }
        }

        public void modulePrediction(string moduleValues)
        {
            _completeModuleArray = moduleValues.Split(',');
            _moduleGrades = GetAssessmentGrades(_completeModuleArray);
            _assessmentAmount = GetAmountOfAssessments(_completeModuleArray);
            _courseId = _completeModuleArray[3];
            _moduleName = _completeModuleArray[1];
            _moduleCode = _completeModuleArray[2];
        }


        public string GetModuleName(string[] moduleData)
        {
            return moduleData[1];
        }

        public int GetAmountOfAssessments(string[] moduleData)
        {
            return Convert.ToInt32(moduleData[5]);
        }



        public int[] GetAssessmentGrades(string[] moduleData)
        {
            int[] assessmentGrades = new int[8];
            assessmentGrades[0] = Convert.ToInt32(moduleData[6]);
            assessmentGrades[1] = Convert.ToInt32(moduleData[7]);
            assessmentGrades[2] = Convert.ToInt32(moduleData[8]);
            assessmentGrades[3] = Convert.ToInt32(moduleData[9]);
            assessmentGrades[4] = Convert.ToInt32(moduleData[10]);
            assessmentGrades[5] = Convert.ToInt32(moduleData[11]);
            assessmentGrades[6] = Convert.ToInt32(moduleData[12]);
            assessmentGrades[7] = Convert.ToInt32(moduleData[13]);

            return assessmentGrades;
        }

        public void ResolveAllResults()
        {

            if (_assessmentAmount == 1)
            {
                int[] temp = { _moduleGrades[0], _moduleGrades[1] };
                _avgOne = calculateGrade(temp);
            }

            if (_assessmentAmount == 2)
            {
                int[] temp = { _moduleGrades[0], _moduleGrades[1] };
                int[] temp2 = { _moduleGrades[2], _moduleGrades[3] };
                _avgOne = calculateGrade(temp);
                _avgTwo = calculateGrade(temp2);
            }

            if (_assessmentAmount == 3)
            {
                int[] temp = { _moduleGrades[0], _moduleGrades[1] };
                int[] temp2 = { _moduleGrades[2], _moduleGrades[3] };
                int[] temp3 = { _moduleGrades[4], _moduleGrades[5] };
                _avgOne = calculateGrade(temp);
                _avgTwo = calculateGrade(temp2);
                _avgThree = calculateGrade(temp3);
            }

            if (_assessmentAmount == 4)
            {
                int[] temp = { _moduleGrades[0], _moduleGrades[1] };
                int[] temp2 = { _moduleGrades[2], _moduleGrades[3] };
                int[] temp3 = { _moduleGrades[4], _moduleGrades[5] };
                int[] temp4 = { _moduleGrades[6], _moduleGrades[7] };
                _avgOne = calculateGrade(temp);
                _avgTwo = calculateGrade(temp2);
                _avgThree = calculateGrade(temp3);
                _avgFour = calculateGrade(temp4);
            }
        }

        public int getModuleTotal()
        {
            if (_assessmentAmount == 1)
            {
                int total = (int)Math.Round(_avgOne);
                return total;
            }
            if (_assessmentAmount == 2)
            {
                int total = (int)(Math.Round(_avgOne + _avgTwo));
                return total;
            }
            if (_assessmentAmount == 3)
            {
                int total = (int)(Math.Round(_avgOne + _avgTwo + _avgThree));
                return total;
            }
            if (_assessmentAmount == 4)
            {
                int total = (int)(Math.Round(_avgOne + _avgTwo + _avgThree + _avgFour));
                return total;
            }
            return 0;
        }

        public float calculateGrade(int[] grade)
        {
            float actualGrade = grade[0];
            float weight = grade[1];

            float averageGrade = ((actualGrade / 100) * weight);
            return averageGrade;
        }

        public int[] GetAssessmentDetails(int val, int[] assessValues)
        {

            if (val == 1)
            {
                int[] assess = new int[2];
                assess[0] = assessValues[0];
                assess[1] = assessValues[1];
                return assess;
            }

            if (val == 2)
            {
                int[] assess = new int[4];
                assess[0] = assessValues[0];
                assess[1] = assessValues[1];
                assess[2] = assessValues[2];
                assess[3] = assessValues[3];
                return assess;
            }

            if (val == 3)
            {
                int[] assess = new int[6];
                assess[0] = assessValues[0];
                assess[1] = assessValues[1];
                assess[2] = assessValues[2];
                assess[3] = assessValues[3];
                assess[4] = assessValues[4];
                assess[5] = assessValues[5];
                return assess;
            }

            if (val == 4)
            {
                int[] assess = new int[8];
                assess[0] = assessValues[0];
                assess[1] = assessValues[1];
                assess[2] = assessValues[2];
                assess[3] = assessValues[3];
                assess[4] = assessValues[4];
                assess[5] = assessValues[5];
                assess[6] = assessValues[6];
                assess[7] = assessValues[7];
                return assess;
            }
            return null;
        }

        public int CalculateLevelSix(List<int> list)
        {

            List<int> levelValues = list;
            List<int> newList = new List<int>();
            int i = 0;

            int min = 0;
            if (levelValues.Count > 7)
            {
                foreach (var values in levelValues)
                {
                    min = levelValues.Min();
                    minimum = min;
                }
            }

            foreach (var values in levelValues)
            {
                if (values != min)
                {
                    newList.Add(values);
                    i++;
                }
            }
            //MessageBox.Show(i + " sixessss");
            int A = getAverageMark(newList);

            //MessageBox.Show(A + " is the grade of the sixth year");
            return A;
        }

        public int CalculateLevelFive(List<int> list)
        {

            List<int> levelValues = list;
            List<int> newList = new List<int>();
            int gradeCount = 0;
            int gradeCount2 = 0;
            int j = 0;


            foreach (var values in levelValues)
            {
                //MessageBox.Show(values + " five list");
                newList.Add(values);
                //min = newList.Min();
                //MessageBox.Show(min + " low val");

            }
            newList.Add(this.Minimum);
            //MessageBox.Show(minimum+" lowest value took from the six side");

            newList.Sort();
            //foreach (var i in newList)
            //{
            //    MessageBox.Show(i + "five");
            //}
            if (newList.Count == 8)
            {
                newList.RemoveAt(0);
            }
            if (newList.Count == 9)
            {
                newList.RemoveAt(0);
                newList.RemoveAt(0);
            }


            //MessageBox.Show(newList.Count + " five before coutnt");
            //foreach (var i in newList)
            //{
            //    //MessageBox.Show(i + " last list of fives");
            //    j++;
            //}
            //MessageBox.Show(j + " number of of fives");


            int B = getAverageMark(newList);
            //MessageBox.Show(B + " is the grade of the fifth year");
            return B;
        }

        private int getAverageMark(List<int> newList)
        {
            int tot = 0;
            foreach (var list in newList)
            {
                tot += list;
            }
            tot = tot / 7;

            return tot;
        }


        public string FinalDegreeResult(int A, int B)
        {
            if (A >= 70)
            {
                if (B >= 60)
                {
                    return "First Class Degree";
                }
                else if (B >= 50)
                {
                    return "Upper second Class Degree";
                }
                else if (B >= 40)
                {
                    return "Lower Second Class Degree";
                }
            }
            else if (A >= 60)
            {
                if (B >= 50)
                {
                    return "Upper second Class Degree";
                }
                else if (B >= 40)
                {
                    return "Lower Second Class Degree";
                }
            }
            else if (A >= 50)
            {
                if (B >= 40)
                {
                    return "Lower Second Class Degree";
                }
            }
            else
            {
                return "Fail";
            }

            return null;
        }

        //public int FinalAvgResult(List<string> list)
        //{
        //    foreach (var arr in list)
        //    {
        //        string[] completeModuleArray = arr.Split(',');
        //    int[] moduleGrades = GetAssessmentGrades(completeModuleArray);
        //    int assessmentAmount = GetAmountOfAssessments(completeModuleArray);
        //    }
            
        //}
    }
}

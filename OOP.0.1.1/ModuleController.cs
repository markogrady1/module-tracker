using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace OOP._0._1._1
{
    class ModuleController
    {
        private delegate void RunOnThreadPool(string moduleName, string moduleCode, string moduleLevel, string moduleAssessmentAmount, string moduleCredit, string courseId);
        private StartUp startUp;
        private Database db;
        private string _courseId;

        /*set the startup course instance via constructor injection*/
        public ModuleController(StartUp startup)
        {
            startUp = startup;
            db = new Database();
        }

        public string CourseId
        {
            get { return _courseId; }
            set { _courseId = value; }
        }

        /*
         * create a new module by setting the module model data 
         * and setting the database details via a new thread
         */
        public void CreateNewModule(CourseController cc, string username, string coursename, string moduleName, string moduleCode, string moduleLevel, string moduleAssessmentAmount, string moduleCredit)
        {
            _courseId = cc.getCourseDbId();
            Module module = new Module();
            module.CourseDatabaseId = _courseId;
            module.ModuleCode = moduleCode;
            string pattern = "Level ";
            Regex rgx = new Regex(pattern);
            string newLevelStr = rgx.Replace(moduleLevel, "");
            module.ModuleLevel = newLevelStr;
            module.ModuleAssessmentAmount = moduleAssessmentAmount;
            db.OpenConnection();
            RunOnThreadPool poolDelegate = db.InsertNewModule;
            /*Thread used to write to the database when a new module is created*/
            Thread thread = new Thread(() => db.InsertNewModule(moduleName, moduleCode, moduleLevel, moduleAssessmentAmount, moduleCredit, _courseId));
            thread.Start();
            poolDelegate.Invoke(moduleName, moduleCode, moduleLevel, moduleAssessmentAmount, moduleCredit, _courseId);
            getAllModulesByLevel();
        }

        public void setCourseId(CourseController cc)
        {
            _courseId = cc.getCourseDbId();
        }

        /*reset all the module data by getting the new details from the DB*/
        public void resetAllModules()
        {
            getAllModulesByLevel();
        }

        /*get all the modules by way of the different levels and set the views*/
        public void getAllModulesByLevel()
        {
            db.OpenConnection();
            List<string> levelFourModules = db.getModulesByLevel(_courseId, "four");
            db.OpenConnection();
            List<string> levelFiveModules = db.getModulesByLevel(_courseId, "five");
            db.OpenConnection();
            List<string> levelSixModules = db.getModulesByLevel(_courseId, "six");

            if (levelFourModules.Count != 0)
            {
                startUp.setLevel("four", levelFourModules);
            }
            else
            {
                startUp.LevelFourCleanUp();
            }

            if (levelFiveModules.Count != 0)
            {
                startUp.setLevel("five", levelFiveModules);
            }
            else
            {
                startUp.LevelFiveCleanUp();
            }

            if (levelSixModules.Count != 0)
            {
                startUp.setLevel("six", levelSixModules);
            }
            else
            {
                startUp.LevelSixCleanUp();
            }
        }

        /*get all modules regardless of level*/
        public List<string> resolveAllModules()
        {

            db.OpenConnection();
            List<string> modList = db.getAllModules(_courseId);

            return modList;
        }

        /*get all modules in levels five and six*/
        public List<string> resolveAllDegreeModules(string level)
        {
            db.OpenConnection();
            List<string> modList = db.getAllFinalModules(_courseId, level);

            return modList;

        }

        /*get the final outcome for the level six modules*/
        public int Level6Outcome(Prediction prediction, List<int> list)
        {
            Prediction predict = prediction;
            int A = predict.CalculateLevelSix(list);

            return A;
        }

        /*get the final outcome for the level five modules*/
        public int Level5Outcome(Prediction prediction, List<int> list)
        {
            Prediction predict = prediction;
            int B = predict.CalculateLevelFive(list);

            return B;
        }
        /*get the final outcome for the level five and six modules*/
        public string GetFinalDegree(int a, int b)
        {
            Prediction pred = new Prediction();
            string result = pred.FinalDegreeResult(a, b);

            return result;
        }

        /*get the average values*/
        public int getAvg(List<int> fours)
        {
            int tot = 0;
            foreach (var list in fours)
            {
                tot += list;
            }

            if (fours.Count != 0)
            {
                tot = tot / fours.Count;
                return tot;
            }

            return 0;
        }

        /*remove a module from the course*/
        public void removeModule(string moduleId)
        {
            string moduleID = moduleId;
            db.OpenConnection();
            db.DeleteModule( moduleID);

        }
    }
}

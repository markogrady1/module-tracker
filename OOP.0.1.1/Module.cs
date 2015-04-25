namespace OOP._0._1._1
{
    class Module
    {
        private string _moduleName,_moduleCode, _moduleLevel, _moduleAssessmentAmount, _courseDatabaseId;
      
        public string ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }

        public string ModuleCode
        {
            get { return _moduleCode; }
            set { _moduleCode = value; }
        }

        public string ModuleLevel
        {
            get { return _moduleLevel; }
            set { _moduleLevel = value; }
        }

        public string CourseDatabaseId
        {
            get { return _courseDatabaseId; }
            set { _courseDatabaseId = value; }
        }

        public string ModuleAssessmentAmount
        {
            get { return _moduleAssessmentAmount; }
            set { _moduleAssessmentAmount = value; }
        }
    }
}

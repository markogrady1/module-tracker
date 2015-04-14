using System.Threading;

namespace OOP._0._1._1
{
    class CourseController
    {
        private delegate void RunOnThreadPool(string username, string coursename);
        private Database db;
        private Course course;
        private User user;
        public void setDependencies(Course course, User user)
        {
            this.course = course;
            this.user = user;
        }
        public bool setData(string username, string courseName)
        {
            course.setCourseName(courseName);
            user.setName(username);
           var res = addToDatabase(username, courseName);
            if (res)
                return true;
            else
                return false;
        }
        
        public bool addToDatabase(string username, string coursename)
        {
            db = new Database();
            var stat = db.OpenConnection();
            if (stat)
            {
                int threadId = 0;
                RunOnThreadPool poolDelegate = InsertData;
                var t = new Thread(() => InsertData(username, coursename));
                poolDelegate.Invoke(username, coursename);


                db.CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void InsertData(string username, string coursename)
        {
            
            db.InsertNewCourse(username, coursename);
        }
    }
}

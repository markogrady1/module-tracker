using System.Threading;
using System.Windows.Forms;

namespace OOP._0._1._1
{
    class CourseController
    {

        public CourseController()
        {
            db = new Database();
        }
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
            course.CourseName = courseName;
            user.Name = username;
           var res = addToDatabase(username, courseName);
            if (res)
                return true;
            else
                return false;
        }
        
        public bool addToDatabase(string username, string coursename)
        {
            
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
            string id = db.GetId(username, coursename, "course");
            course.CourseDatabaseId = id;
            
        }

        public void MatchCourseData(string existingData)
        {
            string queryParameters = existingData;
            string[] strArray = queryParameters.Split(',');
            
            db.OpenConnection();
            string id =  db.GetId(strArray[0], strArray[1], "course");
            course.CourseDatabaseId = id;
            
            //id has been resolved from the database but we have not implemented it yet
        }

        public string getCourseDbId()
        {
            return course.CourseDatabaseId;
        }

    }
}

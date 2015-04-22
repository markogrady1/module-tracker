using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace OOP._0._1._1
{
    class CourseController
    {
        private delegate void RunOnThreadPool(string username, string coursename);
        private Database db;
        private Course course;
        private User user;
        private string courseName, userName;
        public CourseController()
        {
            db = new Database();
        }
      
        public void setDependencies(Course course, User user)
        {
            this.course = course;
            this.user = user;
        }

       

        public bool setData(string username, string courseName)
        {
            this.courseName = courseName;
            this.userName = username;
            course.CourseName = this.courseName;
            user.Name = this.userName;
           var res = addToDatabase(username, courseName);
            if (res)
                return true;
            else
                return false;
        }

        public bool setExistingData(string existingData)
        {
            string queryParameters = existingData;
            string[] strArray = queryParameters.Split(',');
            
            user.Name = strArray[0];
            course.CourseName = strArray[1];
            return true;
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

        public void assignNewUser(string username, string coursename)
        {
            courseName = coursename;
            userName = username;
            course.CourseName = courseName;
            user.Name = userName;
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

        public List<string> getCourseData(string courseDat)
        {
            db.OpenConnection();
            List<string>data = db.getCourseDetails(courseDat);
            return data;
        }

        public string UserName
        {
            get { return userName; }
        }

        public string CourseName
        {
            get { return courseName; }
        }
    }
}

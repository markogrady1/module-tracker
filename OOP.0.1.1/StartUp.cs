using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace OOP._0._1._1
{
    public partial class StartUp : Form
    {
        private delegate void RunOnThreadPool(int threadId);
        private TabControl tabControlViews;

        private TabPage tabPageView1, tabPageView2, tabPageView3, tabPageView4, tabPageView5;
        private Thread t;
        private Database db;
        private Course course;
        private User user;
        public StartUp()
        {
            InitializeComponent();
            ConfigureTabs();

            user = new User();
            course = new Course();
            db = new Database();
            var stat = db.OpenConnection();
            if (stat)
            {
                
                var list = db.Select();
                db.CloseConnection();
                db.OpenConnection();
               var courseList = db.GetAvailableCourses();
               foreach (string line in courseList)
               {
                string  s =   line.Substring(0, 1).ToUpper() + line.Substring(1).ToLower();
                
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                   availableCoursesCbo.Items.Add(s);
               }
                db.CloseConnection();
            }
            else
            {
                //lblConn.Text = "not connected";
                db.CloseConnection();
            }

        }

        public void ConfigureTabs()
        {
            this.tabPageView1 = new TabPage();
            this.tabPageView2 = new TabPage();
            this.tabPageView3 = new TabPage();
            this.tabPageView4 = new TabPage();
            this.tabPageView5 = new TabPage();
            this.mainTabControl.Controls.Add(this.tabPageView1);
            this.mainTabControl.Controls.Add(this.tabPageView2);
            this.mainTabControl.Controls.Add(this.tabPageView3);
            this.mainTabControl.Controls.Add(this.tabPageView4);
            this.mainTabControl.Controls.Add(this.tabPageView5);
            this.tabPageView1.Text = "Create Course";
            this.tabPageView2.Text = "Add Module";
            this.tabPageView3.Text = "Level Three";
            this.tabPageView4.Text = "Level Four";
            this.tabPageView5.Text = "Level Five";
        }

        private void buildCourseBtn_Click(object sender, System.EventArgs e)
        {
            selectCoursePnl.Visible =true;
        }

        private void SubmitCourseNameBtn_Click(object sender, EventArgs e)
        {
            string chosenCourse = availableCoursesCbo.Text;
            string username = userNameTxt.Text;
            if (chosenCourse == "")
            {
                MessageBox.Show("Please select a course");
            }
            else
            {
                if (username == "")
                {
                    MessageBox.Show("Please enter your name");
                }
                else
                {
                   CourseController cc = new CourseController();
                    cc.setDependencies(course, user);
                   bool result = cc.setData(username, chosenCourse);
                    if (result)
                    {
                        this.tabPageView2.BringToFront();
                        mainTabControl.SelectedTab = tabPage2;
                    }
                }
            }
        }

    

    }
}

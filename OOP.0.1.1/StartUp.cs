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

        private TabPage tabPageLvl3, tabPageLvl4, tabPageLvl5, tabPageView4, tabPageView5;
        private Thread t;
        private Database db;
        private Course course;
        private User user;
        private Panel mainCoverLvl4Pnl;
        private Panel mainCoverLvl5Pnl;
        private Panel mainCoverLvl6Pnl;
        private Label moduleStatusLbl;
        CourseController cc = new CourseController();

        public StartUp()
        {
            InitializeComponent();
            
            db = new Database();
            var stat = db.OpenConnection();
            if (stat)
            {
                
                var existingList = db.SelectExistingCourses();
                if (existingList.Count == 0 )
                {
                    existingCourseCbo.Items.Add("There are no existing courses");
                    openExistingPredictionBtn.Enabled = false;
                }
                else
                {
                    foreach (string lst in existingList)
                {
                    string s = lst.Substring(0, 1).ToUpper() + lst.Substring(1).ToLower();

                    s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                    existingCourseCbo.Items.Add(s);
                }

                }
                




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
                db.CloseConnection();
            }

        }

        public void ConfigureTabs()
        {
            mainCoverLvl4Pnl = new Panel();
            mainCoverLvl5Pnl = new Panel();
            mainCoverLvl6Pnl = new Panel();
            mainCoverLvl4Pnl.SuspendLayout();
            moduleStatusLbl = new Label();



            this.moduleStatusLbl.AutoSize = true;
            this.moduleStatusLbl.Font = new System.Drawing.Font("Courgette", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moduleStatusLbl.Location = new System.Drawing.Point(222, 15);
            this.moduleStatusLbl.Name = "moduleStatus";
            this.moduleStatusLbl.Size = new System.Drawing.Size(185, 24);
    
            this.moduleStatusLbl.Text = "There are no modules set for this level";











            this.tabPageLvl3 = new TabPage();
            this.tabPageLvl4 = new TabPage();
            this.tabPageLvl5 = new TabPage();
            this.tabPageView4 = new TabPage();
            this.mainTabControl.Controls.Add(this.tabPageLvl3);
            this.mainTabControl.Controls.Add(this.tabPageLvl4);
            this.mainTabControl.Controls.Add(this.tabPageLvl5);
            this.mainTabControl.Controls.Add(this.tabPageView4);
            this.tabPage2.Text = "Add Module";
            this.tabPageLvl3.Text = "Level Three";
            tabPageLvl3.Controls.Add(mainCoverLvl4Pnl);
            mainCoverLvl4Pnl.Controls.Add(moduleStatusLbl);
            mainCoverLvl4Pnl.Location = new System.Drawing.Point(3, 0);
            mainCoverLvl4Pnl.Name = "mainCoverPnl";
            mainCoverLvl4Pnl.Size = new System.Drawing.Size(690, 216);
            mainCoverLvl4Pnl.Visible = true;

            tabPageLvl4.Text = "Level Four";
            tabPageLvl5.Text = "Level Five";
        }

        private void buildCourseBtn_Click(object sender, System.EventArgs e)
        {
            selectCoursePnl.Visible =true;
            openExistingCoursePnl.Visible = true;
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
                    username = username.Substring(0, 1).ToUpper() + username.Substring(1).ToLower();
                    cc.setDependencies(new Course(), new User());
                   bool result = cc.setData(username, chosenCourse);
                    if (result)
                    {
                        ConfigureTabs();
                        mainCoverPnl.Visible = true;
                        mainCoverPnl.BringToFront();
                        mainTabControl.SelectedTab = tabPage2;
                        addModUserLbl.Text = username;
            
                        addModCourseLbl.Text = chosenCourse;
            
                        covertab2Pnl.Visible = true;
                        covertab2Pnl.BringToFront();
                    }
                }
            }
        }

        private void openExistingPredictionBtn_Click(object sender, EventArgs e)
        {

            string existingData =  existingCourseCbo.Text;
            if (existingData == "")
            {
                MessageBox.Show("You did not select an existing course");
            }
            else
            {
                cc.MatchCourseData(existingData);
            }
            
        }

        private void activeCreateNewCourseBtn_Click(object sender, EventArgs e)
        {
            mainCoverPnl.Visible = false;
            mainCoverPnl.SendToBack();
            userNameTxt.Text = "";
            availableCoursesCbo.Text = "";
        }

        private void AddModuleBtnBlue_Click(object sender, EventArgs e)
        {
            MessageBox.Show("whats up fool");
        }



    }
}

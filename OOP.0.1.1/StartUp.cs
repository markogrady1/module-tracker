using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace OOP._0._1._1
{
    public partial class StartUp : Form
    {
        private delegate void RunOnThreadPool(int threadId);
        private TabControl tabControlViews;

        private TabPage tabPageLvl4, tabPageLvl5, tabPageLvl6, tabPageView4, tabPageView5;
        private Thread t;
        private Database db;
        private Course course;
        private User user;
        private Panel mainCoverLvl4Pnl;
        private Panel mainCoverLvl5Pnl;
        private Panel mainCoverLvl6Pnl;
        private Label levFourStatusLbl, levFiveStatusLbl, levSixStatusLbl;
        private CourseController cc = new CourseController();
        private AssessmentController assessController;
        private ModuleController moduleController;
        
        public StartUp()
        {
            InitializeComponent();
            this.moduleController = new ModuleController(this);
            db = new Database();
            var stat = db.OpenConnection();
            if (stat)
            {

                var existingList = db.SelectExistingCourses();
                if (existingList.Count == 0)
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
                    string s = line.Substring(0, 1).ToUpper() + line.Substring(1).ToLower();

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
            mainCoverLvl5Pnl.SuspendLayout();
            mainCoverLvl6Pnl.SuspendLayout();
            levFourStatusLbl = new Label();
            levFiveStatusLbl = new Label();
            levSixStatusLbl = new Label();

            setStatusLabel(levFourStatusLbl);
            setStatusLabel(levFiveStatusLbl);
            setStatusLabel(levSixStatusLbl);

            tabPageLvl4 = new TabPage();
            tabPageLvl5 = new TabPage();
            tabPageLvl6 = new TabPage();
            tabPageView4 = new TabPage();
            mainTabControl.Controls.Add(this.tabPageLvl4);
            mainTabControl.Controls.Add(this.tabPageLvl5);
            mainTabControl.Controls.Add(this.tabPageLvl6);
            mainTabControl.Controls.Add(this.tabPageView4);
            tabPage2.Text = "Add Module";

            this.tabPageLvl4.Text = "Level Four";
            AddCover(tabPageLvl4, mainCoverLvl4Pnl, levFourStatusLbl);
            tabPageLvl5.Text = "Level Five";
            AddCover(tabPageLvl5, mainCoverLvl5Pnl, levFiveStatusLbl);
            tabPageLvl6.Text = "Level Six";
            AddCover(tabPageLvl6, mainCoverLvl6Pnl, levSixStatusLbl);
        }

        private void AddCover(TabPage tb, Panel pnl, Label lbl)
        {
            tb.Controls.Add(pnl);
            pnl.Controls.Add(lbl);
            pnl.Location = new System.Drawing.Point(3, 0);
            pnl.Name = "mainCoverPnl";
            pnl.Size = new System.Drawing.Size(690, 216);
            pnl.Visible = true;
        }

        public void hidePanel(Panel pnl)
        {
            pnl.Visible = false;
        }

        private void setStatusLabel(Label lbl)
        {
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Courgette", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl.Location = new System.Drawing.Point(222, 15);
            lbl.Name = "moduleStatus";
            lbl.Size = new System.Drawing.Size(185, 24);
            lbl.Text = "There are no modules set for this level";
        }

        private void buildCourseBtn_Click(object sender, System.EventArgs e)
        {
            selectCoursePnl.Visible = true;
            openExistingCoursePnl.Visible = true;
        }

        private void SubmitCourseNameBtn_Click(object sender, EventArgs e)
        {
            resetTabs();
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

        public void resetTabs()
        {

            this.mainTabControl.Controls.Remove(this.tabPageLvl4);
            this.mainTabControl.Controls.Remove(this.tabPageLvl5);
            this.mainTabControl.Controls.Remove(this.tabPageLvl6);
            this.mainTabControl.Controls.Remove(this.tabPageView4);
        }

        private void openExistingPredictionBtn_Click(object sender, EventArgs e)
        {

            string existingData = existingCourseCbo.Text;
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
            resetTextFields(userNameTxt);
            resetComboBox(availableCoursesCbo);
        }

        private void AddModuleBtnBlue_Click(object sender, EventArgs e)
        {
            string moduleName = moduleNameTxt.Text;
            string moduleCode = moduleCodeTxt.Text;
            string moduleLevel = moduleLevelCbo.Text;
            string moduleAssessmentAmount = moduleAssessmentAmountCbo.Text;
            if (moduleName == "")
            {
                MessageBox.Show("You must provide a module name");
            }
            else
            {
                if (moduleCode == "")
                {
                    MessageBox.Show("You must provide a module Code");
                }
                else
                {
                    if (moduleLevel == "")
                    {
                        MessageBox.Show("You must provide a module Level");
                    }
                    else
                    {
                        if (moduleAssessmentAmount == "")
                        {
                            MessageBox.Show("You must provide the number of assessments for this module");
                        }
                        else
                        {
                            moduleAssessmentAmount = moduleAssessmentAmount == ""
                          ? "0"
                          : moduleAssessmentAmount;

                            moduleController.CreateNewModule(cc,
                                addModUserLbl.Text,
                                addModCourseLbl.Text,
                                moduleName,
                                moduleCode,
                                moduleLevel,
                                moduleAssessmentAmount

                                );
                            covertab3Pnl.Visible = false;
                            addAddGradeTabPage.Text = "Add Module Grade";
                            resetComboBox(moduleLevelCbo);
                            resetComboBox(moduleAssessmentAmountCbo);
                            resetTextFields(moduleNameTxt);
                            resetTextFields(moduleCodeTxt);
                        }

                    }

                }
            }
        }

        public void resetTextFields(TextBox txt)
        {
            txt.Text = "";
        }

        public void resetComboBox(ComboBox cbo)
        {
            cbo.SelectedIndex = -1;
        }

        public void setLevel(string level, List<string> moduleList)
        {

            if (level == "four")
            {
                hidePanel(mainCoverLvl4Pnl);
                configureLevelTab("four", moduleList);
            }
            populateAvailableModuleCombo();
        }

        public void configureLevelTab(string level, List<string> moduleList)
        {

            int x = 50;

            int modNameLabelStart = 10;
            int modNameStart = 10;
            int modeCodeLabelStart = 10;
            int modeCodeStart = 10;
            int assessmentNoLabelStart = 10;
            int assessmentNoStart = 10;
            int assessment1LblStart = 40;
            int assessment1Start = 40;
           

            if (level == "four")
            {
                Label[] modNameLbl = new Label[moduleList.Count];
                Label[] modName = new Label[moduleList.Count];
                Label[] modCodeLbl = new Label[moduleList.Count];
                Label[] modCode = new Label[moduleList.Count];
                Label[] assessmentNoLbl = new Label[moduleList.Count];
                Label[] assessmentNo = new Label[moduleList.Count];
                Label[] assessment1Lbl = new Label[moduleList.Count];
                Label[] assessment1 = new Label[moduleList.Count];
                Label[] assessment3 = new Label[moduleList.Count];
                Label[] assessment4 = new Label[moduleList.Count];

                for (var i = 0; i < moduleList.Count; i++)
                {

                    string[] strArray = moduleList[i].Split(',');
                    MessageBox.Show("Module " + moduleList[i] + " contains " + strArray[0] + "," + strArray[1] + "," + strArray[2] + "," + strArray[3] + "," + strArray[4] + "," + strArray[5]);
                    modNameLbl[i] = new Label();
                    modName[i] = new Label();
                    modCodeLbl[i] = new Label();
                    modCode[i] = new Label();
                    assessmentNoLbl[i] = new Label();
                    assessmentNo[i] = new Label();
                    assessment1Lbl[i] = new Label();
                    assessment1[i] = new Label();
                    assessment3[i] = new Label();
                    assessment4[i] = new Label();

                    modNameLbl[i].Text = "Module Name:";
                    modName[i].Text = strArray[1];
                    modCodeLbl[i].Text = "Module Code:";
                    modCode[i].Text = strArray[2];
                    assessmentNoLbl[i].Text = "No. of Assessments:";
                    assessmentNo[i].Text = ValidateAssessmentValue(strArray[5]);
                    assessment1Lbl[i].Text = "Assessments One:";
                    assessment1[i].Text = "Assessments One:";

                    modNameLbl[i].Location = new System.Drawing.Point(40, modNameLabelStart += 30);
                    tabPageLvl4.Controls.Add(modNameLbl[i]);

                    modName[i].Location = new System.Drawing.Point(150, modNameStart += 30);
                    modName[i].Size = new System.Drawing.Size(184, 18);
                    tabPageLvl4.Controls.Add(modName[i]);

                    modCodeLbl[i].Location = new System.Drawing.Point(360, modeCodeLabelStart += 30);
                    tabPageLvl4.Controls.Add(modCodeLbl[i]);

                    modCode[i].Location = new System.Drawing.Point(460, modeCodeStart += 30);
                    modCode[i].Size = new System.Drawing.Size(184, 18);
                    tabPageLvl4.Controls.Add(modCode[i]);

                    assessmentNoLbl[i].Location = new System.Drawing.Point(660, assessmentNoLabelStart += 30);
                    assessmentNoLbl[i].Size = new System.Drawing.Size(134, 18);
                    tabPageLvl4.Controls.Add(assessmentNoLbl[i]);

                    assessmentNo[i].Location = new System.Drawing.Point(820, assessmentNoStart += 30);
                    assessmentNo[i].Size = new System.Drawing.Size(150, 18);
                    tabPageLvl4.Controls.Add(assessmentNo[i]);

                    assessment1Lbl[i].Location = new System.Drawing.Point(20, assessment1LblStart += 30);
                    assessment1Lbl[i].Size = new System.Drawing.Size(150, 18);
                    tabPageLvl4.Controls.Add(assessment1Lbl[i]);

                }
            }
        }

        private string ValidateAssessmentValue(string assessmentAmount)
        {
            if (assessmentAmount == "0")
            {
                return "Assessment details do not exist for this module";
            }
            return assessmentAmount;
        }

        private void addGradesBtn_Click(object sender, EventArgs e)
        {
            if (availableModulesCbo.Text == "")
            {
                MessageBox.Show("You must select a module");
            }
            else
            {
                string moduleSelection = availableModulesCbo.Text;
                int index = availableModulesCbo.SelectedIndex;
                string moduleDBId = hiddenCombo.Items[index].ToString();
                string[] modData = moduleSelection.Split('-');
                
                string assessment1 = assessment1GradeTxt.Text;
                assessment1 = assessment1 == "" ? "0" : assessment1;
                string assessment1Weight = assessment1WeightTxt.Text;
                assessment1Weight = assessment1Weight == "" ? "0" : assessment1Weight;

                string assessment2 = assessment2GradeTxt.Text;
                assessment2 = assessment2 == "" ? "0" : assessment2;
                string assessment2Weight = assessment2WeightTxt.Text;
                assessment2Weight = assessment2Weight == "" ? "0" : assessment2Weight;

                string assessment3 = assessment3GradeTxt.Text;
                assessment3 = assessment3 == "" ? "0" : assessment3;
                string assessment3Weight = assessment3WeightTxt.Text;
                assessment3Weight = assessment3Weight == "" ? "0" : assessment3Weight;

                string assessment4 = assessment4GradeTxt.Text;
                assessment4 = assessment4 == "" ? "0" : assessment4;
                string assessment4Weight = assessment4WeightTxt.Text;
                assessment4Weight = assessment4Weight == "" ? "0" : assessment4Weight;
                assessController = new AssessmentController(moduleController);
                assessController.setAssessmentGrades(
                    assessment1,
                    assessment1Weight,
                    assessment2,
                    assessment2Weight,
                    assessment3,
                    assessment3Weight,
                    assessment4,
                    assessment4Weight
                    );

               

                assessController.setSpecificModule(modData, moduleDBId);
                assessController.updateSystem();
            }

        }

        private void populateAvailableModuleCombo()
        {
            List<string> modList = moduleController.resolveAllModules();
            availableModulesCbo.Items.Clear();
            hiddenCombo.Items.Clear();
            for (var i = 0; i < modList.Count; i++)
            {
                string[] modArr = modList[i].Split(',');
                string modname = Regex.Replace(modArr[1], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                string modCode = modArr[2].ToUpper();
                string modLevel = modArr[4].Substring(0, 1).ToUpper() + modArr[4].Substring(1).ToLower();

                //string modname = Regex.Replace(modArr[1], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                string moduleDetail = modname + "- " + modCode + " - Level: " + modLevel;
               
                availableModulesCbo.Items.Add(moduleDetail);
                hiddenCombo.Items.Add(modArr[0]);
            }

        }
    }
}

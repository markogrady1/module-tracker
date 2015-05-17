using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient.Properties;

namespace OOP._0._1._1
{
    /*
     *  This program was created by Mark O Grady 2015
     *  Object Oriented Programming CW2
     */
    public partial class StartUp : Form
    {

        private delegate void RunOnThreadPool(int threadId);
        private TabControl tabControlViews;
        private TextBox levelFiveOutputTxt, levelSixOutputTxt, levelSixAvgTxt, levelFiveAvgTxt, levelFourAvgTxt;
        private Panel outcomePnl;
        private TabPage tabPageLvl4, tabPageLvl5, tabPageLvl6, tabPageModulePrediction, tabPageView5;
        private Thread t;
        private Database db;
        private Label newValBtn;
        private User user;
        private Panel mainCoverLvl4Pnl, mainCoverLvl5Pnl, mainCoverLvl6Pnl, modulePredictPnl;
        private Label levFourStatusLbl, levFiveStatusLbl, levSixStatusLbl, modulePredictStatusLbl;
        private CourseController cc = new CourseController();
        private AssessmentController assessController;
        private ModuleController moduleController;
        private bool gradeAssessment = true;
        private Label hiddenLbl = new Label();
        private Label modTotalLbl = new Label();
        /*this is the constructor for this class and sets up all the configuration for the opening sequence*/
        public StartUp()
        {
            InitializeComponent();
            newValBtn = new Label();
            levelFourAvgTxt = new TextBox();
            levelFiveAvgTxt = new TextBox();
            levelSixAvgTxt = new TextBox();
            levelFiveOutputTxt = new TextBox();
            levelSixOutputTxt = new TextBox();
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
        /*dynamically create the tabs for creating a module and adding grades*/
        public void ConfigureTabs()
        {
            modTotalLbl.Location = new Point(680, 150);
            modTotalLbl.AutoSize = true;
            modTotalLbl.Font = new Font("Courgette", 41.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
  
            assessment4WeightTxt.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assessment4WeightTxt.Location = new Point(546, 275);
            assessment4WeightTxt.Size = new Size(100, 24);
            assessment4WeightTxt.TabIndex = 17;
          
            assGrade4WeightLbl.AutoSize = true;
            assGrade4WeightLbl.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assGrade4WeightLbl.Location = new Point(479, 275);
            assGrade4WeightLbl.Size = new Size(58, 18);
            assGrade4WeightLbl.TabIndex = 16;
            assGrade4WeightLbl.Text = "Weight:";
         
            assessment4GradeTxt.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assessment4GradeTxt.Location = new Point(376, 275);
            assessment4GradeTxt.Size = new Size(85, 24);
            assessment4GradeTxt.TabIndex = 15;
            
            assGrade4Lbl.AutoSize = true;
            assGrade4Lbl.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assGrade4Lbl.Location = new Point(222, 278);
            assGrade4Lbl.Size = new Size(151, 18);
            assGrade4Lbl.TabIndex = 14;
            assGrade4Lbl.Text = "Assessment 4 Grade:";
           
            assessment3WeightTxt.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assessment3WeightTxt.Location = new Point(546, 231);
            assessment3WeightTxt.Size = new Size(100, 24);
            assessment3WeightTxt.TabIndex = 13;
           
            assGrade3WeightLbl.AutoSize = true;
            assGrade3WeightLbl.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assGrade3WeightLbl.Location = new Point(479, 231);
            assGrade3WeightLbl.Size = new Size(58, 18);
            assGrade3WeightLbl.TabIndex = 12;
            assGrade3WeightLbl.Text = "Weight:";
           
            assessment3GradeTxt.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assessment3GradeTxt.Location = new Point(376, 231);
            assessment3GradeTxt.Size = new Size(85, 24);
            assessment3GradeTxt.TabIndex = 11;
        
            assGrade3Lbl.AutoSize = true;
            assGrade3Lbl.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assGrade3Lbl.Location = new Point(222, 234);
            assGrade3Lbl.Size = new Size(151, 18);
            assGrade3Lbl.TabIndex = 10;
            assGrade3Lbl.Text = "Assessment 3 Grade:";
          
            assessment2WeightTxt.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assessment2WeightTxt.Location = new Point(546, 187);
            assessment2WeightTxt.Size = new Size(100, 24);
            assessment2WeightTxt.TabIndex = 9;
            
            assGrade2WeightLbl.AutoSize = true;
            assGrade2WeightLbl.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assGrade2WeightLbl.Location = new Point(479, 187);
            assGrade2WeightLbl.Size = new Size(58, 18);
            assGrade2WeightLbl.TabIndex = 8;
            assGrade2WeightLbl.Text = "Weight:";
          
            assessment2GradeTxt.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assessment2GradeTxt.Location = new Point(376, 187);
            assessment2GradeTxt.Size = new Size(85, 24);
            assessment2GradeTxt.TabIndex = 7;
           
            assGrade2Lbl.AutoSize = true;
            assGrade2Lbl.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assGrade2Lbl.Location = new Point(222, 190);
            assGrade2Lbl.Size = new Size(151, 18);
            assGrade2Lbl.TabIndex = 6;
            assGrade2Lbl.Text = "Assessment 2 Grade:";
          
            assessment1WeightTxt.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assessment1WeightTxt.Location = new Point(546, 143);
            assessment1WeightTxt.Size = new Size(100, 24);
            assessment1WeightTxt.TabIndex = 5;
           
            assGrade1WeightLbl.AutoSize = true;
            assGrade1WeightLbl.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assGrade1WeightLbl.Location = new Point(479, 146);
            assGrade1WeightLbl.Size = new Size(58, 18);
            assGrade1WeightLbl.TabIndex = 4;
            assGrade1WeightLbl.Text = "Weight:";
        
            assessment1GradeTxt.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assessment1GradeTxt.Location = new Point(376, 143);
            assessment1GradeTxt.Size = new Size(85, 24);
            assessment1GradeTxt.TabIndex = 3;
          
            assGrade1Lbl.AutoSize = true;
            assGrade1Lbl.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assGrade1Lbl.Location = new Point(222, 146);
            assGrade1Lbl.Size = new Size(151, 18);
            assGrade1Lbl.TabIndex = 2;
            assGrade1Lbl.Text = "Assessment 1 Grade:";
          
            availableModulesCbo.Location = new Point(0, 0);
            availableModulesCbo.Size = new Size(121, 21);
            availableModulesCbo.TabIndex = 24;
            percent4Lbl.AutoSize = true;
            percent4Lbl.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            percent4Lbl.Location = new Point(649, 278);
            percent4Lbl.Size = new Size(21, 18);
            percent4Lbl.TabIndex = 21;
            percent4Lbl.Text = "%";
         
            percent3Lbl.AutoSize = true;
            percent3Lbl.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            percent3Lbl.Location = new Point(649, 234);
            percent3Lbl.Size = new Size(21, 18);
            percent3Lbl.TabIndex = 20;
            percent3Lbl.Text = "%";
         
            percent2Lbl.AutoSize = true;
            percent2Lbl.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            percent2Lbl.Location = new Point(649, 190);
            percent2Lbl.Size = new Size(21, 18);
            percent2Lbl.TabIndex = 19;
            percent2Lbl.Text = "%";
            
            percent1Lbl.AutoSize = true;
            percent1Lbl.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            percent1Lbl.Location = new Point(649, 148);
            percent1Lbl.Size = new Size(21, 18);
            percent1Lbl.TabIndex = 18;
            percent1Lbl.Text = "%";

            mainTabControl.Controls.Remove(tabPage2);
            mainTabControl.Controls.Remove(addGradeTabPage);
            mainTabControl.Controls.Add(tabPage2);
            mainTabControl.Controls.Add(addGradeTabPage);
            mainCoverLvl4Pnl = new Panel();
            mainCoverLvl5Pnl = new Panel();
            mainCoverLvl6Pnl = new Panel();
            modulePredictPnl = new Panel();

            mainCoverLvl4Pnl.SuspendLayout();
            mainCoverLvl5Pnl.SuspendLayout();
            mainCoverLvl6Pnl.SuspendLayout();
            modulePredictPnl.SuspendLayout();

            levFourStatusLbl = new Label();
            levFiveStatusLbl = new Label();
            levSixStatusLbl = new Label();
            modulePredictStatusLbl = new Label();

            setStatusLabel(levFourStatusLbl);
            setStatusLabel(levFiveStatusLbl);
            setStatusLabel(levSixStatusLbl);
            setPredictionStatusLabel(modulePredictStatusLbl);

            tabPageLvl4 = new TabPage();
            tabPageLvl5 = new TabPage();
            tabPageLvl6 = new TabPage();
            tabPageModulePrediction = new TabPage();
            mainTabControl.Controls.Add(this.tabPageLvl4);
            mainTabControl.Controls.Add(this.tabPageLvl5);
            mainTabControl.Controls.Add(this.tabPageLvl6);
            mainTabControl.Controls.Add(this.tabPageModulePrediction);
            tabPage2.Text = "Add Module";

            tabPageLvl4.Text = "Level Four";
            AddCover(tabPageLvl4, mainCoverLvl4Pnl, levFourStatusLbl);
            tabPageLvl5.Text = "Level Five";
            AddCover(tabPageLvl5, mainCoverLvl5Pnl, levFiveStatusLbl);
            tabPageLvl6.Text = "Level Six";
            AddCover(tabPageLvl6, mainCoverLvl6Pnl, levSixStatusLbl);
            tabPageModulePrediction.Text = "Module Prediction";
            tabPageModulePrediction.BackColor = Color.FromArgb(177, 192, 243);
            AddModulePredictionCover(tabPageModulePrediction, modulePredictPnl, modulePredictStatusLbl);


            availableModulesCbo.DropDownStyle = ComboBoxStyle.DropDownList;
            availableModulesCbo.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            availableModulesCbo.FormattingEnabled = true;
            availableModulesCbo.Location = new Point(376, 94);
            availableModulesCbo.Size = new Size(396, 26);
            availableModulesCbo.TabIndex = 1;
            availableModulesCbo.SelectedIndexChanged += availableModulesCbo_SelectedIndexChanged;
            availableModulesCbo.Cursor = Cursors.Hand;

            moduleAssessmentAmountCbo.Cursor = Cursors.Hand;
            mainTabControl.Controls.Add(degreePredictionTabPage);
            degreePredictionTabPage.Text = "Degree Prediction";
            degreePredictionTabPage.Controls.Add(degreePredictionCoverPnl);
            degreePredictionCoverPnl.Controls.Add(statLbl);
            degreePredictionCoverPnl.Location = new Point(7, 9);
            degreePredictionCoverPnl.Size = new Size(1060, 611);
            degreePredictionCoverPnl.TabIndex = 0;
            degreePredictionCoverPnl.ResumeLayout(false);
            degreePredictionCoverPnl.PerformLayout();

            statLbl.AutoSize = true;
            statLbl.Font = new Font("Courgette", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            statLbl.ForeColor = Color.FromArgb(255, 128, 128);
            statLbl.Location = new Point(271, 99);
            statLbl.Size = new Size(476, 81);
            statLbl.TabIndex = 0;
            statLbl.Text = "No Modules Exist";

            degreePredictionTabPage.Controls.Add(degreePredictionPnl);
            degreePredictionTabPage.Location = new Point(4, 25);
 
            degreePredictionTabPage.Padding = new Padding(3);
            degreePredictionTabPage.Size = new Size(1070, 711);
            degreePredictionTabPage.TabIndex = 1;
            degreePredictionTabPage.UseVisualStyleBackColor = true;


            dgSummaryTitle.Image = Properties.Resources.degreeSummary;
            dgSummaryTitle.Location = new Point(354, 14);
            dgSummaryTitle.Size = new Size(290, 28);
            dgSummaryTitle.TabIndex = 5;

            dgPredictUsernamePnl.Controls.Add(dgCourseNameLbl);
            dgPredictUsernamePnl.Controls.Add(dgUsernameLbl);
            dgPredictUsernamePnl.Location = new Point(117, 77);
            dgPredictUsernamePnl.Size = new Size(766, 49);
            dgPredictUsernamePnl.TabIndex = 4;
            dgPredictUsernamePnl.BackColor = Color.FromArgb(177, 192, 243);

            dgCourseNameLbl.AutoSize = true;
            dgCourseNameLbl.Location = new Point(421, 17);
            dgCourseNameLbl.Size = new Size(83, 16);
            dgCourseNameLbl.TabIndex = 1;
            dgCourseNameLbl.Text = "coursename";

            dgUsernameLbl.AutoSize = true;
            dgUsernameLbl.Location = new Point(183, 17);
            dgUsernameLbl.Size = new Size(68, 16);
            dgUsernameLbl.TabIndex = 0;
            dgUsernameLbl.Text = "username";

            dgTotalOutComeLbl.AutoSize = true;
            dgTotalOutComeLbl.Font = new Font("Microsoft Sans Serif", 40.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgTotalOutComeLbl.Location = new Point(452, 504);
            dgTotalOutComeLbl.Size = new Size(175, 105);
            dgTotalOutComeLbl.TabIndex = 2;
            dgTotalOutComeLbl.Text = "res";

            mainAddGradePanel.Controls.Add(modTotalLbl);
            mainAddGradePanel.BackColor = Color.FromArgb(235, 235, 235);
            mainAddGradePanel.Controls.Add(label25);
            mainAddGradePanel.Controls.Add(addGradesBtn);
            mainAddGradePanel.Controls.Add(percent4Lbl);
            mainAddGradePanel.Controls.Add(percent3Lbl);
            mainAddGradePanel.Controls.Add(percent2Lbl);
            mainAddGradePanel.Controls.Add(percent1Lbl);
            mainAddGradePanel.Controls.Add(assessment4WeightTxt);
            mainAddGradePanel.Controls.Add(assGrade4WeightLbl);
            mainAddGradePanel.Controls.Add(assessment4GradeTxt);
            mainAddGradePanel.Controls.Add(assGrade4Lbl);
            mainAddGradePanel.Controls.Add(assessment3WeightTxt);
            mainAddGradePanel.Controls.Add(assGrade3WeightLbl);
            mainAddGradePanel.Controls.Add(assessment3GradeTxt);
            mainAddGradePanel.Controls.Add(assGrade3Lbl);
            mainAddGradePanel.Controls.Add(assessment2WeightTxt);
            mainAddGradePanel.Controls.Add(assGrade2WeightLbl);
            mainAddGradePanel.Controls.Add(assessment2GradeTxt);
            mainAddGradePanel.Controls.Add(assGrade2Lbl);
            mainAddGradePanel.Controls.Add(assessment1WeightTxt);
            mainAddGradePanel.Controls.Add(assGrade1WeightLbl);
            mainAddGradePanel.Controls.Add(assessment1GradeTxt);
            mainAddGradePanel.Controls.Add(assGrade1Lbl);
            mainAddGradePanel.Controls.Add(availableModulesCbo);
            mainAddGradePanel.Controls.Add(label12);
            mainAddGradePanel.Location = new Point(36, 12);
            mainAddGradePanel.Size = new Size(1025, 515);
            mainAddGradePanel.TabIndex = 1;
        }

        /*add covers to tabs if no module exists*/
        private void AddCover(TabPage tb, Panel pnl, Label lbl)
        {
            tb.Controls.Add(pnl);
            pnl.Controls.Add(lbl);
            pnl.Location = new Point(3, 0);
            pnl.Size = new Size(690, 216);
            pnl.Visible = true;
        }

        /*add covers to tabs if no module prediction exists*/
        private void AddModulePredictionCover(TabPage tb, Panel pnl, Label lbl)
        {
            tb.Controls.Add(pnl);
            pnl.Controls.Add(lbl);
            pnl.Location = new Point(3, 0);
            pnl.AutoSize = true;
            pnl.Visible = true;
        }

        public void hidePanel(Panel pnl)
        {
            pnl.Visible = false;
        }

        private void setStatusLabel(Label lbl)
        {
            lbl.AutoSize = true;
            lbl.Font = new Font("Courgette", 14.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl.Location = new Point(222, 15);
            lbl.Size = new Size(185, 24);
            lbl.Text = "There are no modules set for this level";
        }

        public void addGradesWeights(List<string> moduleList )
        {
            Panel[] pn = new Panel[moduleList.Count];
            Label[] s_modNameLbl = new Label[moduleList.Count];
            TextBox[] s_modName = new TextBox[moduleList.Count];
            Label[] s_modCodeLbl = new Label[moduleList.Count];
            TextBox[] s_modCode = new TextBox[moduleList.Count];
            Label[] s_assessmentNoLbl = new Label[moduleList.Count];
            TextBox[] s_assessmentNo = new TextBox[moduleList.Count];
            Label[] s_assessment1Lbl = new Label[moduleList.Count];

            for (var i = 0; i < moduleList.Count; i++)
            {
                pn[i] = new Panel();
                s_modNameLbl[i] = new Label();
                s_modName[i] = new TextBox();
                s_modCodeLbl[i] = new Label();
                s_modCode[i] = new TextBox();
                s_assessmentNoLbl[i] = new Label();
                s_assessmentNo[i] = new TextBox();
                s_assessment1Lbl[i] = new Label();
                s_assessmentNo[i] = new TextBox();
                s_assessment1Lbl[i] = new Label();
                


                s_assessmentNo[i].Font = new Font("Verdana", 9.75F, FontStyle.Bold);
                s_assessmentNo[i].AutoSize = true;
                s_assessmentNo[i].Location = new Point(40, 45);
                pn[i].Controls.Add(s_assessmentNo[i]);

                s_assessment1Lbl[i].Location = new Point(150, 34);
                s_assessment1Lbl[i].AutoSize = true;
                pn[i].Controls.Add(s_assessment1Lbl[i]);

                s_assessmentNoLbl[i].Font = new Font("Verdana", 9.75F, FontStyle.Bold);
                s_assessmentNo[i].AutoSize = true;
                s_assessmentNo[i].Location = new Point(360, 45);
                pn[i].Controls.Add(s_assessment1Lbl[i]);

                s_assessmentNo[i].Location = new Point(470, 56);
                s_assessmentNo[i].AutoSize = true;
                pn[i].Controls.Add(s_assessmentNo[i]);
            }
        }

        private void setPredictionStatusLabel(Label lbl)
        {
            lbl.AutoSize = true;
            lbl.Font = new Font("Courgette", 48F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl.ForeColor = Color.FromArgb(90, 131, 187);
            lbl.Location = new Point(220, 57);
            lbl.TabIndex = 0;
            lbl.Text = "No Modules to predict";
        }

        public void resetTabs()
        {

            mainTabControl.Controls.Remove(tabPageLvl4);
            mainTabControl.Controls.Remove(tabPageLvl5);
            mainTabControl.Controls.Remove(tabPageLvl6);
            mainTabControl.Controls.Remove(tabPageModulePrediction);
            mainTabControl.Controls.Remove(degreePredictionTabPage);
        }

        private void AddModuleBtnBlue_Enter(object sender, EventArgs e)
        {
            AddModuleBtnBlue.Image = Properties.Resources.btnHover;
        }

        private void AddModuleBtnBlue_Leave(object sender, EventArgs e)
        {
            AddModuleBtnBlue.Image = Properties.Resources.btn;
        }

        /*click event for adding a new module*/
        private void AddModuleBtnBlue_Click(object sender, EventArgs e)
        {
            string moduleName = moduleNameTxt.Text;
            string moduleCode = moduleCodeTxt.Text;
            string moduleLevel = moduleLevelCbo.Text;
            string moduleAssessmentAmount = moduleAssessmentAmountCbo.Text;
            string moduleCredit = moduleCreditsCbo.Text;
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
                            if (moduleCredit == "")
                            {
                                MessageBox.Show("You must provide the available credits for this module");
                            }
                            else
                            {

                                int fourLimit = 0;
                                int fiveLimit = 0;
                                int sixLimit = 0;
                                fourLimit = moduleLevel == "Level Four" ? Convert.ToInt32(moduleCredit) : 0;
                                fiveLimit = moduleLevel == "Level Five" ? Convert.ToInt32(moduleCredit) : 0;
                                sixLimit = moduleLevel == "Level Six" ? Convert.ToInt32(moduleCredit) : 0;

                                moduleController.setCourseId(cc);
                                List<string> creditCheckList = moduleController.resolveAllModules();
                                bool limitReached = false;
                                foreach (var list in creditCheckList)
                                {
                                    string[] modData = list.Split(',');

                                    if (modData[4] == "four" && moduleLevel == "Level Four")
                                    {
                                        fourLimit += Convert.ToInt32(modData[14]);
                                        if (fourLimit > 120)
                                        {
                                            MessageBox.Show("Level four has a limit of 120 credits.\nThis limit  has been reached.");
                                            limitReached = true;
                                            break;
                                        }
                                    }

                                    if (modData[4] == "five" && moduleLevel == "Level Five")
                                    {
                                        fiveLimit += Convert.ToInt32(modData[14]);
                                        if (fiveLimit > 120)
                                        {
                                            MessageBox.Show("Level five has a limit of 120 credits.\nThis limit  has been reached.");
                                            limitReached = true;
                                            break;
                                        }
                                    }

                                    if (modData[4] == "six" && moduleLevel == "Level Six")
                                    {
                                        sixLimit += Convert.ToInt32(modData[14]);
                                        if (sixLimit > 120)
                                        {
                                            MessageBox.Show("Level six has a limit of 120 credits.\nThis limit  has been reached.");
                                            limitReached = true;
                                            break;
                                        }
                                    }
                                }
                                if (!limitReached)
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
                                        moduleAssessmentAmount,
                                        moduleCredit
                                        );
                                    covertab3Pnl.Visible = false;
                                    addGradeTabPage.Text = "Add Module Grade";

                                    resetComboBox(moduleLevelCbo);
                                    resetComboBox(moduleAssessmentAmountCbo);
                                    resetComboBox(moduleCreditsCbo);
                                    resetTextFields(moduleNameTxt);
                                    resetTextFields(moduleCodeTxt);

                                    moduleName = moduleName.Substring(0, 1).ToUpper() + moduleName.Substring(1).ToLower();
                                    moduleName = Regex.Replace(moduleName, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                                    MessageBox.Show("The " + moduleName + " module has been created.\n" +
                                                                       "You may now grade the assessments.");

                                    mainTabControl.SelectedTab = addGradeTabPage;

                                }
                            }
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
            hidePanel(degreePredictionCoverPnl);
            hidePanel(modulePredictPnl);
            if (level == "four")
            {
                hidePanel(mainCoverLvl4Pnl);
                configLevelTab("four", moduleList, tabPageLvl4);
            }

            if (level == "five")
            {
                hidePanel(mainCoverLvl5Pnl);
                configLevelTab("five", moduleList, tabPageLvl5);
            }
            if (level == "six")
            {
                hidePanel(mainCoverLvl6Pnl);
                configLevelTab("six", moduleList, tabPageLvl6);
            }
            populateAvailableModuleCombo();
            configModulePredictionTab(tabPageModulePrediction);
        }

        /*when a new module is added the system retrieves all the neccessary info concerning existing modules 
         as well as the new one. It gets this data from the relevant controller. It then configures the tabs to contain the relevant data*/
        public void configModulePredictionTab(TabPage currentPage)
        {

            List<string> moduleList = moduleController.resolveAllModules();
            currentPage.Controls.Clear();

            predictionTopControlsPnl = new Panel();
            predictionTopControlsPnl.AutoSize = true;
            predictionTopControlsPnl.Location = new Point(170, 20);

            Label selectLbl = new Label();
            selectLbl.Text = "Select a module";
            selectLbl.AutoSize = true;
            selectLbl.Location = new Point(40, 14);
            selectLbl.Font = new Font("Verdana", 13, FontStyle.Regular);

            hiddenPredictionChoiceCbo = new ComboBox();

            modulePredictionChoiceCbo = new ComboBox();
            modulePredictionChoiceCbo.AutoSize = true;
            modulePredictionChoiceCbo.Width = 400;
            modulePredictionChoiceCbo.DropDownStyle = ComboBoxStyle.DropDownList;
            modulePredictionChoiceCbo.Font = new Font("Verdana", 11, FontStyle.Regular);
            modulePredictionChoiceCbo.Location = new Point(200, 10);
            modulePredictionChoiceCbo.Cursor = Cursors.Hand;

            modPredictChoiceBtn = new Label();
            modPredictChoiceBtn.Cursor = Cursors.Hand;
            modPredictChoiceBtn.Image = Properties.Resources.submit;
            modPredictChoiceBtn.Location = new Point(320, 80);
            modPredictChoiceBtn.Size = new Size(135, 42);

            for (var i = 0; i < moduleList.Count; i++)
            {
                string[] strArray = moduleList[i].Split(',');
                string modName = strArray[1].Substring(0, 1).ToUpper() + strArray[1].Substring(1).ToLower();
                modName = Regex.Replace(modName, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                string modCode = strArray[2].ToUpper();
                string level = strArray[4].Substring(0, 1).ToUpper() + strArray[4].Substring(1).ToLower();
                string comboStr = modName + " - " + modCode + " - Level: " + level;
                modulePredictionChoiceCbo.Items.Add(comboStr);
                hiddenPredictionChoiceCbo.Items.Add(moduleList[i]);
            }

            predictionTopControlsPnl.Controls.Add(selectLbl);
            predictionTopControlsPnl.Controls.Add(modulePredictionChoiceCbo);
            predictionTopControlsPnl.Controls.Add(modPredictChoiceBtn);
            currentPage.Controls.Add(predictionTopControlsPnl);
            predictionTopControlsPnl.BackColor = Color.FromArgb(177, 192, 243);
            modPredictChoiceBtn.Click += modPredictChoiceBtn_Click;
            modPredictChoiceBtn.MouseEnter += modPredictChoiceBtn_Enter;
            modPredictChoiceBtn.MouseLeave += modPredictChoiceBtn_Leave;
        }

        public void modPredictChoiceBtn_Enter(object sender, EventArgs e)
        {
            modPredictChoiceBtn.Image = Properties.Resources.submitHover;
        }

        public void modPredictChoiceBtn_Leave(object sender, EventArgs e)
        {
            modPredictChoiceBtn.Image = Properties.Resources.submit;
        }

        /*this method listens out for the degreePrediction tab to be selected. It will then populate the tab
         with the outcome for the entire degree. This data is obtained from the moduleController*/
        public void tabControl_Selecting(object sender, EventArgs e)
        {
            if (!gradeAssessment)
            {
                setVisibility();
            }

            if (mainTabControl.SelectedTab == degreePredictionTabPage)
            {
                degreePredictionPnl.Controls.Clear();
                degreePredictionTabPage.BackColor = Color.FromArgb(200, 211, 250);
                List<string> fourList = moduleController.resolveAllDegreeModules("four");
                List<string> fiveList = moduleController.resolveAllDegreeModules("five");
                List<string> sixList = moduleController.resolveAllDegreeModules("six");

                var fr = CalculateLevelModules(fourList);
                var fv = CalculateLevelModules(fiveList);
                var sx = CalculateLevelModules(sixList);

                List<int> fours = new List<int>();
                List<int> fives = new List<int>();
                List<int> sixes = new List<int>();

                int i = 0;
                foreach (var outlist in fv)
                {
                    string[] data = fiveList[i++].Split(',');
                    if (Convert.ToInt32(data[6]) != 0)
                    {
                        fives.Add(outlist);

                        if (Convert.ToInt32(data[14]) == 30)
                        {
                            fives.Add(outlist);
                        }
                        if (Convert.ToInt32(data[14]) == 45)
                        {
                            fives.Add(outlist);
                            fives.Add(outlist);
                        }
                    }

                }

                int j = 0;
                foreach (var outlist in sx)
                {
                    string[] data = sixList[j++].Split(',');
                    if (Convert.ToInt32(data[6]) != 0)
                    {
                        sixes.Add(outlist);

                        if (Convert.ToInt32(data[14]) == 30)
                        {
                            sixes.Add(outlist);
                        }
                        if (Convert.ToInt32(data[14]) == 45)
                        {
                            sixes.Add(outlist);
                            sixes.Add(outlist);
                        }
                    }

                }

                int k = 0;
                foreach (var li in fr)
                {
                    string[] data = fourList[k++].Split(',');
                    if (Convert.ToInt32(data[6]) != 0)
                    {
                        fours.Add(li);

                        if (Convert.ToInt32(data[14]) == 30)
                        {
                            fours.Add(li);
                        }
                        if (Convert.ToInt32(data[14]) == 45)
                        {
                            fours.Add(li);
                            fours.Add(li);
                        }
                    }
                }

                Prediction pred = new Prediction();
                int A = moduleController.Level6Outcome(pred, sixes);
                int B = moduleController.Level5Outcome(pred, fives);
                string degreeResult = moduleController.GetFinalDegree(A, B);
                /*
                 * Dynamically added content concerning the final degree result
                 */
                degreePredictionPnl.Controls.Add(dgSummaryTitle);
                degreePredictionPnl.Controls.Add(dgPredictUsernamePnl);
                degreePredictionPnl.Location = new Point(7, 9);
                degreePredictionPnl.Size = new Size(1054, 644);
                degreePredictionPnl.TabIndex = 0;

                Panel allModuleAvgPnl = new Panel();
                allModuleAvgPnl.Location = new Point(600, 150);
                allModuleAvgPnl.AutoSize = true;

                Panel bestOfPnl = new Panel();
                bestOfPnl.AutoSize = true;
                bestOfPnl.Location = new Point(0, 150);
                degreePredictionPnl.Controls.Add(bestOfPnl);

                Label levelFiveLbl = new Label();
                levelFiveLbl.Text = "Best 105 Credits Level 6:";
                levelFiveLbl.Location = new Point(79, 20);
                levelFiveLbl.AutoSize = true;
                levelFiveLbl.Font = new Font("Verdana", 16, FontStyle.Regular);
                bestOfPnl.Controls.Add(levelFiveLbl);

                levelFiveOutputTxt.Text = A + "";
                levelFiveOutputTxt.Font = new Font("Verdana", 28, FontStyle.Regular);
                levelFiveOutputTxt.ForeColor = Color.FromArgb(30, 30, 30);
                levelFiveOutputTxt.Location = new Point(383, 10);
                levelFiveOutputTxt.BorderStyle = BorderStyle.FixedSingle;

                Label levelSixLbl = new Label();
                levelSixLbl.Text = "Best 105 Credits Level 5 and 6:";
                levelSixLbl.Location = new Point(10, 80);
                levelSixLbl.AutoSize = true;
                levelSixLbl.Font = new Font("Verdana", 16, FontStyle.Regular);
                bestOfPnl.Controls.Add(levelSixLbl);

                Label instructLbl = new Label();
                instructLbl.Size = new Size(439, 51);
                instructLbl.Image = Properties.Resources.degreeOutcomeInstruction;
                instructLbl.Location = new Point(10, 190);

                outcomePnl = new Panel();
                outcomePnl.AutoSize = true;

                Label achieveLbl = new Label();
                achieveLbl.AutoSize = true;
                achieveLbl.Font = new Font("Verdana", 28, FontStyle.Regular);
                achieveLbl.Text = "You are expected to achieve a ";
                outcomePnl.Location = new Point(220, 410);
                achieveLbl.Location = new Point(0, 0);
                dgTotalOutComeLbl.Location = new Point(250, 50);
                dgTotalOutComeLbl.AutoSize = true;

                if (degreeResult == "First Class Degree")
                {
                    dgTotalOutComeLbl.Location = new Point(65, 50);
                    dgTotalOutComeLbl.ForeColor = Color.FromArgb(24, 240, 13);
                }

                if (degreeResult == "Upper second Class Degree")
                {
                    outcomePnl.Location = new Point(180, 410);
                    achieveLbl.Location = new Point(50, 0);
                    dgTotalOutComeLbl.Location = new Point(0, 50);
                    dgTotalOutComeLbl.ForeColor = Color.FromArgb(0, 0, 255);
                }

                if (degreeResult == "Lower Second Class Degree")
                {
                    outcomePnl.Location = new Point(180, 410);
                    achieveLbl.Location = new Point(50, 0);
                    dgTotalOutComeLbl.Location = new Point(0, 50);
                    dgTotalOutComeLbl.ForeColor = Color.FromArgb(0, 0, 255);
                }

                if (degreeResult == "Third Class Degree")
                {
                    outcomePnl.Location = new Point(180, 410);
                    achieveLbl.Location = new Point(50, 0);
                    dgTotalOutComeLbl.Location = new Point(90, 50);
                    dgTotalOutComeLbl.ForeColor = Color.FromArgb(240, 128, 53);
                    dgTotalOutComeLbl.Text = "Third Class Degree";
                }

                if (degreeResult == "Fail")
                {
                    outcomePnl.Location = new Point(220, 410);
                    achieveLbl.Location = new Point(0, 0);
                    dgTotalOutComeLbl.Location = new Point(250, 50);
                    dgTotalOutComeLbl.ForeColor = Color.FromArgb(255, 0, 0);
                }

                outcomePnl.Controls.Add(achieveLbl);
                outcomePnl.Controls.Add(dgTotalOutComeLbl);
                degreePredictionPnl.Controls.Add(outcomePnl);

                newValBtn.Size = new Size(159, 42);
                newValBtn.Location = new Point(320, 135);
                newValBtn.Image = Properties.Resources.submitNewValues;
                newValBtn.Cursor = Cursors.Hand;
                newValBtn.Click += newValBtn_Click;
                newValBtn.MouseEnter += newValBtn_Enter;
                newValBtn.MouseLeave += newValBtn_Leave;
                bestOfPnl.Controls.Add(newValBtn);

                levelSixOutputTxt.Text = B + "";
                levelSixOutputTxt.Font = new Font("Verdana", 28, FontStyle.Regular);
                levelSixOutputTxt.ForeColor = Color.FromArgb(30, 30, 30);
                levelSixOutputTxt.Location = new Point(383, 70);
                levelSixOutputTxt.BorderStyle = BorderStyle.FixedSingle;

                Label levelFourAvgLbl = new Label();
                levelFourAvgLbl.Location = new Point(0, 20);
                levelFourAvgLbl.Text = "Level Four avg.";
                levelFourAvgLbl.Font = new Font("Verdana", 16, FontStyle.Regular);
                levelFourAvgLbl.AutoSize = true;

                levelFourAvgTxt.Location = new Point(183, 10);
                levelFourAvgTxt.Font = new Font("Verdana", 28, FontStyle.Regular);
                levelFourAvgTxt.ForeColor = Color.FromArgb(30, 30, 30);
                levelFourAvgTxt.BorderStyle = BorderStyle.FixedSingle;
                int fourAvg = moduleController.getAvg(fours);
                levelFourAvgTxt.Text = fourAvg + "";
                levelFourAvgTxt.Enabled = false;

                Label levelFiveAvgLbl = new Label();
                levelFiveAvgLbl.Location = new Point(0, 80);
                levelFiveAvgLbl.Text = "Level Five avg.";
                levelFiveAvgLbl.Font = new Font("Verdana", 16, FontStyle.Regular);
                levelFiveAvgLbl.AutoSize = true;

                levelFiveAvgTxt.Location = new Point(183, 70);
                levelFiveAvgTxt.Font = new Font("Verdana", 28, FontStyle.Regular);
                levelFiveAvgTxt.ForeColor = Color.FromArgb(30, 30, 30);
                levelFiveAvgTxt.BorderStyle = BorderStyle.FixedSingle;
                int fiveAvg = moduleController.getAvg(fives);
                levelFiveAvgTxt.Text = fiveAvg + "";
                levelFiveAvgTxt.Enabled = false;

                Label levelSixAvgLbl = new Label();
                levelSixAvgLbl.Location = new Point(0, 140);
                levelSixAvgLbl.Text = "Level Six avg.";
                levelSixAvgLbl.Font = new Font("Verdana", 16, FontStyle.Regular);
                levelSixAvgLbl.AutoSize = true;

                levelSixAvgTxt.Location = new Point(183, 130);
                levelSixAvgTxt.Font = new Font("Verdana", 28, FontStyle.Regular);
                levelSixAvgTxt.ForeColor = Color.FromArgb(30, 30, 30);
                levelSixAvgTxt.BorderStyle = BorderStyle.FixedSingle;
                levelSixAvgTxt.Enabled = false;
                int sixAvg = moduleController.getAvg(sixes);
                levelSixAvgTxt.Text = sixAvg + "";

                allModuleAvgPnl.Controls.Add(levelFourAvgLbl);
                allModuleAvgPnl.Controls.Add(levelFourAvgTxt);
                allModuleAvgPnl.Controls.Add(levelFiveAvgLbl);
                allModuleAvgPnl.Controls.Add(levelFiveAvgTxt);
                allModuleAvgPnl.Controls.Add(levelSixAvgLbl);
                allModuleAvgPnl.Controls.Add(levelSixAvgTxt);

                bestOfPnl.Controls.Add(levelFiveOutputTxt);
                bestOfPnl.Controls.Add(levelSixOutputTxt);

                degreePredictionPnl.Controls.Add(allModuleAvgPnl);
                dgCourseNameLbl.Text = cc.CourseName;
                dgUsernameLbl.Text = cc.UserName;
                dgCourseNameLbl.Font = new Font("Verdana", 14, FontStyle.Regular);
                dgUsernameLbl.Font = new Font("Verdana", 14, FontStyle.Regular);
                dgTotalOutComeLbl.Text = degreeResult;
                outcomePnl.BackColor = Color.FromArgb(177, 192, 243);

            }
        }

        public void openExistingPredictionBtn_Enter(object sender, EventArgs e)
        {
            openExistingPredictionBtn.Image = Properties.Resources.openCourseHover;
        }

        public void openExistingPredictionBtn_Leave(object sender, EventArgs e)
        {
            openExistingPredictionBtn.Image = Properties.Resources.openCourse;
        }

        public void submitCourseNameBtn_Enter(object sender, EventArgs e)
        {
            submitCourseNameBtn.Image = Properties.Resources.createCourseHover;
        }

        public void submitCourseNameBtn_Leave(object sender, EventArgs e)
        {
            submitCourseNameBtn.Image = Properties.Resources.create;
        }

        public void newValBtn_Leave(object sender, EventArgs e)
        {
            newValBtn.Image = Properties.Resources.submitNewValues;
        }

        public void newValBtn_Enter(object sender, EventArgs e)
        {
            newValBtn.Image = Properties.Resources.submitNewValuesHover;
        }

        /*This button retrieves the users new value for a degree prediction and sends it to 
         the Prediction class. It then populates the page with the new outcome*/
        public void newValBtn_Click(object sender, EventArgs e)
        {
            string aVal = levelFiveOutputTxt.Text;
            string bVal = levelSixOutputTxt.Text;
            Prediction pred = new Prediction();
            try
            {
                int A = Convert.ToInt32(aVal);
                int B = Convert.ToInt32(bVal);
                if (A <= 100 && B <= 100)
                {
                    levelFourAvgTxt.Text = " - ";
                    levelFiveAvgTxt.Text = " - ";
                    levelSixAvgTxt.Text = " - ";
                    dgTotalOutComeLbl.Text = "";
                    String degreeResult = pred.FinalDegreeResult(A, B);
                    outcomePnl.Controls.Clear();
                    Label achieveLbl = new Label();

                    achieveLbl.AutoSize = true;
                    achieveLbl.Font = new Font("Verdana", 28, FontStyle.Regular);
                    achieveLbl.Text = "You are expected to achieve a ";
                    outcomePnl.Location = new Point(220, 410);
                    achieveLbl.Location = new Point(0, 0);
                    dgTotalOutComeLbl.Location = new Point(250, 50);

                    if (degreeResult == "First Class Degree")
                    {
                        dgTotalOutComeLbl.Location = new Point(65, 50);
                        dgTotalOutComeLbl.ForeColor = Color.FromArgb(24, 240, 13);
                        dgTotalOutComeLbl.Text = "First Class Degree";
                    }

                    if (degreeResult == "Upper second Class Degree")
                    {
                        outcomePnl.Location = new Point(180, 410);
                        achieveLbl.Location = new Point(50, 0);
                        dgTotalOutComeLbl.Location = new Point(0, 50);
                        dgTotalOutComeLbl.ForeColor = Color.FromArgb(0, 0, 255);
                        dgTotalOutComeLbl.Text = "Upper second Class Degree";
                    }

                    if (degreeResult == "Lower Second Class Degree")
                    {
                        outcomePnl.Location = new Point(180, 410);
                        achieveLbl.Location = new Point(50, 0);
                        dgTotalOutComeLbl.Location = new Point(0, 50);
                        dgTotalOutComeLbl.ForeColor = Color.FromArgb(0, 0, 255);
                        dgTotalOutComeLbl.Text = "Lower Second Class Degree";
                    }

                    if (degreeResult == "Third Class Degree")
                    {
                        outcomePnl.Location = new Point(180, 410);
                        achieveLbl.Location = new Point(50, 0);
                        dgTotalOutComeLbl.Location = new Point(90, 50);
                        dgTotalOutComeLbl.ForeColor = Color.FromArgb(240, 128, 53);
                        dgTotalOutComeLbl.Text = "Third Class Degree";
                    }

                    if (degreeResult == "Fail")
                    {
                        outcomePnl.Location = new Point(220, 410);
                        achieveLbl.Location = new Point(0, 0);
                        dgTotalOutComeLbl.Location = new Point(250, 50);
                        dgTotalOutComeLbl.ForeColor = Color.FromArgb(255, 0, 0);
                        dgTotalOutComeLbl.Text = "Fail";
                    }

                    outcomePnl.Controls.Add(achieveLbl);
                    outcomePnl.BackColor = Color.FromArgb(177, 192, 243);
                    outcomePnl.Controls.Add(dgTotalOutComeLbl);
                    degreePredictionPnl.Controls.Add(outcomePnl);
                }
                else
                {
                    MessageBox.Show("The value you have entered must not exceed 100");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("The value you have entered is not a number");
            }
        }

        /*This method requests a prediction for a given module. */
        private static List<int> CalculateLevelModules(List<string> moduleList)
        {
            List<int> res = new List<int>();
            foreach (var list in moduleList)
            {
                Prediction prediction = new Prediction();
                prediction.modulePrediction(list);
                prediction.ResolveAllResults();

                int actual = prediction.getModuleTotal();
                res.Add(actual);
            }
            return res;
        }

        private void modPredictChoiceBtn_Click(object sender, EventArgs e)
        {
            int index = modulePredictionChoiceCbo.SelectedIndex;
            if (index != -1)
            {
                if (detailsPnl != null)
                {
                    detailsPnl.Controls.Clear();
                }

                string vals = hiddenPredictionChoiceCbo.Items[index].ToString();

                Prediction prediction = new Prediction();
                prediction.modulePrediction(vals);
                prediction.ResolveAllResults();

                int actual = prediction.getModuleTotal();

                instructionLbl = new Label();
                instructionLbl.Image = Properties.Resources.modPassInstruction;
                instructionLbl.Location = new Point(0, 33);
                instructionLbl.Size = new Size(531, 50);
                instructionLbl.BringToFront();

                legendLbl = new Label();
                legendLbl.Image = Properties.Resources.legend;
                legendLbl.Location = new Point(561, 0);
                legendLbl.Size = new Size(165, 102);

                instructionPnl = new Panel();
                instructionPnl.Location = new Point(190, 145);
                instructionPnl.Controls.Add(instructionLbl);
                instructionPnl.Controls.Add(legendLbl);
                instructionPnl.Size = new Size(726, 102);
                detailsPnl = new Panel();

                List<string> data = cc.getCourseData(prediction.CourseId);
                string[] courseDat = data[0].Split(',');

                detailsPnl.Location = new Point(60, 250);
                detailsPnl.BackColor = Color.FromArgb(177, 192, 243);
                detailsPnl.AutoSize = true;

                Label courseDetailsLbl = new Label();
                courseDetailsLbl.Location = new Point(200, 0);
                courseDetailsLbl.Font = new Font("Verdana", 11.25F, FontStyle.Bold);
                courseDetailsLbl.AutoSize = true;
                courseDetailsLbl.ForeColor = Color.FromArgb(88, 89, 90);
                courseDetailsLbl.Text = GetCapitalValue(courseDat[0]) + "      " + GetCapitalValue(courseDat[1]);

                moduleResultLbl = new Label();
                moduleResultLbl.Location = new Point(660, 0);
                moduleResultLbl.AutoSize = true;
                moduleResultLbl.Text = actual.ToString();
                moduleResultLbl.Font = new Font("Courgette", 100.25F, FontStyle.Regular);
                moduleDetailsLbl = new Label();
                moduleDetailsLbl.Location = new Point(200, 30);
                moduleDetailsLbl.AutoSize = true;
                moduleDetailsLbl.ForeColor = Color.FromArgb(88, 89, 90);
                moduleDetailsLbl.Font = new Font("Verdana", 11.25F, FontStyle.Bold);
                moduleDetailsLbl.Text = modulePredictionChoiceCbo.Text; ;

                int numberOfAssessments = prediction.AssessmentAmount;

                if (numberOfAssessments == 1)
                {
                    layoutOne(prediction, vals);
                }
                if (numberOfAssessments == 2)
                {
                    layoutTwo(prediction, vals);
                }
                if (numberOfAssessments == 3)
                {
                    layoutThree(prediction, vals);
                }
                if (numberOfAssessments == 4)
                {
                    layoutFour(prediction, vals);
                }

                string[] allData = vals.Split(',');

                if (actual < 40)
                {
                    moduleResultLbl.ForeColor = Color.FromArgb(246, 11, 11);
                }
                else
                {
                    if ((actual > 40) && (parseData(allData[6]) < 30 && parseData(allData[7]) > 0) ||
                        (parseData(allData[8]) < 30 && parseData(allData[9]) > 0) ||
                        (parseData(allData[10]) < 30 && parseData(allData[11]) > 0) ||
                        (parseData(allData[12]) < 30 && parseData(allData[13]) > 0))
                    {
                        moduleResultLbl.ForeColor = Color.FromArgb(246, 132, 11);
                    }
                    else
                    {
                        moduleResultLbl.ForeColor = Color.FromArgb(24, 240, 13);
                    }
                }

                detailsPnl.Controls.Add(courseDetailsLbl);
                detailsPnl.Controls.Add(moduleDetailsLbl);
                detailsPnl.Controls.Add(moduleResultLbl);

                tabPageModulePrediction.BackColor = Color.FromArgb(177, 192, 243);
                tabPageModulePrediction.Controls.Add(instructionPnl);
                tabPageModulePrediction.Controls.Add(detailsPnl);
            }
            else
            {
                MessageBox.Show("You have not chosen a module");
            }
        }

        private int parseData(string s)
        {
            try
            {
                return Convert.ToInt32(s);
            }
            catch (OverflowException e)
            {
                
            }
            return -1;
        }

        private void layoutOne(Prediction prediction, string vals)
        {
            string[] allData = vals.Split(',');
            Label assessOneTitle = new Label();
            assessOneTitle.AutoSize = true;
            assessOneTitle.ForeColor = Color.FromArgb(50, 50, 51);
            assessOneTitle.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessOneTitle.Text = "Assessment One: " + allData[6] + "/100";
            Label assessOneWeight = new Label();
            assessOneWeight.AutoSize = true;
            assessOneWeight.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessOneWeight.ForeColor = Color.FromArgb(50, 50, 51);
            assessOneWeight.Text = "Weight: " + allData[7];
            assessOneTitle.Location = new Point(200, 55);
            assessOneWeight.Location = new Point(200, 75);
            detailsPnl.Controls.Add(assessOneTitle);
            detailsPnl.Controls.Add(assessOneWeight);
        }

        private void layoutTwo(Prediction prediction, string vals)
        {
            //first assessment
            string[] allData = vals.Split(',');
            Label assessOneTitle = new Label();
            assessOneTitle.AutoSize = true;
            assessOneTitle.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessOneTitle.ForeColor = Color.FromArgb(50, 50, 51);
            assessOneTitle.Text = "Assessment One: " + allData[6] + "/100";
            Label assessOneWeight = new Label();
            assessOneWeight.AutoSize = true;
            assessOneWeight.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessOneWeight.ForeColor = Color.FromArgb(50, 50, 51);
            assessOneWeight.Text = "Weight: " + allData[7];
            assessOneTitle.Location = new Point(200, 55);
            assessOneWeight.Location = new Point(200, 75);
            detailsPnl.Controls.Add(assessOneTitle);
            detailsPnl.Controls.Add(assessOneWeight);
            //second assessment
            Label assessTwoTitle = new Label();
            assessTwoTitle.AutoSize = true;
            assessTwoTitle.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessTwoTitle.ForeColor = Color.FromArgb(50, 50, 51);
            assessTwoTitle.Text = "Assessment Two: " + allData[8] + "/100";
            Label assessTwoWeight = new Label();
            assessTwoWeight.AutoSize = true;
            assessTwoWeight.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessTwoWeight.ForeColor = Color.FromArgb(50, 50, 51);
            assessTwoWeight.Text = "Weight: " + allData[9];
            assessTwoTitle.Location = new Point(200, 95);
            assessTwoWeight.Location = new Point(200, 115);
            detailsPnl.Controls.Add(assessTwoTitle);
            detailsPnl.Controls.Add(assessTwoWeight);
        }

        private void layoutThree(Prediction prediction, string vals)
        {
            //first assessment
            string[] allData = vals.Split(',');
            Label assessOneTitle = new Label();
            assessOneTitle.AutoSize = true;
            assessOneTitle.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessOneTitle.ForeColor = Color.FromArgb(50, 50, 51);
            assessOneTitle.Text = "Assessment One: " + allData[6] + "/100";
            Label assessOneWeight = new Label();
            assessOneWeight.AutoSize = true;
            assessOneWeight.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessOneWeight.ForeColor = Color.FromArgb(50, 50, 51);
            assessOneWeight.Text = "Weight: " + allData[7];
            assessOneTitle.Location = new Point(200, 55);
            assessOneWeight.Location = new Point(200, 75);
            detailsPnl.Controls.Add(assessOneTitle);
            detailsPnl.Controls.Add(assessOneWeight);
            //second assessment
            Label assessTwoTitle = new Label();
            assessTwoTitle.AutoSize = true;
            assessTwoTitle.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessTwoTitle.ForeColor = Color.FromArgb(50, 50, 51);
            assessTwoTitle.Text = "Assessment Two: " + allData[8] + "/100";
            Label assessTwoWeight = new Label();
            assessTwoWeight.AutoSize = true;
            assessTwoWeight.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessTwoWeight.ForeColor = Color.FromArgb(50, 50, 51);
            assessTwoWeight.Text = "Weight: " + allData[9];
            assessTwoTitle.Location = new Point(200, 95);
            assessTwoWeight.Location = new Point(200, 115);
            detailsPnl.Controls.Add(assessTwoTitle);
            detailsPnl.Controls.Add(assessTwoWeight);
            //third assessment
            Label assessThreeTitle = new Label();
            assessThreeTitle.AutoSize = true;
            assessThreeTitle.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessThreeTitle.ForeColor = Color.FromArgb(50, 50, 51);
            assessThreeTitle.Text = "Assessment Three: " + allData[10] + "/100";
            Label assessThreeWeight = new Label();
            assessThreeWeight.AutoSize = true;
            assessThreeWeight.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessThreeWeight.ForeColor = Color.FromArgb(50, 50, 51);
            assessThreeWeight.Text = "Weight: " + allData[11];
            assessThreeTitle.Location = new Point(200, 135);
            assessThreeWeight.Location = new Point(200, 155);
            detailsPnl.Controls.Add(assessThreeTitle);
            detailsPnl.Controls.Add(assessThreeWeight);
        }

        private void layoutFour(Prediction prediction, string vals)
        {
            //first assessment
            string[] allData = vals.Split(',');
            Label assessOneTitle = new Label();
            assessOneTitle.AutoSize = true;
            assessOneTitle.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessOneTitle.ForeColor = Color.FromArgb(50, 50, 51);
            assessOneTitle.Text = "Assessment One: " + allData[6] + "/100";
            Label assessOneWeight = new Label();
            assessOneWeight.AutoSize = true;
            assessOneWeight.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessOneWeight.ForeColor = Color.FromArgb(50, 50, 51);
            assessOneWeight.Text = "Weight: " + allData[7];
            assessOneTitle.Location = new Point(200, 55);
            assessOneWeight.Location = new Point(200, 75);
            detailsPnl.Controls.Add(assessOneTitle);
            detailsPnl.Controls.Add(assessOneWeight);
            //second assessment
            Label assessTwoTitle = new Label();
            assessTwoTitle.AutoSize = true;
            assessTwoTitle.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessTwoTitle.ForeColor = Color.FromArgb(50, 50, 51);
            assessTwoTitle.Text = "Assessment Two: " + allData[8] + "/100";
            Label assessTwoWeight = new Label();
            assessTwoWeight.AutoSize = true;
            assessTwoWeight.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessTwoWeight.ForeColor = Color.FromArgb(50, 50, 51);
            assessTwoWeight.Text = "Weight: " + allData[9];
            assessTwoTitle.Location = new Point(200, 95);
            assessTwoWeight.Location = new Point(200, 115);
            detailsPnl.Controls.Add(assessTwoTitle);
            detailsPnl.Controls.Add(assessTwoWeight);
            //third assessment
            Label assessThreeTitle = new Label();
            assessThreeTitle.AutoSize = true;
            assessThreeTitle.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessThreeTitle.ForeColor = Color.FromArgb(50, 50, 51);
            assessThreeTitle.Text = "Assessment Three: " + allData[10] + "/100";
            Label assessThreeWeight = new Label();
            assessThreeWeight.AutoSize = true;
            assessThreeWeight.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessThreeWeight.ForeColor = Color.FromArgb(50, 50, 51);
            assessThreeWeight.Text = "Weight: " + allData[11];
            assessThreeTitle.Location = new Point(200, 135);
            assessThreeWeight.Location = new Point(200, 155);
            detailsPnl.Controls.Add(assessThreeTitle);
            detailsPnl.Controls.Add(assessThreeWeight);
            //third assessment
            Label assessFourTitle = new Label();
            assessFourTitle.AutoSize = true;
            assessFourTitle.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessFourTitle.ForeColor = Color.FromArgb(50, 50, 51);
            assessFourTitle.Text = "Assessment Four: " + allData[12] + "/100";
            Label assessFourWeight = new Label();
            assessFourWeight.AutoSize = true;
            assessFourWeight.Font = new Font("Verdana", 12.25F, FontStyle.Bold);
            assessFourWeight.ForeColor = Color.FromArgb(50, 50, 51);
            assessFourWeight.Text = "Weight: " + allData[13];
            assessFourTitle.Location = new Point(200, 175);
            assessFourWeight.Location = new Point(200, 195);
            detailsPnl.Controls.Add(assessFourTitle);
            detailsPnl.Controls.Add(assessFourWeight);
        }

        protected string GetCapitalValue(string name)
        {
            string modName = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
            modName = Regex.Replace(modName, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
            return modName;
        }

        public void configLevelTab(string level, List<string> moduleList, TabPage currentPage)
        {
            int panelStart = 10;
            const int TOP_ROW_POS = 10;
            const int MID_ROW_POS = 30;
            const int SECOND_ROW_POS = 50;

            currentPage.Controls.Clear();
            panel = new Panel[moduleList.Count];
            modNameLbl = new Label[moduleList.Count];
            modName = new Label[moduleList.Count];
            modCodeLbl = new Label[moduleList.Count];
            modCode = new Label[moduleList.Count];
            assessmentNoLbl = new Label[moduleList.Count];
            assessmentNo = new Label[moduleList.Count];
            assessment1Lbl = new Label[moduleList.Count];
            assessment1 = new Label[moduleList.Count];
            assessment2Lbl = new Label[moduleList.Count];
            assessment2 = new Label[moduleList.Count];
            assessment3Lbl = new Label[moduleList.Count];
            assessment3 = new Label[moduleList.Count];
            assessment4Lbl = new Label[moduleList.Count];
            assessment4 = new Label[moduleList.Count];
            Label[] creditLbl = new Label[moduleList.Count];
            Label[] credit = new Label[moduleList.Count];
            removeBtn = new Label[moduleList.Count];

            for (var i = 0; i < moduleList.Count; i++)
            {

                string[] strArray = moduleList[i].Split(',');
                panel[i] = new Panel();
                modNameLbl[i] = new Label();
                modName[i] = new Label();
                modCodeLbl[i] = new Label();
                modCode[i] = new Label();
                assessmentNoLbl[i] = new Label();
                assessmentNo[i] = new Label();
                assessment1Lbl[i] = new Label();
                assessment1[i] = new Label();
                assessment2Lbl[i] = new Label();
                assessment2[i] = new Label();
                assessment3Lbl[i] = new Label();
                assessment3[i] = new Label();
                assessment4Lbl[i] = new Label();
                assessment4[i] = new Label();
                assessment1Lbl[i].Text = null;
                creditLbl[i] = new Label();
                credit[i] = new Label();
                removeBtn[i] = new Label();


                modNameLbl[i].Text = "Module Name:";
                modName[i].Text = GetCapitalValue(strArray[1]);
                modCodeLbl[i].Text = "Module Code:";
                modCode[i].Text = strArray[2].ToUpper();
                assessmentNoLbl[i].Text = "No. of Assessments:";
                assessmentNo[i].Text = strArray[5];
                assessment1Lbl[i].Text = "Assessment One:";
                assessment1[i].Text = null;
                assessment1[i].BringToFront();
                assessment1[i].Text = strArray[6];
                assessment2Lbl[i].Text = "Assessment Two:";
                assessment2[i].Text = strArray[7];
                assessment3Lbl[i].Text = "Assessment Three:";
                assessment3[i].Text = strArray[8];
                assessment4Lbl[i].Text = "Assessment Four:";
                assessment4[i].Text = strArray[9];
                creditLbl[i].Text = "Credits:";
                credit[i].Text = strArray[10];


                panel[i].Location = new Point(60, panelStart);
                panel[i].Padding = new Padding(0, 0, 25, 0);
                panel[i].AutoSize = true;
                panel[i].BackColor = Color.DarkBlue;
                panel[i].Height = 70;
                panel[i].BringToFront();

                currentPage.Controls.Add(panel[i]);
                currentPage.BringToFront();
                modNameLbl[i].Font = new Font("Verdana", 9.75F, FontStyle.Bold);
                modNameLbl[i].AutoSize = true;
                modNameLbl[i].Location = new Point(40, TOP_ROW_POS);
                panel[i].Controls.Add(modNameLbl[i]);

                modName[i].Location = new Point(150, TOP_ROW_POS);
                modName[i].AutoSize = true;
                panel[i].Controls.Add(modName[i]);

                modCodeLbl[i].Font = new Font("Verdana", 9.75F, FontStyle.Bold);
                modCodeLbl[i].AutoSize = true;
                modCodeLbl[i].Location = new Point(360, TOP_ROW_POS);
                panel[i].Controls.Add(modCodeLbl[i]);

                modCode[i].Location = new Point(470, TOP_ROW_POS);
                modCode[i].AutoSize = true;
                panel[i].Controls.Add(modCode[i]);

                assessmentNoLbl[i].Font = new Font("Verdana", 9.75F, FontStyle.Bold);
                assessmentNoLbl[i].Location = new Point(660, TOP_ROW_POS);
                assessmentNoLbl[i].AutoSize = true;
                panel[i].Controls.Add(assessmentNoLbl[i]);

                creditLbl[i].Font = new Font("Verdana", 9.75F, FontStyle.Bold);
                creditLbl[i].Location = new Point(40, MID_ROW_POS);
                creditLbl[i].AutoSize = true;
                panel[i].Controls.Add(creditLbl[i]);

                credit[i].Font = new Font("Verdana", 9.75F, FontStyle.Regular);
                credit[i].Location = new Point(104, MID_ROW_POS);
                credit[i].AutoSize = true;
                panel[i].Controls.Add(credit[i]);

                assessmentNo[i].Location = new Point(820, TOP_ROW_POS);
                assessmentNo[i].AutoSize = true;
                panel[i].Controls.Add(assessmentNo[i]);

                removeBtn[i].Location = new Point(859, TOP_ROW_POS + 15);
                removeBtn[i].AutoSize = false;
                removeBtn[i].Cursor = Cursors.Hand;
                removeBtn[i].Size = new Size(98, 26);
                removeBtn[i].Name = strArray[0];
                removeBtn[i].Click += removeElement_Click;
                removeBtn[i].Image = Properties.Resources.remove1;
                panel[i].Controls.Add(removeBtn[i]);

                if (i % 2 == 0)
                {
                    panel[i].BackColor = Color.FromArgb(200, 211, 250);
                }
                else
                {
                    panel[i].BackColor = Color.FromArgb(105, 193, 196);
                }
                if (strArray[5] == "1")
                {
                    setOneAssessment(assessment1Lbl, i, SECOND_ROW_POS, panel, assessment1);
                }
                if (strArray[5] == "2")
                {
                    setTwoAssessments(assessment1Lbl, i, SECOND_ROW_POS, panel, assessment1, assessment2Lbl, assessment2);
                }
                if (strArray[5] == "3")
                {
                    setThreeAssessments(assessment1Lbl, i, SECOND_ROW_POS, panel, assessment1, assessment2Lbl, assessment2, assessment3Lbl, assessment3);
                }
                if (strArray[5] == "4")
                {
                    setFourAssessments(assessment1Lbl, i, SECOND_ROW_POS, panel, assessment1, assessment2Lbl, assessment2, assessment3Lbl, assessment3, assessment4Lbl, assessment4);
                }
                panelStart += 80;

            }

        }

        public void removeElement_Click(object sender, EventArgs e)
        {
            string moduleID = ((Label)sender).Name;
            moduleController.removeModule(moduleID);
            moduleController.getAllModulesByLevel();
           List <string> list = moduleController.resolveAllModules();
            populateAvailableModuleCombo();
            modulePredictionChoiceCbo.Items.Clear();
            hiddenPredictionChoiceCbo.Items.Clear();
            for (var i = 0; i < list.Count; i++)
            {
                string[] strArray = list[i].Split(',');
                string modName = strArray[1].Substring(0, 1).ToUpper() + strArray[1].Substring(1).ToLower();
                modName = Regex.Replace(modName, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                string modCode = strArray[2].ToUpper();
                string level = strArray[4].Substring(0, 1).ToUpper() + strArray[4].Substring(1).ToLower();
                string comboStr = modName + " - " + modCode + " - Level: " + level;
                modulePredictionChoiceCbo.Items.Add(comboStr);
                hiddenPredictionChoiceCbo.Items.Add(list[i]);
            }
        }

        private void setThreeAssessments(Label[] assessment1Lbl, int i, int SECOND_ROW_POS, Panel[] panel, Label[] assessment1, Label[] assessment2Lbl, Label[] assessment2, Label[] assessment3Lbl, Label[] assessment3)
        {
            assessment1Lbl[i].Location = new Point(20, SECOND_ROW_POS);
            assessment1Lbl[i].AutoSize = true;

            panel[i].Controls.Add(assessment1Lbl[i]);

            assessment1[i].Location = new Point(140, SECOND_ROW_POS);
            assessment1[i].AutoSize = true;
            panel[i].Controls.Add(assessment1[i]);

            assessment2Lbl[i].Location = new Point(220, SECOND_ROW_POS);
            assessment2Lbl[i].AutoSize = true;
            panel[i].Controls.Add(assessment2Lbl[i]);

            assessment2[i].Location = new Point(340, SECOND_ROW_POS);
            assessment2[i].AutoSize = true;
            panel[i].Controls.Add(assessment2[i]);

            assessment3Lbl[i].Location = new Point(430, SECOND_ROW_POS);
            assessment3Lbl[i].AutoSize = true;
            panel[i].Controls.Add(assessment3Lbl[i]);

            assessment3[i].Location = new Point(560, SECOND_ROW_POS);
            assessment3[i].AutoSize = true;
            panel[i].Controls.Add(assessment3[i]);
        }

        private void setTwoAssessments(Label[] assessment1Lbl, int i, int SECOND_ROW_POS, Panel[] panel, Label[] assessment1, Label[] assessment2Lbl, Label[] assessment2)
        {
            assessment1Lbl[i].Location = new Point(20, SECOND_ROW_POS);
            assessment1Lbl[i].AutoSize = true;

            panel[i].Controls.Add(assessment1Lbl[i]);

            assessment1[i].Location = new Point(140, SECOND_ROW_POS);
            assessment1[i].AutoSize = true;
            panel[i].Controls.Add(assessment1[i]);

            assessment2Lbl[i].Location = new Point(220, SECOND_ROW_POS);
            assessment2Lbl[i].AutoSize = true;
            panel[i].Controls.Add(assessment2Lbl[i]);

            assessment2[i].Location = new Point(340, SECOND_ROW_POS);
            assessment2[i].AutoSize = true;
            panel[i].Controls.Add(assessment2[i]);
        }

        private static void setFourAssessments(Label[] assessment1Lbl, int i, int SECOND_ROW_POS, Panel[] panel,
            Label[] assessment1, Label[] assessment2Lbl, Label[] assessment2, Label[] assessment3Lbl, Label[] assessment3,
            Label[] assessment4Lbl, Label[] assessment4)
        {
            assessment1Lbl[i].Location = new Point(20, SECOND_ROW_POS);
            assessment1Lbl[i].AutoSize = true;

            panel[i].Controls.Add(assessment1Lbl[i]);

            assessment1[i].Location = new Point(140, SECOND_ROW_POS);
            assessment1[i].AutoSize = true;
            panel[i].Controls.Add(assessment1[i]);

            assessment2Lbl[i].Location = new Point(220, SECOND_ROW_POS);
            assessment2Lbl[i].AutoSize = true;
            panel[i].Controls.Add(assessment2Lbl[i]);

            assessment2[i].Location = new Point(340, SECOND_ROW_POS);
            assessment2[i].AutoSize = true;
            panel[i].Controls.Add(assessment2[i]);

            assessment3Lbl[i].Location = new Point(430, SECOND_ROW_POS);
            assessment3Lbl[i].AutoSize = true;
            panel[i].Controls.Add(assessment3Lbl[i]);

            assessment3[i].Location = new Point(560, SECOND_ROW_POS);
            assessment3[i].AutoSize = true;
            panel[i].Controls.Add(assessment3[i]);

            assessment4Lbl[i].Location = new Point(670, SECOND_ROW_POS);
            assessment4Lbl[i].AutoSize = true;
            panel[i].Controls.Add(assessment4Lbl[i]);

            assessment4[i].Location = new Point(800, SECOND_ROW_POS);
            assessment4[i].AutoSize = true;
            panel[i].Controls.Add(assessment4[i]);
        }

        private static void setOneAssessment(Label[] assessment1Lbl, int i, int SECOND_ROW_POS, Panel[] panel,
            Label[] assessment1)
        {
            assessment1Lbl[i].Location = new Point(20, SECOND_ROW_POS);
            assessment1Lbl[i].AutoSize = true;

            panel[i].Controls.Add(assessment1Lbl[i]);

            assessment1[i].Location = new Point(140, SECOND_ROW_POS);
            assessment1[i].AutoSize = true;
            panel[i].Controls.Add(assessment1[i]);
        }

        public void availableModulesCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            gradeAssessment = true;
            setVisibility();
            int index = availableModulesCbo.SelectedIndex;
            string moduleDBId = hiddenModuleGradesCbo.Items[index].ToString();
            string[] modGrades = moduleDBId.Split(',');
            assessment1GradeTxt.Text = modGrades[0];
            assessment1WeightTxt.Text = modGrades[1];
            assessment2GradeTxt.Text = modGrades[2];
            assessment2WeightTxt.Text = modGrades[3];
            assessment3GradeTxt.Text = modGrades[4];
            assessment3WeightTxt.Text = modGrades[5];
            assessment4GradeTxt.Text = modGrades[6];
            assessment4WeightTxt.Text = modGrades[7];
            hiddenLbl.Text = modGrades[8];
            getModAvg();
            assessment1GradeTxt.TextChanged += assessment1GradeText_TextChanged;
            assessment1WeightTxt.TextChanged += assessment1GradeText_TextChanged;
            assessment2GradeTxt.TextChanged += assessment1GradeText_TextChanged;
            assessment2WeightTxt.TextChanged += assessment1GradeText_TextChanged;
            assessment3GradeTxt.TextChanged += assessment1GradeText_TextChanged;
            assessment3WeightTxt.TextChanged += assessment1GradeText_TextChanged;
            assessment4GradeTxt.TextChanged += assessment1GradeText_TextChanged;
            assessment4WeightTxt.TextChanged += assessment1GradeText_TextChanged;
            if (Convert.ToInt32(modGrades[8]) < 2)
            {
                percent2Lbl.Visible = false;
                assessment2GradeTxt.Visible = false;
                assessment2WeightTxt.Visible = false;
                assGrade2WeightLbl.Visible = false;
                assGrade2Lbl.Visible = false;
            }

            if (Convert.ToInt32(modGrades[8]) < 3)
            {
                percent3Lbl.Visible = false;
                assessment3GradeTxt.Visible = false;
                assessment3WeightTxt.Visible = false;
                assGrade3WeightLbl.Visible = false;
                assGrade3Lbl.Visible = false;
            }

            if (Convert.ToInt32(modGrades[8]) < 4)
            {
                percent4Lbl.Visible = false;
                assessment4GradeTxt.Visible = false;
                assessment4WeightTxt.Visible = false;
                assGrade4WeightLbl.Visible = false;
                assGrade4Lbl.Visible = false;
            }
           
        }

        private void assessment1GradeText_TextChanged(object sender, EventArgs e)
        {
            getModAvg();
        }



        private void addGradesBtn_Enter(object sender, EventArgs e)
        {
            addGradesBtn.Image = Properties.Resources.addGradesBtnHover;
        }
        private void addGradesBtn_Leave(object sender, EventArgs e)
        {
            addGradesBtn.Image = Properties.Resources.addGradesBtn;
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
                assessment2 = assessment2 == "" || assessment2 == "-" ? "0" : assessment2;
                string assessment2Weight = assessment2WeightTxt.Text;
                assessment2Weight = assessment2Weight == "" || assessment2Weight == "-" ? "0" : assessment2Weight;

                string assessment3 = assessment3GradeTxt.Text;
                assessment3 = assessment3 == "" || assessment3 == "-" ? "0" : assessment3;
                string assessment3Weight = assessment3WeightTxt.Text;
                assessment3Weight = assessment3Weight == "" || assessment3Weight == "-" ? "0" : assessment3Weight;

                string assessment4 = assessment4GradeTxt.Text;
                assessment4 = assessment4 == "" || assessment4 == "-" ? "0" : assessment4;
                string assessment4Weight = assessment4WeightTxt.Text;
                assessment4Weight = assessment4Weight == "" || assessment4Weight == "-" ? "0" : assessment4Weight;

                try
                {
                    int assess1 = validateString(assessment1);
                    assessment1 = returnToString(assess1);

                    int assess1W = validateString(assessment1Weight);
                    assessment1Weight = returnToString(assess1W);

                    int assess2 = validateString(assessment2);
                    assessment2 = returnToString(assess2);

                    int assess2W = validateString(assessment2Weight);
                    assessment2Weight = returnToString(assess2W);

                    int assess3 = validateString(assessment3);
                    assessment3 = returnToString(assess3);

                    int assess3W = validateString(assessment3Weight);
                    assessment3Weight = returnToString(assess3W);

                    int assess4 = validateString(assessment4);
                    assessment4 = returnToString(assess4);

                    int assess4W = validateString(assessment4Weight);
                    assessment4Weight = returnToString(assess4W);

                    if ((assess1 > 100) || (assess2 > 100) || (assess3 > 100) || (assess4 > 100) ||
                        (assess1W > 100) || (assess2W > 100) || (assess3W > 100) || (assess4W > 100))
                    {
                        MessageBox.Show("Grades and Weights values cannot exceed 100");
                    }
                    else
                    {
                        if ((assess1 < 0) || (assess2 < 0) || (assess3 < 0) || (assess4 < 0) ||
                            (assess1W < 0) || (assess2W < 0) || (assess3W < 0) || (assess4W < 0))
                        {
                            MessageBox.Show("Grades and Weights values cannot fall beneath 0");
                        }
                        else
                        {
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

                            string modulename = modData[0];
                            modulename = modulename.Substring(0, 1).ToUpper() + modulename.Substring(1).ToLower();
                            modulename = Regex.Replace(modulename, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                            assessController.setSpecificModule(modData, moduleDBId);
                            assessController.updateSystem();
                            moduleController.getAllModulesByLevel();
                            resetTextFields(assessment1GradeTxt);
                            resetTextFields(assessment1WeightTxt);
                            resetTextFields(assessment2GradeTxt);
                            resetTextFields(assessment2WeightTxt);
                            resetTextFields(assessment3GradeTxt);
                            resetTextFields(assessment3WeightTxt);
                            resetTextFields(assessment4GradeTxt);
                            resetTextFields(assessment4WeightTxt);
                            MessageBox.Show("New grades have been added to the " + modulename + " module");
                            assessment1GradeTxt.Focus();
                            gradeAssessment = false;

                        }
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("You must only use numbers for grades");
                }
            }
        }

        private void getModAvg()
        {
            string tempstr = assessment1GradeTxt.Text == "" ? "0" : assessment1GradeTxt.Text;
            string tempstr2 = assessment1WeightTxt.Text == "" ? "0" : assessment1WeightTxt.Text;
            string tempstr3 = assessment2GradeTxt.Text == "" ? "0" : assessment2GradeTxt.Text;
            string tempstr4 = assessment2WeightTxt.Text == "" ? "0" : assessment2WeightTxt.Text;
            string tempstr5 = assessment3GradeTxt.Text == "" ? "0" : assessment3GradeTxt.Text;
            string tempstr6 = assessment3WeightTxt.Text == "" ? "0" : assessment3WeightTxt.Text;
            string tempstr7 = assessment4GradeTxt.Text == "" ? "0" : assessment4GradeTxt.Text;
            string tempstr8 = assessment4WeightTxt.Text == "" ? "0" : assessment4WeightTxt.Text;
            try
            {
                int[] tmpInt = new int[8];
                tmpInt[0] = parseData(tempstr);
                tmpInt[1] = parseData(tempstr2);
                tmpInt[2] = parseData(tempstr3);
                tmpInt[3] = parseData(tempstr4);
                tmpInt[4] = parseData(tempstr5);
                tmpInt[5] = parseData(tempstr6);
                tmpInt[6] = parseData(tempstr7);
                tmpInt[7] = parseData(tempstr8);
                if ((tmpInt[0] <= 100 && tmpInt[0] >= 0) && (tmpInt[1] <= 100 && tmpInt[1] >= 0) &&
                    (tmpInt[2] <= 100 && tmpInt[2] >= 0) &&
                    (tmpInt[3] <= 100 && tmpInt[3] >= 0) && (tmpInt[4] <= 100 && tmpInt[4] >= 0) &&
                    (tmpInt[5] <= 100 && tmpInt[5] >= 0) &&
                    (tmpInt[6] <= 100 && tmpInt[6] >= 0) && (tmpInt[7] <= 100 && tmpInt[7] >= 0))
                {
                    string assessmentVals = ",,,,," + hiddenLbl.Text + "," +
                                            tempstr + "," +
                                            tempstr2 + "," +
                                            tempstr3 + "," +
                                            tempstr4 + "," +
                                            tempstr5 + "," +
                                            tempstr6 + "," +
                                            tempstr7 + "," +
                                            tempstr8;

                    Prediction prediction = new Prediction();
                    prediction.modulePrediction(assessmentVals);
                    prediction.ResolveAllResults();

                    int actual = prediction.getModuleTotal();
                    modTotalLbl.Text = actual.ToString();
     
                    if (actual < 40)
                    {
                        modTotalLbl.ForeColor = Color.FromArgb(246, 11, 11);
                    }
                    else
                    {
                        if ((actual > 40) && (tmpInt[0] < 30) && (tmpInt[1] > 0) ||
                            (tmpInt[2] < 30) && (tmpInt[3] > 0) ||
                            (tmpInt[4] < 30) && (tmpInt[5] > 0) ||
                            (tmpInt[6] < 30) && (tmpInt[7] > 0))
                        {
                            modTotalLbl.ForeColor = Color.FromArgb(246, 132, 11);
                        }
                        else
                        {
                            modTotalLbl.ForeColor = Color.FromArgb(24, 240, 13);
                        }
                    }
                }
                else
                {
                    modTotalLbl.Text = "Not valid";
                    modTotalLbl.ForeColor = Color.FromArgb(15, 11, 11);
                }
            }
            catch (FormatException ex)
            {
                modTotalLbl.Text = "Not valid";
                modTotalLbl.ForeColor = Color.FromArgb(15, 11, 11);
            }
        }

        private void setVisibility()
        {
            assessment2GradeTxt.Visible = true;
            assessment2WeightTxt.Visible = true;
            assessment3GradeTxt.Visible = true;
            assessment3WeightTxt.Visible = true;
            assessment4GradeTxt.Visible = true;
            assessment4WeightTxt.Visible = true;
            percent2Lbl.Visible = true;
            percent3Lbl.Visible = true;
            percent4Lbl.Visible = true;


            assGrade2Lbl.Visible = true;
            assGrade2WeightLbl.Visible = true;
            assGrade3Lbl.Visible = true;
            assGrade3WeightLbl.Visible = true;
            assGrade4Lbl.Visible = true;
            assGrade4WeightLbl.Visible = true;
        }
        private string returnToString(int grade)
        {
            return grade.ToString();
        }

        private int validateString(string grade)
        {
            return int.Parse(grade);
        }

        private void populateAvailableModuleCombo()
        {
            List<string> modList = moduleController.resolveAllModules();
            availableModulesCbo.Items.Clear();
            hiddenCombo.Items.Clear();
            hiddenModuleGradesCbo = new ComboBox();
            for (var i = 0; i < modList.Count; i++)
            {
                string[] modArr = modList[i].Split(',');
                string modname = Regex.Replace(modArr[1], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                string modCode = modArr[2].ToUpper();
                string modLevel = modArr[4].Substring(0, 1).ToUpper() + modArr[4].Substring(1).ToLower();
                string moduleDetail = modname + "- " + modCode + " - Level: " + modLevel;

                availableModulesCbo.Items.Add(moduleDetail);

                string grades = modArr[6] + "," + modArr[7] + "," + modArr[8] + "," + modArr[9] + "," + modArr[10] + "," +
                                modArr[11] + "," + modArr[12] + "," + modArr[13] + "," + modArr[5];
                hiddenCombo.Items.Add(modArr[0]);
                hiddenModuleGradesCbo.Items.Add(grades);
            }

        }

        public void buildCourseBtn_Enter(object sender, EventArgs e)
        {
            buildCourseBtn.Image = Properties.Resources.buildCourseHover;
        }

        public void buildCourseBtn_Leave(object sender, EventArgs e)
        {
            buildCourseBtn.Image = Properties.Resources.buildCourse;
        }

        public void activeCreateNewCourseBtn_Enter(object sender, EventArgs e)
        {
            activeCreateNewCourseBtn.Image = Properties.Resources.createNewCourseHover;
        }

        public void activeCreateNewCourseBtn_Leave(object sender, EventArgs e)
        {
            activeCreateNewCourseBtn.Image = Properties.Resources.createNewCourse;
        }

        private void activeCreateNewCourseBtn_Click(object sender, EventArgs e)
        {
            mainCoverPnl.Visible = false;
            mainCoverPnl.SendToBack();
        }

        private void openExistingPredictionBtn_Click(object sender, EventArgs e)
        {
            resetTabs();
            string existingData = existingCourseCbo.Text;
            if (existingData == "")
            {
                MessageBox.Show("You did not select an existing course");
            }
            else
            {
                cc.setDependencies(new Course(), new User());
                cc.MatchCourseData(existingData);
                string id = cc.getCourseDbId();
                moduleController.CourseId = id;
                bool result = cc.setExistingData(existingData);

                ConfigureTabs();
                mainCoverPnl.Visible = true;
                mainCoverPnl.BringToFront();
                mainTabControl.SelectedTab = tabPage2;

                string queryParameters = existingData;
                string[] strArray = queryParameters.Split(',');
                cc.assignNewUser(strArray[0], strArray[1]);
                string user = strArray[0].Substring(0, 1).ToUpper() + strArray[0].Substring(1).ToLower();
                user = Regex.Replace(user, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                string course = strArray[1].Substring(0, 1).ToUpper() + strArray[1].Substring(1).ToLower();
                course = Regex.Replace(course, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                addModUserLbl.Text = user;
                addModCourseLbl.Text = course;
                covertab2Pnl.Visible = true;
                covertab2Pnl.BringToFront();
                moduleNameTxt.Focus();

                moduleController.resetAllModules();
                covertab3Pnl.Visible = false;
                addGradeTabPage.Text = "Add Module Grade";
                existingCourseCbo.SelectedIndex = -1;
                availableModulesCbo.Items.Clear();
                populateAvailableModuleCombo();
                resetTextFields(assessment1GradeTxt);
                resetTextFields(assessment1WeightTxt);
                resetTextFields(assessment2GradeTxt);
                resetTextFields(assessment2WeightTxt);
                resetTextFields(assessment3GradeTxt);
                resetTextFields(assessment3WeightTxt);
                resetTextFields(assessment4GradeTxt);
                resetTextFields(assessment4WeightTxt);
                gradeAssessment = false;
            }
        }
            /*this method retrieves and sends the given course name to relevant controller*/
        private void submitCourseNameBtn_Click(object sender, EventArgs e)
        {

            availableModulesCbo.Items.Clear();
            degreePredictionCoverPnl.Visible = true;

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
                        resetTextFields(userNameTxt);
                        resetComboBox(availableCoursesCbo);
                        MessageBox.Show("A new course has been created.\n" +
                                                               "You may now add modules to it.");
                        ConfigureTabs();
                        mainCoverPnl.Visible = true;
                        mainCoverPnl.BringToFront();
                        mainTabControl.SelectedTab = tabPage2;
                        addModUserLbl.Text = username;
                        addModCourseLbl.Text = chosenCourse;
                        covertab2Pnl.Visible = true;
                        covertab2Pnl.BringToFront();
                        moduleNameTxt.Focus();
                        gradeAssessment = false;
                    }
                }
            }
        }

        /*This button starts the process of building a course*/
        private void buildCourseBtn_Click(object sender, EventArgs e)
        {
            selectCoursePnl.Visible = true;
            openExistingCoursePnl.Visible = true;
        }

        public void LevelFourCleanUp()
        {
            tabPageLvl4.Controls.Clear();
            mainCoverLvl4Pnl.Visible = true;
            levFourStatusLbl.Visible = true;
            AddCover(tabPageLvl4, mainCoverLvl4Pnl, levFourStatusLbl);
        }

        public void LevelFiveCleanUp()
        {
            tabPageLvl5.Controls.Clear();
            AddCover(tabPageLvl5, mainCoverLvl5Pnl, levFiveStatusLbl);
            mainCoverLvl5Pnl.Visible = true;
            levFiveStatusLbl.Visible = true;
        }

        public void LevelSixCleanUp()
        {
            tabPageLvl6.Controls.Clear();
            mainCoverLvl6Pnl.Visible = true;
            levSixStatusLbl.Visible = true;
            AddCover(tabPageLvl6, mainCoverLvl6Pnl, levSixStatusLbl);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace OOP._0._1._1
{
    public partial class StartUp : Form
    {
        private Boolean existingState = false;
        private delegate void RunOnThreadPool(int threadId);
        private TabControl tabControlViews;

        private TabPage tabPageLvl4, tabPageLvl5, tabPageLvl6, tabPageModulePrediction, tabPageView5;
        private Thread t;
        private Database db;

        private User user;
        private Panel mainCoverLvl4Pnl, mainCoverLvl5Pnl, mainCoverLvl6Pnl, modulePredictPnl;
        private Label levFourStatusLbl, levFiveStatusLbl, levSixStatusLbl, modulePredictStatusLbl;
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
            AddModulePredictionCover(tabPageModulePrediction, modulePredictPnl, modulePredictStatusLbl);


            availableModulesCbo.DropDownStyle = ComboBoxStyle.DropDownList;
            availableModulesCbo.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            availableModulesCbo.FormattingEnabled = true;
            availableModulesCbo.Location = new Point(376, 94);
            availableModulesCbo.Name = "availableModulesCbo";
            availableModulesCbo.Size = new Size(396, 26);
            availableModulesCbo.TabIndex = 1;
            availableModulesCbo.SelectedIndexChanged += availableModulesCbo_SelectedIndexChanged;
            availableModulesCbo.Cursor = Cursors.Hand;

            moduleAssessmentAmountCbo.Cursor = Cursors.Hand;
        }

        private void AddCover(TabPage tb, Panel pnl, Label lbl)
        {
            tb.Controls.Add(pnl);
            pnl.Controls.Add(lbl);
            pnl.Location = new Point(3, 0);
            pnl.Name = "mainCoverPnl";
            pnl.Size = new Size(690, 216);
            pnl.Visible = true;
        }

        private void AddModulePredictionCover(TabPage tb, Panel pnl, Label lbl)
        {
            tb.Controls.Add(pnl);
            pnl.Controls.Add(lbl);
            pnl.Location = new Point(3, 0);
            pnl.Name = "mainCoverPnl";
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
            lbl.Name = "moduleStatus";
            lbl.Size = new Size(185, 24);
            lbl.Text = "There are no modules set for this level";
        }

        private void setPredictionStatusLabel(Label lbl)
        {
            lbl.AutoSize = true;
            lbl.Font = new Font("Courgette", 48F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl.ForeColor = Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(131)))), ((int)(((byte)(187)))));
            lbl.Location = new Point(220, 57);
            lbl.Name = "labelg";
            //lbl.Size = new Size(477, 81);
            lbl.TabIndex = 0;
            lbl.Text = "No Modules to predict";
        }

        //private void buildCourseBtn_Click(object sender, System.EventArgs e)
        //{
        //    selectCoursePnl.Visible = true;
        //    openExistingCoursePnl.Visible = true;
        //}

        //private void SubmitCourseNameBtn_Click(object sender, EventArgs e)
        //{
        //    if (existingState)
        //    {
        //        availableModulesCbo.Items.Clear();
        //        existingState = false;
        //    }

        //    resetTabs();
        //    string chosenCourse = availableCoursesCbo.Text;
        //    string username = userNameTxt.Text;

        //    if (chosenCourse == "")
        //    {
        //        MessageBox.Show("Please select a course");
        //    }
        //    else
        //    {
        //        if (username == "")
        //        {
        //            MessageBox.Show("Please enter your name");
        //        }
        //        else
        //        {
        //            username = username.Substring(0, 1).ToUpper() + username.Substring(1).ToLower();
        //            cc.setDependencies(new Course(), new User());
        //            bool result = cc.setData(username, chosenCourse);
        //            if (result)
        //            {
        //                resetTextFields(userNameTxt);
        //                resetComboBox(availableCoursesCbo);
        //                MessageBox.Show("A new course has been created.\n" +
        //                                                       "You may now add modules to it.");
        //                ConfigureTabs();
        //                mainCoverPnl.Visible = true;
        //                mainCoverPnl.BringToFront();
        //                mainTabControl.SelectedTab = tabPage2;
        //                addModUserLbl.Text = username;
        //                addModCourseLbl.Text = chosenCourse;
        //                covertab2Pnl.Visible = true;
        //                covertab2Pnl.BringToFront();
        //                moduleNameTxt.Focus();

        //            }
        //        }
        //    }
        //}

        public void resetTabs()
        {

            this.mainTabControl.Controls.Remove(this.tabPageLvl4);
            this.mainTabControl.Controls.Remove(this.tabPageLvl5);
            this.mainTabControl.Controls.Remove(this.tabPageLvl6);
            this.mainTabControl.Controls.Remove(this.tabPageModulePrediction);
        }

        //private void openExistingPredictionBtn_Click(object sender, EventArgs e)
        //{
        //    resetTabs();
        //    string existingData = existingCourseCbo.Text;
        //    if (existingData == "")
        //    {
        //        MessageBox.Show("You did not select an existing course");
        //    }
        //    else
        //    {
        //        cc.setDependencies(new Course(), new User());
        //        cc.MatchCourseData(existingData);
        //        string id = cc.getCourseDbId();
        //        moduleController.CourseId = id;
        //        bool result = cc.setExistingData(existingData);

        //        mainCoverPnl.Visible = true;
        //        mainCoverPnl.BringToFront();
        //        mainTabControl.SelectedTab = tabPage2;

        //        string queryParameters = existingData;
        //        string[] strArray = queryParameters.Split(',');
        //        string user = strArray[0].Substring(0, 1).ToUpper() + strArray[0].Substring(1).ToLower();
        //        user = Regex.Replace(user, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

        //        string course = strArray[1].Substring(0, 1).ToUpper() + strArray[1].Substring(1).ToLower();
        //        course = Regex.Replace(course, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

        //        addModUserLbl.Text = user;
        //        addModCourseLbl.Text = course;
        //        covertab2Pnl.Visible = true;
        //        covertab2Pnl.BringToFront();
        //        moduleNameTxt.Focus();
        //        ConfigureTabs();
        //        moduleController.resetAllModules();
        //        covertab3Pnl.Visible = false;
        //        addGradeTabPage.Text = "Add Module Grade";
        //        existingCourseCbo.SelectedIndex = -1;
        //        existingState = true;

        //    }

        //}



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

        public void configModulePredictionTab(TabPage currentPage)
        {

            List<string> moduleList = moduleController.resolveAllModules();
            currentPage.Controls.Clear();


            predictionTopControlsPnl = new Panel();
            predictionTopControlsPnl.AutoSize = true;
            predictionTopControlsPnl.Location = new Point(40, 20);


            Label selectLbl = new Label();
            selectLbl.Text = "Select a module";
            selectLbl.AutoSize = true;
            selectLbl.Location = new Point(40, 17);

            hiddenPredictionChoiceCbo = new ComboBox();


            modulePredictionChoiceCbo = new ComboBox();
            modulePredictionChoiceCbo.AutoSize = true;
            modulePredictionChoiceCbo.Width = 400;
            modulePredictionChoiceCbo.DropDownStyle = ComboBoxStyle.DropDownList;
            modulePredictionChoiceCbo.Font = new Font("Verdana", 11, FontStyle.Regular);
            modulePredictionChoiceCbo.Location = new Point(150, 10);
            modulePredictionChoiceCbo.Cursor = Cursors.Hand;

            modPredictChoiceBtn = new Label();
            modPredictChoiceBtn.Cursor = Cursors.Hand;
            modPredictChoiceBtn.Image = Properties.Resources.submit;
            modPredictChoiceBtn.Location = new Point(60, 80);
            modPredictChoiceBtn.Name = "modPredictChoiceBtn";
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
            //assessment2Lbl[i].Location = new Point(220, SECOND_ROW_POS);
            //assessment2Lbl[i].AutoSize = true;
            //panel[i].Controls.Add(assessment2Lbl[i]);
            predictionTopControlsPnl.Controls.Add(selectLbl);
            predictionTopControlsPnl.Controls.Add(modulePredictionChoiceCbo);
            predictionTopControlsPnl.Controls.Add(modPredictChoiceBtn);
            modPredictChoiceBtn.BackColor = Color.Brown;
            currentPage.Controls.Add(predictionTopControlsPnl);
            predictionTopControlsPnl.BackColor = Color.Blue;
            modPredictChoiceBtn.Click += this.modPredictChoiceBtn_Click;

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
                instructionPnl.Location = new Point(30, 145);
                instructionPnl.Controls.Add(instructionLbl);
                instructionPnl.Controls.Add(legendLbl);
                instructionPnl.Size = new Size(726, 102);
                instructionPnl.BackColor = Color.CadetBlue;
                detailsPnl = new Panel();

                List<string> data = cc.getCourseData(prediction.CourseId);
                string[] courseDat = data[0].Split(',');


                detailsPnl.Location = new Point(0, 250);
                detailsPnl.BackColor = Color.Transparent;
                detailsPnl.AutoSize = true;

                Label courseDetailsLbl = new Label();
                courseDetailsLbl.Location = new Point(200, 0);
                courseDetailsLbl.Font = new Font("Verdana", 11.25F, FontStyle.Regular);
                courseDetailsLbl.BackColor = Color.BlueViolet;
                courseDetailsLbl.AutoSize = true;
                courseDetailsLbl.Text = GetCapitalValue(courseDat[0]) + "      " + GetCapitalValue(courseDat[1]);


                moduleResultLbl = new Label();
                moduleResultLbl.Location = new Point(600, 0);
                moduleResultLbl.AutoSize = true;
                moduleResultLbl.Text = actual.ToString();
                moduleResultLbl.BackColor = Color.BlueViolet;
                moduleResultLbl.Font = new Font("Courgette", 100.25F, FontStyle.Regular);

                moduleDetailsLbl = new Label();
                moduleDetailsLbl.Location = new Point(200, 30);
                moduleDetailsLbl.AutoSize = true;
                moduleDetailsLbl.BackColor = Color.BlueViolet;
                moduleDetailsLbl.Font = new Font("Verdana", 11.25F, FontStyle.Regular);
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
                        //246,132,11 = referral - 246, 11, 11 = fail - 24, 240, 13 = pass
                    }
                    else
                    {
                        moduleResultLbl.ForeColor = Color.FromArgb(24, 240, 13);
                    }
                }



                //resultsPnl.Controls.Add(moduleResultLbl);
                detailsPnl.Controls.Add(courseDetailsLbl);
                detailsPnl.Controls.Add(moduleDetailsLbl);
                detailsPnl.Controls.Add(moduleResultLbl);
                detailsPnl.BackColor = Color.Chartreuse;
                tabPageModulePrediction.Controls.Add(instructionPnl);
                tabPageModulePrediction.Controls.Add(detailsPnl);
                //tabPageModulePrediction.Controls.Add(resultsPnl);
            }
            else
            {
                MessageBox.Show("You have not chosen a module");
            }

        }

        private int parseData(string s)
        {
            return Convert.ToInt32(s);
        }


        private void layoutOne(Prediction prediction, string vals)
        {
            string[] allData = vals.Split(',');
            Label assessOneTitle = new Label();
            assessOneTitle.AutoSize = true;
            assessOneTitle.Text = "Assessment One: " + allData[6] + "/100";
            Label assessOneWeight = new Label();
            assessOneWeight.AutoSize = true;
            assessOneWeight.Text = "Weight: " + allData[7];
            assessOneTitle.Location = new Point(200, 50);
            assessOneWeight.Location = new Point(200, 70);
            detailsPnl.Controls.Add(assessOneTitle);
            detailsPnl.Controls.Add(assessOneWeight);
        }

        private void layoutTwo(Prediction prediction, string vals)
        {
            //first assessment
            string[] allData = vals.Split(',');
            Label assessOneTitle = new Label();
            assessOneTitle.AutoSize = true;
            assessOneTitle.Text = "Assessment One: " + allData[6] + "/100";
            Label assessOneWeight = new Label();
            assessOneWeight.AutoSize = true;
            assessOneWeight.Text = "Weight: " + allData[7];
            assessOneTitle.Location = new Point(200, 50);
            assessOneWeight.Location = new Point(200, 70);
            detailsPnl.Controls.Add(assessOneTitle);
            detailsPnl.Controls.Add(assessOneWeight);
            //second assessment
            Label assessTwoTitle = new Label();
            assessTwoTitle.AutoSize = true;
            assessTwoTitle.Text = "Assessment One: " + allData[8] + "/100";
            Label assessTwoWeight = new Label();
            assessTwoWeight.AutoSize = true;
            assessTwoWeight.Text = "Weight: " + allData[9];
            assessTwoTitle.Location = new Point(200, 90);
            assessTwoWeight.Location = new Point(200, 110);
            detailsPnl.Controls.Add(assessTwoTitle);
            detailsPnl.Controls.Add(assessTwoWeight);
        }

        private void layoutThree(Prediction prediction, string vals)
        {
            //first assessment
            string[] allData = vals.Split(',');
            Label assessOneTitle = new Label();
            assessOneTitle.AutoSize = true;
            assessOneTitle.Text = "Assessment One: " + allData[6] + "/100";
            Label assessOneWeight = new Label();
            assessOneWeight.AutoSize = true;
            assessOneWeight.Text = "Weight: " + allData[7];
            assessOneTitle.Location = new Point(200, 50);
            assessOneWeight.Location = new Point(200, 70);
            detailsPnl.Controls.Add(assessOneTitle);
            detailsPnl.Controls.Add(assessOneWeight);
            //second assessment
            Label assessTwoTitle = new Label();
            assessTwoTitle.AutoSize = true;
            assessTwoTitle.Text = "Assessment One: " + allData[8] + "/100";
            Label assessTwoWeight = new Label();
            assessTwoWeight.AutoSize = true;
            assessTwoWeight.Text = "Weight: " + allData[9];
            assessTwoTitle.Location = new Point(200, 90);
            assessTwoWeight.Location = new Point(200, 110);
            detailsPnl.Controls.Add(assessTwoTitle);
            detailsPnl.Controls.Add(assessTwoWeight);
            //third assessment
            Label assessThreeTitle = new Label();
            assessThreeTitle.AutoSize = true;
            assessThreeTitle.Text = "Assessment One: " + allData[10] + "/100";
            Label assessThreeWeight = new Label();
            assessThreeWeight.AutoSize = true;
            assessThreeWeight.Text = "Weight: " + allData[11];
            assessThreeTitle.Location = new Point(200, 130);
            assessThreeWeight.Location = new Point(200, 150);
            detailsPnl.Controls.Add(assessThreeTitle);
            detailsPnl.Controls.Add(assessThreeWeight);
        }


        private void layoutFour(Prediction prediction, string vals)
        {
            //first assessment
            string[] allData = vals.Split(',');
            Label assessOneTitle = new Label();
            assessOneTitle.AutoSize = true;
            assessOneTitle.Text = "Assessment One: " + allData[6] + "/100";
            Label assessOneWeight = new Label();
            assessOneWeight.AutoSize = true;
            assessOneWeight.Text = "Weight: " + allData[7];
            assessOneTitle.Location = new Point(200, 50);
            assessOneWeight.Location = new Point(200, 70);
            detailsPnl.Controls.Add(assessOneTitle);
            detailsPnl.Controls.Add(assessOneWeight);
            //second assessment
            Label assessTwoTitle = new Label();
            assessTwoTitle.AutoSize = true;
            assessTwoTitle.Text = "Assessment One: " + allData[8] + "/100";
            Label assessTwoWeight = new Label();
            assessTwoWeight.AutoSize = true;
            assessTwoWeight.Text = "Weight: " + allData[9];
            assessTwoTitle.Location = new Point(200, 90);
            assessTwoWeight.Location = new Point(200, 110);
            detailsPnl.Controls.Add(assessTwoTitle);
            detailsPnl.Controls.Add(assessTwoWeight);
            //third assessment
            Label assessThreeTitle = new Label();
            assessThreeTitle.AutoSize = true;
            assessThreeTitle.Text = "Assessment One: " + allData[8] + "/100";
            Label assessThreeWeight = new Label();
            assessThreeWeight.AutoSize = true;
            assessThreeWeight.Text = "Weight: " + allData[9];
            assessThreeTitle.Location = new Point(200, 130);
            assessThreeWeight.Location = new Point(200, 150);
            detailsPnl.Controls.Add(assessThreeTitle);
            detailsPnl.Controls.Add(assessThreeWeight);
            //third assessment
            Label assessFourTitle = new Label();
            assessFourTitle.AutoSize = true;
            assessFourTitle.Text = "Assessment One: " + allData[8] + "/100";
            Label assessFourWeight = new Label();
            assessFourWeight.AutoSize = true;
            assessFourWeight.Text = "Weight: " + allData[9];
            assessFourTitle.Location = new Point(200, 170);
            assessFourWeight.Location = new Point(200, 190);
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
            DateTime now = DateTime.Now;

            for (var i = 0; i < moduleList.Count; i++)
            {

                string[] strArray = moduleList[i].Split(',');
                //MessageBox.Show("Module " + moduleList[i] + " contains " + strArray[0] + "," + strArray[1] + "," + strArray[2] + "," + strArray[3] + "," + strArray[4] + "," + strArray[5]);
                //MessageBox.Show("    indexOf     " + i + "Module " + moduleList[i] + " contains " + strArray[0] + "," + strArray[1] + "," + strArray[2] + "," + strArray[3] + "," + strArray[4] + "," + strArray[5] + "," + strArray[6] + "," + strArray[7] + "," + strArray[8] + "," + strArray[9]);
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
                credit[i].Text =  strArray[10];


                panel[i].Location = new Point(100, panelStart);
                panel[i].Padding = new Padding(0, 0, 25, 0);

                panel[i].AutoSize = true;
                panel[i].BackColor = Color.DarkBlue;
                panel[i].Height = 70;
                //panel[i].Width = 700;
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
                credit[i].Location = new Point(100, MID_ROW_POS);
                credit[i].AutoSize = true;
                
                panel[i].Controls.Add(credit[i]);

                assessmentNo[i].Location = new Point(820, TOP_ROW_POS);
                assessmentNo[i].AutoSize = true;
                panel[i].Controls.Add(assessmentNo[i]);
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

                        }
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("You must only use numbers for grades");
                }
            }
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

                //string modname = Regex.Replace(modArr[1], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                string moduleDetail = modname + "- " + modCode + " - Level: " + modLevel;

                availableModulesCbo.Items.Add(moduleDetail);


                string grades = modArr[6] + "," + modArr[7] + "," + modArr[8] + "," + modArr[9] + "," + modArr[10] + "," +
                                modArr[11] + "," + modArr[12] + "," + modArr[13];
                hiddenCombo.Items.Add(modArr[0]);
                hiddenModuleGradesCbo.Items.Add(grades);
            }

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
                existingState = true;

            }
        }

        private void submitCourseNameBtn_Click(object sender, EventArgs e)
        {
            //if (existingState)
            //{
            availableModulesCbo.Items.Clear();

            existingState = false;
            //}

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

                    }
                }
            }
        }

        private void buildCourseBtn_Click(object sender, EventArgs e)
        {
            selectCoursePnl.Visible = true;
            openExistingCoursePnl.Visible = true;
        }
    }
}

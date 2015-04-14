namespace OOP._0._1._1
{
    partial class StartUp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.selectCoursePnl = new System.Windows.Forms.Panel();
            this.SubmitCourseNameBtn = new System.Windows.Forms.Button();
            this.levelCbo = new System.Windows.Forms.ComboBox();
            this.availableCoursesCbo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buildCourseBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.userNameTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mainTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.selectCoursePnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.tabPage1);
            this.mainTabControl.Controls.Add(this.tabPage2);
            this.mainTabControl.Location = new System.Drawing.Point(1, 1);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(602, 366);
            this.mainTabControl.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.selectCoursePnl);
            this.tabPage1.Controls.Add(this.buildCourseBtn);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(594, 340);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // selectCoursePnl
            // 
            this.selectCoursePnl.Controls.Add(this.label2);
            this.selectCoursePnl.Controls.Add(this.userNameTxt);
            this.selectCoursePnl.Controls.Add(this.SubmitCourseNameBtn);
            this.selectCoursePnl.Controls.Add(this.levelCbo);
            this.selectCoursePnl.Controls.Add(this.availableCoursesCbo);
            this.selectCoursePnl.Controls.Add(this.label1);
            this.selectCoursePnl.Location = new System.Drawing.Point(3, 6);
            this.selectCoursePnl.Name = "selectCoursePnl";
            this.selectCoursePnl.Size = new System.Drawing.Size(588, 331);
            this.selectCoursePnl.TabIndex = 1;
            this.selectCoursePnl.Visible = false;
            // 
            // SubmitCourseNameBtn
            // 
            this.SubmitCourseNameBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubmitCourseNameBtn.Location = new System.Drawing.Point(275, 244);
            this.SubmitCourseNameBtn.Name = "SubmitCourseNameBtn";
            this.SubmitCourseNameBtn.Size = new System.Drawing.Size(140, 41);
            this.SubmitCourseNameBtn.TabIndex = 4;
            this.SubmitCourseNameBtn.Text = "Create";
            this.SubmitCourseNameBtn.UseVisualStyleBackColor = true;
            this.SubmitCourseNameBtn.Click += new System.EventHandler(this.SubmitCourseNameBtn_Click);
            // 
            // levelCbo
            // 
            this.levelCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.levelCbo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelCbo.FormattingEnabled = true;
            this.levelCbo.Items.AddRange(new object[] {
            "Level Three",
            "Level Four",
            "Level Five"});
            this.levelCbo.Location = new System.Drawing.Point(216, 184);
            this.levelCbo.Name = "levelCbo";
            this.levelCbo.Size = new System.Drawing.Size(199, 26);
            this.levelCbo.TabIndex = 3;
            // 
            // availableCoursesCbo
            // 
            this.availableCoursesCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.availableCoursesCbo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availableCoursesCbo.FormattingEnabled = true;
            this.availableCoursesCbo.Location = new System.Drawing.Point(216, 133);
            this.availableCoursesCbo.Name = "availableCoursesCbo";
            this.availableCoursesCbo.Size = new System.Drawing.Size(199, 26);
            this.availableCoursesCbo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(99, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Course";
            // 
            // buildCourseBtn
            // 
            this.buildCourseBtn.Location = new System.Drawing.Point(40, 38);
            this.buildCourseBtn.Name = "buildCourseBtn";
            this.buildCourseBtn.Size = new System.Drawing.Size(75, 23);
            this.buildCourseBtn.TabIndex = 0;
            this.buildCourseBtn.Text = "Build Course";
            this.buildCourseBtn.UseVisualStyleBackColor = true;
            this.buildCourseBtn.Click += new System.EventHandler(this.buildCourseBtn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(594, 340);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // userNameTxt
            // 
            this.userNameTxt.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameTxt.Location = new System.Drawing.Point(216, 63);
            this.userNameTxt.Name = "userNameTxt";
            this.userNameTxt.Size = new System.Drawing.Size(302, 26);
            this.userNameTxt.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(99, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Enter Name";
            // 
            // StartUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 365);
            this.Controls.Add(this.mainTabControl);
            this.Name = "StartUp";
            this.Text = "StartUp";
            this.mainTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.selectCoursePnl.ResumeLayout(false);
            this.selectCoursePnl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel selectCoursePnl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buildCourseBtn;
        private System.Windows.Forms.ComboBox availableCoursesCbo;
        private System.Windows.Forms.ComboBox levelCbo;
        private System.Windows.Forms.Button SubmitCourseNameBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox userNameTxt;
    }
}


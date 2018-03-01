namespace NewAntProject
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.lblAnts = new System.Windows.Forms.Label();
            this.lblPherEffect = new System.Windows.Forms.Label();
            this.lblPherEvapor = new System.Windows.Forms.Label();
            this.tbAnts = new System.Windows.Forms.TextBox();
            this.tbPherEffect = new System.Windows.Forms.TextBox();
            this.tbPherEvapor = new System.Windows.Forms.TextBox();
            this.lblFinish = new System.Windows.Forms.Label();
            this.pbRecord = new System.Windows.Forms.ProgressBar();
            this.lblN = new System.Windows.Forms.Label();
            this.tbC = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.rbHybrid = new System.Windows.Forms.RadioButton();
            this.rbSimple = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(12, 62);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(100, 50);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Открыть";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(216, 62);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 50);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Начать";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // ofd
            // 
            this.ofd.DefaultExt = "txt";
            this.ofd.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            this.ofd.Title = "Открыть файл";
            // 
            // sfd
            // 
            this.sfd.DefaultExt = "txt";
            this.sfd.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            this.sfd.Title = "Сохранить как";
            // 
            // lblAnts
            // 
            this.lblAnts.AutoSize = true;
            this.lblAnts.Location = new System.Drawing.Point(12, 130);
            this.lblAnts.Name = "lblAnts";
            this.lblAnts.Size = new System.Drawing.Size(121, 13);
            this.lblAnts.TabIndex = 3;
            this.lblAnts.Text = "Количество муравьев:";
            // 
            // lblPherEffect
            // 
            this.lblPherEffect.AutoSize = true;
            this.lblPherEffect.Location = new System.Drawing.Point(12, 155);
            this.lblPherEffect.Name = "lblPherEffect";
            this.lblPherEffect.Size = new System.Drawing.Size(114, 13);
            this.lblPherEffect.TabIndex = 4;
            this.lblPherEffect.Text = "Влияние феромонов:";
            // 
            // lblPherEvapor
            // 
            this.lblPherEvapor.AutoSize = true;
            this.lblPherEvapor.Location = new System.Drawing.Point(12, 180);
            this.lblPherEvapor.Name = "lblPherEvapor";
            this.lblPherEvapor.Size = new System.Drawing.Size(176, 13);
            this.lblPherEvapor.TabIndex = 5;
            this.lblPherEvapor.Text = "Скорость испарения феромонов:";
            // 
            // tbAnts
            // 
            this.tbAnts.Location = new System.Drawing.Point(194, 127);
            this.tbAnts.Name = "tbAnts";
            this.tbAnts.ReadOnly = true;
            this.tbAnts.Size = new System.Drawing.Size(122, 20);
            this.tbAnts.TabIndex = 8;
            // 
            // tbPherEffect
            // 
            this.tbPherEffect.Location = new System.Drawing.Point(194, 152);
            this.tbPherEffect.Name = "tbPherEffect";
            this.tbPherEffect.ReadOnly = true;
            this.tbPherEffect.Size = new System.Drawing.Size(122, 20);
            this.tbPherEffect.TabIndex = 9;
            // 
            // tbPherEvapor
            // 
            this.tbPherEvapor.Location = new System.Drawing.Point(194, 177);
            this.tbPherEvapor.Name = "tbPherEvapor";
            this.tbPherEvapor.ReadOnly = true;
            this.tbPherEvapor.Size = new System.Drawing.Size(122, 20);
            this.tbPherEvapor.TabIndex = 10;
            // 
            // lblFinish
            // 
            this.lblFinish.AutoSize = true;
            this.lblFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFinish.Location = new System.Drawing.Point(12, 231);
            this.lblFinish.Name = "lblFinish";
            this.lblFinish.Size = new System.Drawing.Size(167, 13);
            this.lblFinish.TabIndex = 11;
            this.lblFinish.Text = "Пожалуйста, подождите... ";
            this.lblFinish.Visible = false;
            // 
            // pbRecord
            // 
            this.pbRecord.Location = new System.Drawing.Point(12, 261);
            this.pbRecord.Name = "pbRecord";
            this.pbRecord.Size = new System.Drawing.Size(304, 23);
            this.pbRecord.TabIndex = 12;
            this.pbRecord.Visible = false;
            // 
            // lblN
            // 
            this.lblN.AutoSize = true;
            this.lblN.Location = new System.Drawing.Point(12, 205);
            this.lblN.Name = "lblN";
            this.lblN.Size = new System.Drawing.Size(70, 13);
            this.lblN.TabIndex = 13;
            this.lblN.Text = "Параметр c:";
            // 
            // tbC
            // 
            this.tbC.Location = new System.Drawing.Point(194, 205);
            this.tbC.Name = "tbC";
            this.tbC.ReadOnly = true;
            this.tbC.Size = new System.Drawing.Size(122, 20);
            this.tbC.TabIndex = 14;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(12, 290);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(304, 50);
            this.btnStop.TabIndex = 15;
            this.btnStop.Text = "Остановить";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // rbHybrid
            // 
            this.rbHybrid.AutoSize = true;
            this.rbHybrid.Checked = true;
            this.rbHybrid.Location = new System.Drawing.Point(12, 13);
            this.rbHybrid.Name = "rbHybrid";
            this.rbHybrid.Size = new System.Drawing.Size(198, 17);
            this.rbHybrid.TabIndex = 16;
            this.rbHybrid.TabStop = true;
            this.rbHybrid.Text = "Гибридный муравьиный алгоритм";
            this.rbHybrid.UseVisualStyleBackColor = true;
            this.rbHybrid.CheckedChanged += new System.EventHandler(this.rbHybrid_CheckedChanged);
            // 
            // rbSimple
            // 
            this.rbSimple.AutoSize = true;
            this.rbSimple.Location = new System.Drawing.Point(12, 37);
            this.rbSimple.Name = "rbSimple";
            this.rbSimple.Size = new System.Drawing.Size(185, 17);
            this.rbSimple.TabIndex = 17;
            this.rbSimple.Text = "Простой муравьиный алгоритм";
            this.rbSimple.UseVisualStyleBackColor = true;
            this.rbSimple.CheckedChanged += new System.EventHandler(this.rbSimple_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 361);
            this.Controls.Add(this.rbSimple);
            this.Controls.Add(this.rbHybrid);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.tbC);
            this.Controls.Add(this.lblN);
            this.Controls.Add(this.pbRecord);
            this.Controls.Add(this.lblFinish);
            this.Controls.Add(this.tbPherEvapor);
            this.Controls.Add(this.tbPherEffect);
            this.Controls.Add(this.tbAnts);
            this.Controls.Add(this.lblPherEvapor);
            this.Controls.Add(this.lblPherEffect);
            this.Controls.Add(this.lblAnts);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сокращение диагностической информации";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.Label lblAnts;
        private System.Windows.Forms.Label lblPherEffect;
        private System.Windows.Forms.Label lblPherEvapor;
        private System.Windows.Forms.TextBox tbAnts;
        private System.Windows.Forms.TextBox tbPherEffect;
        private System.Windows.Forms.TextBox tbPherEvapor;
        private System.Windows.Forms.Label lblFinish;
        private System.Windows.Forms.ProgressBar pbRecord;
        private System.Windows.Forms.Label lblN;
        private System.Windows.Forms.TextBox tbC;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.RadioButton rbHybrid;
        private System.Windows.Forms.RadioButton rbSimple;
    }
}
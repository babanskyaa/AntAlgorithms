using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using LogicLayer;
using EntityLayer;
using System.Threading;

namespace NewAntProject
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Конструктор,
        /// инициализация элементов формы
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        Thread thread;

        /// <summary>
        /// Экземпляр класса логики
        /// </summary>
        private DILogic diLogic { get; set; }

        /// <summary>
        /// Текущая выбранная маска
        /// </summary>
        private Mask Mask { get; set; }

        /// <summary>
        /// Текущий набор записей в СПР, соответствующий текущей маске
        /// </summary>
        private List<Record> Records { get { return diLogic.GetRecords(Mask); } }

        private void rbSimple_CheckedChanged(object sender, EventArgs e)
        {
            tbC.Enabled = false;
        }

        private void rbHybrid_CheckedChanged(object sender, EventArgs e)
        {
            tbC.Enabled = true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                btnStart.Enabled = true;
                tbAnts.ReadOnly = false;
                tbPherEffect.ReadOnly = false;
                tbPherEvapor.ReadOnly = false;
                tbC.ReadOnly = false;
                diLogic = new DILogic(ofd.FileName);
                MessageBox.Show("Данные успешно загружены!\nВведите параметры для запуска муравьиного алгоритма.\nКоличество муравьев - целое положительное число\nВлияние феромонов - целое положительное число\nСкорость испарения феромонов - число в отрезке [0, 1]\nПараметр c - целое положиельное число");
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (Int32.Parse(tbAnts.Text) > 0 && Double.Parse(tbPherEffect.Text) >= 0 && Double.Parse(tbPherEvapor.Text) >= 0 && Double.Parse(tbPherEvapor.Text) <= 1 && (rbSimple.Checked || Int32.Parse(tbC.Text) > 0))
                {
                    diLogic.SetAnts(Int32.Parse(tbAnts.Text));
                    diLogic.SetPherEffect(Double.Parse(tbPherEffect.Text));
                    diLogic.SetEvaporRate(Double.Parse(tbPherEvapor.Text));
                    diLogic.SetC(0);
                    if (tbC.Text != "")
                        diLogic.SetC(Int32.Parse(tbC.Text));
                    diLogic.ProcessChanged += dilogic_ProcessChanged;
                    diLogic.WorkCompleted += dilogic_WorkCompleted;
                    if (rbHybrid.Checked)
                        thread = new Thread(diLogic.FindHybridAntMasks);
                    if (rbSimple.Checked)
                        thread = new Thread(diLogic.FindSimpleAntMasks);
                    thread.IsBackground = true;
                    thread.Start();
                    rbHybrid.Enabled = false;
                    rbSimple.Enabled = false;
                    btnOpen.Enabled = false;
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                    tbAnts.ReadOnly = true;
                    tbPherEffect.ReadOnly = true;
                    tbPherEvapor.ReadOnly = true;
                    tbC.ReadOnly = true;
                    lblFinish.Text = "Пожалуйста, подождите...";
                    lblFinish.Visible = true;
                    pbRecord.Value = 0;
                    pbRecord.Minimum = 0;
                    pbRecord.Maximum = diLogic.GetAnts();
                    pbRecord.Visible = true;
                }
                else
                {
                    MessageBox.Show("Введенные данные некорректны!\nКоличество муравьев - целое положительное число\nВлияние феромонов - целое положительное число\nСкорость испарения феромонов - число в отрезке [0, 1]\nПараметр c - целое положительное число");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка ввода данных!\nВозможно Вы использовали точку(.) вместо запятой(,) в дробной части. Попробуйте снова.");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            thread.Abort();
            thread.Join(500);
            pbRecord.Value = 0;
            rbHybrid.Enabled = true;
            rbSimple.Enabled = true;
            btnOpen.Enabled = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            tbAnts.ReadOnly = false;
            tbPherEffect.ReadOnly = false;
            tbPherEvapor.ReadOnly = false;
            tbC.ReadOnly = false;
            lblFinish.Visible = false;
            pbRecord.Visible = false;
            MessageBox.Show("Процесс был успешно остановлен!");
        }

        private void dilogic_WorkCompleted(bool ready)
        {
            Action action = () =>
            {
                thread.Abort();
                thread.Join(500);
                pbRecord.Value = 0;
                rbHybrid.Enabled = true;
                rbSimple.Enabled = true;
                btnOpen.Enabled = true;
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                tbAnts.ReadOnly = false;
                tbPherEffect.ReadOnly = false;
                tbPherEvapor.ReadOnly = false;
                tbC.ReadOnly = false;
                lblFinish.Visible = false;
                pbRecord.Visible = false;
                MessageBox.Show("Готово! Результат находится здесь: " + diLogic.GetFileName());
                Process.Start("C:\\Windows\\System32\\notepad.exe", diLogic.GetFileName());
            };
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        private void dilogic_ProcessChanged(int count)
        {
            Action action = () => 
            {
                pbRecord.Value = count;
                lblFinish.Text = "Пожалуйста, подождите... " + Math.Round((double)count*100.0/Double.Parse(pbRecord.Maximum.ToString())) + "%";
            };
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }
    }
}

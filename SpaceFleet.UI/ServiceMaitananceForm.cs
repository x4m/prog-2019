using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceFleet.UI
{
    public partial class ServiceMaitananceForm : Form
    {
        public ServiceMaitananceForm(ServiceMaitanance serviceMaitanance)
        {
            ServiceMaitanance = serviceMaitanance;
            InitializeComponent();
        }

        public ServiceMaitanance ServiceMaitanance { get; set; }

        private void ServiceMaitananceForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = ServiceMaitanance.Description;
            dateTimePicker1.Value = ServiceMaitanance.MaintananceDate;
            if (ServiceMaitanance.NextPlannedSerice.HasValue)
                dateTimePicker2.Value = ServiceMaitanance.NextPlannedSerice.Value;
            else
                dateTimePicker2.Checked = false;
            checkBox1.Checked = ServiceMaitanance.IsPlannedService;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceMaitanance.Description = textBox1.Text;
            ServiceMaitanance.MaintananceDate = dateTimePicker1.Value;
            ServiceMaitanance.IsPlannedService = checkBox1.Checked;
            if (!dateTimePicker2.Checked)
            {
                ServiceMaitanance.NextPlannedSerice = null;
            }
            else
            {
                ServiceMaitanance.NextPlannedSerice = dateTimePicker2.Value;
            }
        }
    }
}

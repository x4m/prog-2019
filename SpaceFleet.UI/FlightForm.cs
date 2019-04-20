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
    public partial class FlightForm : Form
    {
        public FlightForm()
        {
            InitializeComponent();
        }

        public Flight Flight { get; set; }

        private void FlightForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Flight.From;
            textBox2.Text = Flight.To;
            textBox3.Text = Flight.Crew;
            textBox4.Text = Flight.Passengers;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Flight.From = textBox1.Text;
            Flight.To = textBox2.Text;
            Flight.Crew = textBox3.Text;
            Flight.Passengers = textBox4.Text;
        }
    }
}

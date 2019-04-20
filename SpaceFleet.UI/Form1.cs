using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SpaceFleet.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog() { Filter = "Фотография|*.jpg" };
            var dr = ofd.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ff = new FlightForm() { Flight = new Flight() };
            if (ff.ShowDialog(this) == DialogResult.OK)
            {
                listBox1.Items.Add(ff.Flight);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = listBox1.SelectedItem is Flight;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                var item = (Flight)listBox1.Items[index];
                var ff = new FlightForm() { Flight = item };
                if (ff.ShowDialog(this) == DialogResult.OK)
                {
                    listBox1.Items.Remove(item);
                    listBox1.Items.Insert(index, item);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Flight)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var ff = new ServiceMaitananceForm(new ServiceMaitanance{MaintananceDate = DateTime.Now});
            if (ff.ShowDialog(this) == DialogResult.OK)
            {
                listBox2.Items.Add(ff.ServiceMaitanance);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem is ServiceMaitanance)
            {
                listBox2.Items.Remove(listBox2.SelectedItem);
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button4.Enabled = listBox2.SelectedItem is ServiceMaitanance;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog() {Filter = "Корабль|*.spaceship"};

            if (sfd.ShowDialog(this) != DialogResult.OK)
                return;

            var spaceShip = new SpaceShip()
            {
                Build = dateTimePicker1.Value,
                Journal = listBox1.Items.OfType<Flight>().ToList(),
                Name = textBox1.Text,
                ServiceMaintanance = listBox2.Items.OfType<ServiceMaitanance>().ToList(),
            };

            var stream = new MemoryStream();
            pictureBox1.Image.Save(stream, ImageFormat.Jpeg);
            spaceShip.Photo = stream.ToArray();


            switch (comboBox1.SelectedValue?.ToString())
            {
                case "Гражданский транспорт":
                    spaceShip.ShipType = ShipType.Civil;
                    break;
                case "Военный корабль":
                    spaceShip.ShipType = ShipType.Military;
                    break;
                default:
                    spaceShip.ShipType = ShipType.Cargo;
                    break;
            }

            var xs = new XmlSerializer(typeof(SpaceShip));

            var file = File.Create(sfd.FileName);

            xs.Serialize(file, spaceShip);
            file.Close();
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog() { Filter = "Корабль|*.spaceship" };

            if (ofd.ShowDialog(this) != DialogResult.OK)
                return;
            var xs = new XmlSerializer(typeof(SpaceShip));
            var file = File.OpenRead(ofd.FileName);
            var spaceShip = (SpaceShip)xs.Deserialize(file);
            file.Close();

            textBox1.Text = spaceShip.Name;
            dateTimePicker1.Value = spaceShip.Build;
            switch (spaceShip.ShipType)
            {
                case ShipType.Civil:
                    comboBox1.Text = "Гражданский транспорт";
                    break;
                case ShipType.Military:
                    comboBox1.Text = "Военный корабль";
                    break;
                case ShipType.Cargo:
                    comboBox1.Text = "Грузовой";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var ms = new MemoryStream(spaceShip.Photo);
            pictureBox1.Image = Image.FromStream(ms);

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            foreach (var flight in spaceShip.Journal)
            {
                listBox1.Items.Add(flight);
            }
            foreach (var sm in spaceShip.ServiceMaintanance)
            {
                listBox2.Items.Add(sm);
            }
        }
    }
}

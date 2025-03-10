using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Reflection.Emit;
using Microsoft.Win32;



namespace hwi_spoofer
{
    public partial class Main : Form
    {
        private string registerText = "Success Spoof";
        private int registerIndex = 0;
        private bool isRegisterErasing = false;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer rainTimer;
        private List<Raindrop> raindrops = new List<Raindrop>();
        private Random random = new Random();
        private const int raindropCount = 150;

        public Main()
        {
            InitializeComponent();
        }





        private async void Registerbutton_Click(object sender, EventArgs e)
        {
            bunifuCircleProgress1.Value = 0;

            for (int i = 0; i <= 100; i += 1)
            {
                bunifuCircleProgress1.Value = i;

                await Task.Delay(50);
            }

            string tempFilename = Path.ChangeExtension(Path.GetTempFileName(), ".bat");
            using (StreamWriter writer = new StreamWriter(tempFilename))
            {
                writer.WriteLine(@"echo off");
                writer.WriteLine(@"kdmapper.exe s.sys");
                writer.WriteLine(@"cls");
            }

            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = tempFilename;
            processInfo.UseShellExecute = true;
            processInfo.CreateNoWindow = false;

            Process process = Process.Start(processInfo);
            process.WaitForExit();
            File.Delete(tempFilename);
        }


        private void RegisterPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void LoginBTN_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string url = "https://discord.gg/https://discord.gg/7zAPY4BCey";
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void bunifuCircleProgress1_ProgressChanged(object sender, Bunifu.UI.WinForms.BunifuCircleProgress.ProgressChangedEventArgs e)
        {

        }

        private void VanguardPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void CfxrePictureBox_Click(object sender, EventArgs e)
        {

        }

        private void EasyAntiCheatPictoreBox_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            VanguardPictureBox.Visible = false;
            CfxrePictureBox.Visible = false;
            EasyAntiCheatPictoreBox.Visible = false;
            ricochetPicturebox.Visible = false;

            if (guna2ComboBox1.SelectedItem != null)
            {
                string selectedItem = guna2ComboBox1.SelectedItem.ToString();

                if (selectedItem == "Vanguard")
                {
                    VanguardPictureBox.Visible = true;
                }
                else if (selectedItem == "Cfx.re")
                {
                    CfxrePictureBox.Visible = true;
                }
                else if (selectedItem == "Easy Anti-Cheat")
                {
                    EasyAntiCheatPictoreBox.Visible = true;
                }
                else if (selectedItem == "Ricochet")
                {
                    ricochetPicturebox.Visible = true;
                }
            }
        }

        private void ricochetPicturebox_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void MenuPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }


    public class Raindrop
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        public int Length { get; set; }
    }
}

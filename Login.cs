using Bunifu.UI.WinForms.BunifuButton;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hwi_spoofer
{

    public partial class LoginFormMain : Form
    {

        private string loginText = "Welcome Back Please Login";
        private string registerText = "Please Register";

        // Zmienne do animacji dla LoginLabelWelcome
        private int loginIndex = 0;
        private bool isLoginErasing = false;

        // Zmienne do animacji dla RegisterLabel
        private int registerIndex = 0;
        private bool isRegisterErasing = false;
        private System.Windows.Forms.Timer AntiDebugTimer;

        // Lista nazw procesów do wykrycia
        private string[] blockedProcesses = new string[]
        {
            "ollydbg", "x64dbg", "ida", "ida64", "cheatengine",
            "dnspy", "windbg", "gdb", "scylla", "hxd", "softice",
            "procdump", "LastActivityView", "ProcessHacker",
            "ProcessExplorer", "ProcessMonitor", "ProcessLasso",
            "ProcessTamer", "ProcessKO", "ProcessGuard", "ProcessGovernor",
            "ProcessEliminator", "ProcessController", "ProcessActivityView",
            "ProcessAlive", "ProcessAssassin", "ProcessBlocker", "ProcessButler",
            "ProcessCop", "ProcessDoctor", "ProcessEliminator", "ProcessExplorer",
            "ProcessGovernor", "ProcessGuard", "ProcessHacker", "ProcessKiller",
            "ProcessLasso", "ProcessLogger", "ProcessManager", "ProcessMonitor",
            "ProcessPoliceman", "ProcessProtector", "ProcessScanner", "ProcessStalker",
            "ProcessTamer", "ProcessTerminator", "ProcessTool", "ProcessViewer",
            "ProcessWatcher", "ProcessWizard", "ProcessZapper", "ProcessZapper",
            "ProcKill", "ProcMon", "ProcView", "ProcWatch", "ProcX", "PView", "x32dbg",

        };

        public static api KeyAuthApp = new api(
        name: "spoofer hwid",
        ownerid: "xHY2UEMn3n",
        secret: "92cade5d119dddf880b44ce9e7b53569840e1c1a8eef3249097974e7003b2404",
        version: "1.0"
        );

        public LoginFormMain()
        {
            InitializeComponent();
            InitializeTimer();
        }


        private void InitializeTimer()
        {
            // Inicjalizacja timera antydebugowania
            antiDebugTimer = new System.Windows.Forms.Timer();
            antiDebugTimer.Interval = 200; // Timer będzie wykonywał operacje co 200 ms
            antiDebugTimer.Tick += new EventHandler(AnimateText);
            antiDebugTimer.Tick += new EventHandler(CheckForDebuggerProcesses); // Dodano wywołanie funkcji sprawdzającej programy debuggerskie
            antiDebugTimer.Start();
        }

        private void AnimateText(object sender, EventArgs e)
        {
            if (!isLoginErasing)
            {
                loginIndex++;
                if (loginIndex <= loginText.Length)
                {
                    LoginLabelWelcome.Text = loginText.Substring(0, loginIndex);
                }
                else
                {
                    isLoginErasing = true;
                }
            }
            else
            {
                loginIndex--;
                if (loginIndex >= 0)
                {
                    LoginLabelWelcome.Text = loginText.Substring(0, loginIndex);
                }
                else
                {
                    isLoginErasing = false;
                }
            }

            if (!isRegisterErasing)
            {
                registerIndex++;
                if (registerIndex <= registerText.Length)
                {
                    RegisterLabel.Text = registerText.Substring(0, registerIndex);
                }
                else
                {
                    isRegisterErasing = true;
                }
            }
            else
            {
                registerIndex--;
                if (registerIndex >= 0)
                {
                    RegisterLabel.Text = registerText.Substring(0, registerIndex);
                }
                else
                {
                    isRegisterErasing = false;
                }
            }
        }

        // Funkcja do ciągłego sprawdzania programów debuggerskich przy użyciu timera
        private void CheckForDebuggerProcesses(object sender, EventArgs e)
        {
            foreach (string processName in blockedProcesses)
            {
                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Length > 0)
                {
                    // Wyświetlenie komunikatu o wykryciu debuggerskiego programu
                    MessageBox.Show("Debugger detected! The system will shut down.", "Security Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Wyłączenie systemu
                    Process.Start("shutdown", "/s /f /t 0");
                }
            }
        }

        //REGISTER LABEL
        private void RegisterLabel_Click(object sender, EventArgs e)
        {

        }

        //LOGIN LABEL WELCOME ANIMATED
        private void LoginLabelWelcome_Click(object sender, EventArgs e)
        {

        }

        //MAIN FORM
        private void LoginFormMain_Load(object sender, EventArgs e)
        {
            KeyAuthApp.init();

            if (KeyAuthApp.response.message == "invalidver")
            {
                if (!string.IsNullOrEmpty(KeyAuthApp.app_data.downloadLink))
                {
                    DialogResult dialogResult = MessageBox.Show("Yes to open file in browser\nNo to download file automatically", "Auto update", MessageBoxButtons.YesNo);
                    switch (dialogResult)
                    {
                        case DialogResult.Yes:
                            Process.Start(KeyAuthApp.app_data.downloadLink);
                            Environment.Exit(0);
                            break;
                        case DialogResult.No:
                            WebClient webClient = new WebClient();
                            string destFile = Application.ExecutablePath;

                            string rand = random_string();

                            destFile = destFile.Replace(".exe", $"-{rand}.exe");
                            webClient.DownloadFile(KeyAuthApp.app_data.downloadLink, destFile);

                            Process.Start(destFile);
                            Process.Start(new ProcessStartInfo()
                            {
                                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                                WindowStyle = ProcessWindowStyle.Hidden,
                                CreateNoWindow = true,
                                FileName = "cmd.exe"
                            });
                            Environment.Exit(0);
                            break;
                        default:
                            MessageBox.Show("Invalid option");
                            Environment.Exit(0);
                            break;
                    }
                }
                MessageBox.Show("Posiadasz starą wersję programu, pobierz nową");
                Thread.Sleep(2500);
                Environment.Exit(0);
            }

            if (!KeyAuthApp.response.success)
            {
                MessageBox.Show(KeyAuthApp.response.message);
                Environment.Exit(0);
            }
            KeyAuthApp.check();
        }

        static string random_string()
        {
            string str = null;

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                str += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
            }
            return str;
        }

        //MENU PANEL MAIN
        private void MenuPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        //LOGIN BUTTON
        private void LoginBTN_Click(object sender, EventArgs e)
        {
            LoginPanel.Visible = true;
            RegisterPanel.Visible = false;
        }

        //LOOGIN PANEL
        private void LoginPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        //REGISTER BUTTON
        private void RegisterBTN_Click(object sender, EventArgs e)
        {
            RegisterPanel.Visible = true;
            LoginPanel.Visible = false;
        }

        //REGISTER PANEL
        private void RegisterPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        //USERNAME TEXBOX
        private void UsernameTexBox_TextChanged(object sender, EventArgs e)
        {

        }

        //PASSWORD TEXBOX
        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        //LICENSE TEXTBOX
        private void LicenseTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void UsernameTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Launchbutton_Click(object sender, EventArgs e)
        {
            KeyAuthApp.login(UsernameTexBox.Text, PasswordTextBox.Text);
            if (KeyAuthApp.response.success)
            {
                Main mainForm = new Main();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                Guna.UI2.WinForms.Guna2MessageDialog messageDialog = new Guna.UI2.WinForms.Guna2MessageDialog();
                messageDialog.Text = "Username or password is invalid!";
                messageDialog.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                messageDialog.Style = Guna.UI2.WinForms.MessageDialogStyle.Dark;

                messageDialog.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                messageDialog.Parent = this;

                messageDialog.Show();
            }
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

        private void bunifuButton2_Click(object sender, EventArgs e)
        {

        }

        private void launchbutton_Click_1(object sender, EventArgs e)
        {
            KeyAuthApp.login(UsernameTexBox.Text, PasswordTextBox.Text);
            if (KeyAuthApp.response.success)
            {
                Main mainForm = new Main();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                Guna.UI2.WinForms.Guna2MessageDialog messageDialog = new Guna.UI2.WinForms.Guna2MessageDialog();
                messageDialog.Text = "Username or password is invalid!";
                messageDialog.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                messageDialog.Style = Guna.UI2.WinForms.MessageDialogStyle.Dark;

                messageDialog.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                messageDialog.Parent = this;

                messageDialog.Show();
            }
        }

        private void Registerbutton_Click(object sender, EventArgs e)
        {
            if (Registerbutton.IdleBorderThickness == 0)
            {
                Guna.UI2.WinForms.Guna2MessageDialog messageDialog = new Guna.UI2.WinForms.Guna2MessageDialog();
                messageDialog.Text = "To register please fill in\nusername, password and license\n(Click the button again)";
                messageDialog.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                messageDialog.Style = Guna.UI2.WinForms.MessageDialogStyle.Dark;
                messageDialog.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                messageDialog.Parent = this;
                messageDialog.Show();
            }
            else
            {
                KeyAuthApp.register(UsernameTextBox2.Text, PasswordTextBox2.Text, LicenseTextBox.Text);
                if (KeyAuthApp.response.success)
                {
                    Main main = new Main();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    Guna.UI2.WinForms.Guna2MessageDialog messageDialog = new Guna.UI2.WinForms.Guna2MessageDialog();
                    messageDialog.Text = "License is invalid!";
                    messageDialog.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    messageDialog.Style = Guna.UI2.WinForms.MessageDialogStyle.Dark;
                    messageDialog.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                    messageDialog.Parent = this;
                    messageDialog.Show();
                }
            }
        }
    }
}

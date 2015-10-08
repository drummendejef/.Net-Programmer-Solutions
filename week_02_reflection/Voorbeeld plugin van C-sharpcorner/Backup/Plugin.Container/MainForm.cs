using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plugin.Interface;
using System.Reflection;
using System.IO;

namespace Plugin.Container
{
    public partial class MainForm : Form
    {
        private PluginLoader _pluginLoader = new PluginLoader();

        public MainForm()
        {
            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string path = GetExecutionFolder();
            var plugins = _pluginLoader.LoadPlugins(path);

            if (plugins.Count == 0)
                MessageBox.Show("No Plugins found!");

            else
            {
                foreach (IPlugin plugin in plugins)
                {
                    Button button = new Button() { Width = 200, Height = 40, Left = 2, Top = PluginsPanel.Controls.Count * 40};
                    button.Text = plugin.Text;
                    button.BackColor = plugin.BackColor;
                    button.Tag = plugin;
                    button.Click += new EventHandler(button_Click);
                    PluginsPanel.Controls.Add(button);
                }
            }

        }

        void button_Click(object sender, EventArgs e)
        {
            IPlugin plugin = (sender as Button).Tag as IPlugin;
            plugin.Form.Show();
        }

        public string GetExecutionFolder()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

    }
}

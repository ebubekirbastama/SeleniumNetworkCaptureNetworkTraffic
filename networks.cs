using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V108.Network;
using System;
using System.Threading;
using System.Windows.Forms;
using DevToolsSessionDomains = OpenQA.Selenium.DevTools.V108.DevToolsSessionDomains;

namespace ebsproject
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        Thread th;
        private void button1_Click(object sender, EventArgs e)
        {
            th = new Thread(Kod);th.Start();
       
        }
        public async void Kod()
        {
            // Tarayıcıyı açın ve DevTools özelliklerine erişin
            var driver = new ChromeDriver();
            IDevTools devTools = driver as IDevTools;
            IDevToolsSession session = devTools.GetDevToolsSession();
            var domains = session.GetVersionSpecificDomains<DevToolsSessionDomains>();

            // Ağ isteklerini dinleyen bir dinleyici ekleyin
            domains.Network.ResponseReceived += ResponseReceivedHandler;

            // Ağ isteklerini etkinleştirin
            await domains.Network.Enable(new EnableCommandSettings());

            // Bir URL'ye gidin ve tarama işlemini başlatın
            driver.Navigate().GoToUrl("https://www.google.com");
        }

        public void ResponseReceivedHandler(object sender, ResponseReceivedEventArgs e)
        {
            // Ağ isteğine ait bilgileri alın ve ekrana yazdırın
            listBox1.Items.Add($"Status: {e.Response.Status} : {e.Response.StatusText} | File: {e.Response.MimeType} | Url: {e.Response.Url}");
        }


    }
}

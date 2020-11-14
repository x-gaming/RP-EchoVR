using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

namespace EchoRPService {
    public partial class EchoRPService : ServiceBase {
        private Timer timer;
        public EchoRPService() {
            InitializeComponent();
        }
        protected override void OnStart(string[] args) {
            timer = new Timer(5000);
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }
        protected override void OnStop() {
            timer.Stop();
        }
        private void OnTimer(object sender, ElapsedEventArgs e) {
            if (IsRunning("echovr") & !IsRunning("RP_EchoVR")) {
                using (Process process = new Process()) {
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.FileName = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\RP_EchoVR.exe");
                    process.StartInfo.Arguments = "service";
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                }
            }
        }
        private bool IsRunning(string name) {
            return Process.GetProcessesByName(name).Length > 0;
        }
    }
}

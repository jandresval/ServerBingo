using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ServicioServerBingo
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_BeforeInstall(object sender, InstallEventArgs e)
        {
            try
            {
                ServiceController controller = ServiceController.GetServices().Where
                (s => s.ServiceName == serviceInstaller1.ServiceName).FirstOrDefault();
                if (controller != null)
                {
                    if ((controller.Status != ServiceControllerStatus.Stopped) &&
                    (controller.Status != ServiceControllerStatus.StopPending))
                    {
                        controller.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Configuration.Install.InstallException
                    (ex.Message.ToString());
            }
        }

        private void serviceInstaller1_BeforeUninstall(object sender, InstallEventArgs e)
        {
            try
            {
                ServiceController controller = ServiceController.GetServices().Where
                (s => s.ServiceName == serviceInstaller1.ServiceName).FirstOrDefault();
                if (controller != null)
                {
                    if ((controller.Status != ServiceControllerStatus.Stopped) &&
                    (controller.Status != ServiceControllerStatus.StopPending))
                    {
                        controller.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Configuration.Install.InstallException
                    (ex.Message.ToString());
            }
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            new ServiceController(serviceInstaller1.ServiceName).Start();
        }



        

    }
}

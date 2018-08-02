namespace MailService
{
    partial class KamaMailService
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.KamaMailServiceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.KamaMailServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // KamaMailServiceProcessInstaller1
            // 
            this.KamaMailServiceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.KamaMailServiceProcessInstaller1.Password = null;
            this.KamaMailServiceProcessInstaller1.Username = null;
            // 
            // KamaMailServiceInstaller
            // 
            this.KamaMailServiceInstaller.DisplayName = "KamaMailService";
            this.KamaMailServiceInstaller.ServiceName = "KamaMailService";
            // 
            // KamaMailService
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.KamaMailServiceProcessInstaller1,
            this.KamaMailServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller KamaMailServiceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller KamaMailServiceInstaller;
    }
}
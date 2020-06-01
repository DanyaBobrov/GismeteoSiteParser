namespace WindowsServiceGismeteoHost
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.GismeteoServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.GismeteoHostService = new System.ServiceProcess.ServiceInstaller();
            // 
            // GismeteoServiceProcessInstaller
            // 
            this.GismeteoServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.GismeteoServiceProcessInstaller.Password = null;
            this.GismeteoServiceProcessInstaller.Username = null;
            // 
            // GismeteoHostService
            // 
            this.GismeteoHostService.Description = "Сервис для работы с базой погоды";
            this.GismeteoHostService.ServiceName = "GismeteoHostService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.GismeteoServiceProcessInstaller,
            this.GismeteoHostService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller GismeteoServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller GismeteoHostService;
    }
}
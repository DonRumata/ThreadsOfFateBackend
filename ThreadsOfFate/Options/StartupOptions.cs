
namespace ThreadsOfFate.Options
{
    /// <summary>
    /// Конфигурация приложения, зависит от среды выполнения
    /// </summary>
    public class StartupOptions
    {
        public bool UseCors { get; set; }
        public string[] Origins { get; set; }
    }
}

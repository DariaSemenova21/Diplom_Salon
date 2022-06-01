using System.Windows;

namespace Salon.Models
{
    public partial class Entries
    {
        public string DateString => start_datetime.ToString("dd MMMM");
        public string StartTimeString => start_datetime.ToString("HH:mm");
        public string EndTimeString => end_datetime == null ? "Не завершено" : end_datetime.Value.ToString("HH:mm");

        public Visibility ButtonIsEnded => end_datetime != null ? Visibility.Collapsed : Visibility.Visible;
        public Visibility TextBlockIsEnded => end_datetime != null ? Visibility.Visible : Visibility.Collapsed;
    }
}

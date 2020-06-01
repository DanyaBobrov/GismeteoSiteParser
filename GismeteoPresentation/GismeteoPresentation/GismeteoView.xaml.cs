using System.Windows;

namespace GismeteoPresentation
{
    public partial class GismeteoView : Window
    {
        public GismeteoView()
        {
            InitializeComponent();
            DataContext = new GismeteoViewModel();
            (DataContext as GismeteoViewModel).Init();
        }
    }
}

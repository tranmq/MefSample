using System.Windows;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Registration;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Business.Entities;
using Business.Validator;

namespace MefProtype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CompositionContainer _container;
        private readonly Person _model = new Person();

        public IValidateEntity<Person> PersonValidator { get; set; } 

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _model;
            RegisterMefParts();
        }

        private void RegisterMefParts()
        {
            var conventions = new RegistrationBuilder();
            //conventions.ForTypesDerivedFrom<IValidateEntity<Person>>().Export<IValidateEntity<Person>>();
            conventions.ForType<MainWindow>().ImportProperty(x => x.PersonValidator, y => y.AsContractName("abc")); // contract with name

            Assembly businessAssembly = typeof (IValidateEntity<>).Assembly;
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new AssemblyCatalog(businessAssembly, conventions));

            _container = new CompositionContainer(catalog,
                                                  CompositionOptions.DisableSilentRejection |
                                                  CompositionOptions.IsThreadSafe);

            try
            {
                _container.SatisfyImportsOnce(this, conventions);
            }
            catch (CompositionException ex)
            {
                MessageBox.Show((ex.Message));
            }
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            Status.Text = PersonValidator.Validate(_model) ? "Person is valid." : "Person isn't valid.";
        }
    }
}

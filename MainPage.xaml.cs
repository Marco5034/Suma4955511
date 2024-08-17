namespace Suma4955511
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editResultadoId;

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listview.ItemsSource = await _dbService.GetResultado());


        }

        private async void sumarBtn_Clicked(object sender, EventArgs e)
        {
            if (_editResultadoId == 0)
            {

                Sumar suma = new Sumar();
                suma.Numero1 = Convert.ToInt32(EntryPrimerNumero.Text);
                suma.Numero2 = Convert.ToInt32(EntrySegundoNumero.Text);

                var SumaTotal = suma.RealizarSuma();


                await _dbService.Create(new Resultado
                {
                    Numero1 = EntryPrimerNumero.Text,
                    Numero2 = EntrySegundoNumero.Text,
                    Suma = SumaTotal.ToString(),
                  


                });
                _editResultadoId = 0;

            }
            else
            {
                Sumar sumaActualizacion = new Sumar();
                sumaActualizacion.Numero1 = Convert.ToInt32(EntryPrimerNumero.Text);
                sumaActualizacion.Numero2 = Convert.ToInt32(EntrySegundoNumero.Text);

                var SumaTotalActulizar = sumaActualizacion.RealizarSuma();


                await _dbService.Update(new Resultado
                {
                    Id = _editResultadoId,
                    Numero1 = EntryPrimerNumero.Text,
                    Numero2 = EntrySegundoNumero.Text,
                    Suma = SumaTotalActulizar.ToString(),

                });
               EntryPrimerNumero.Text = string.Empty;
               EntrySegundoNumero.Text = string.Empty;
                labelresultado.Text = string.Empty;
               

            }
            listview.ItemsSource = await _dbService.GetResultado();

        }

        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var resultado = (Resultado)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null,"Edit","Delete");


            switch (action)
            {
                case "Edit":
                    _editResultadoId = resultado.Id;
                    EntryPrimerNumero.Text = resultado.Numero1;
                    EntrySegundoNumero.Text = resultado.Numero2;
                    
                    labelresultado.Text = resultado.Suma;
                   
                    break;

                case "Delete":
                    await _dbService.Delete(resultado);
                    listview.ItemsSource = await _dbService.GetResultado();
                    break;
            }
        }


    }

}

using EvaluacionP2OntanedaT.Modelo;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EvaluacionP2OntanedaT;

public partial class PrimeraPagina : ContentPage
{
	Recarga recarga = new Recarga();
    public PrimeraPagina()
	{
		InitializeComponent();

        recarga = DevuelveRecarga();
        BindingContext = recarga;
    }

    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "recarga.txt");

    private async void Button_Clicked(object sender, EventArgs e)
    {

        Recarga recarga = new Recarga
        {
            Numero = OntanedaTNum.Text,
            Nombre = OntanedaTNom.Text
        };

        bool guardar_recarga = CrearRecarga(recarga);

        if (guardar_recarga)
        {
            await DisplayAlert("La recarga fue exitosa", "Recarga exitosa.", "OK");
        }
        else
        {
            await DisplayAlert("Recarga no se efectuó", "Recarga fallida.", "Fallo");
        }
    }

    private bool CrearRecarga(Recarga recarga)
    {
        try
        {
            string datos = JsonConvert.SerializeObject(recarga);
            File.WriteAllText(_fileName, datos);
            return true;
        }
        catch { throw; }
    }

    private Recarga DevuelveRecarga()
    {
        Recarga recarga = new Recarga();

        if (File.Exists(_fileName))
        {
            string datos = File.ReadAllText(_fileName);
            recarga = JsonConvert.DeserializeObject<Recarga>(datos);
        }
        return recarga;
    }
}
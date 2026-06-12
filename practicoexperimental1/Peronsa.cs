public class Persona
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string NumeroTelefono { get; set; }

    public Persona(string nombre, string apellido, string numeroTelefono)
    {
        Nombre = nombre;
        Apellido = apellido;
        NumeroTelefono = numeroTelefono;
    }
}
public class Agenda
{
    private Persona[] contactos;
    private int cantidad;

    public Agenda()
    {
        contactos = new Persona[100]; // capacidad máxima
        cantidad = 0;
    }

    public void AgregarPersona(Persona persona)
    {
        if (cantidad < contactos.Length)
        {
            contactos[cantidad] = persona;
            cantidad++;
        }
    }

    public void EliminarPersona(string nombre)
    {
        for (int i = 0; i < cantidad; i++)
        {
            if (contactos[i].Nombre == nombre)
            {
                for (int j = i; j < cantidad - 1; j++)
                {
                    contactos[j] = contactos[j + 1];
                }
                contactos[cantidad - 1] = null;
                cantidad--;
                break;
            }
        }
    }

    public void MostrarContactos()
    {
        for (int i = 0; i < cantidad; i++)
        {
            Console.WriteLine(
                $"Nombre: {contactos[i].Nombre}, " +
                $"Apellido: {contactos[i].Apellido}, " +
                $"Teléfono: {contactos[i].NumeroTelefono}");
        }
    }
}

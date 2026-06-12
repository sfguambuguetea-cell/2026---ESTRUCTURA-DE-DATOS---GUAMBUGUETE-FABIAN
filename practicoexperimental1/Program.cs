Agenda agenda = new Agenda();

agenda.AgregarPersona(new Persona("fabian", "Guambuguete", "0990947613"));
agenda.AgregarPersona(new Persona("adamariz", "rodriguez", "0990946574"));

Console.WriteLine("Lista de contactos:");
agenda.MostrarContactos();

agenda.EliminarPersona("segundo");

Console.WriteLine("\nDespués de eliminar:");
agenda.MostrarContactos();
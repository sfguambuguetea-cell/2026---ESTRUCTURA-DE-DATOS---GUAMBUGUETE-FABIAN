using System;
using System.Collections.Generic;

// Clase Persona
class Persona
{
    public string Nombre { get; set; }

    public Persona(string nombre)
    {
        Nombre = nombre;
    }
}

// Clase Atraccion
class Atraccion
{
    private Queue<Persona> cola = new Queue<Persona>();
    private const int CAPACIDAD = 30;

    // Registrar la llegada de una persona
    public void LlegadaPersona(Persona persona)
    {
        if (cola.Count < CAPACIDAD)
        {
            cola.Enqueue(persona);
            Console.WriteLine($"{persona.Nombre} ocupa el asiento #{cola.Count}");
        }
        else
        {
            Console.WriteLine($"No hay asientos disponibles para {persona.Nombre}");
        }
    }

    // Iniciar la atracción
    public void IniciarAtraccion()
    {
        if (cola.Count == CAPACIDAD)
        {
            Console.WriteLine("\n=====================================");
            Console.WriteLine("TODOS LOS ASIENTOS HAN SIDO VENDIDOS");
            Console.WriteLine("La atracción comienza...\n");

            int numero = 1;

            while (cola.Count > 0)
            {
                Persona persona = cola.Dequeue();

                Console.WriteLine($"{numero}. {persona.Nombre} sube a la atracción.");

                numero++;
            }

            Console.WriteLine("\nTodos los pasajeros disfrutaron de la atracción.");
        }
        else
        {
            Console.WriteLine($"\nAún quedan asientos disponibles.");
            Console.WriteLine($"Asientos ocupados: {cola.Count}/{CAPACIDAD}");
        }
    }
}

// Programa Principal
class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Parque de Diversiones";

        Atraccion montañaRusa = new Atraccion();

        Console.WriteLine("=====================================");
        Console.WriteLine("   PARQUE DE DIVERSIONES - UEA");
        Console.WriteLine(" Asignación de 30 asientos (FIFO)");
        Console.WriteLine("=====================================\n");

        // Llegan 30 personas
        for (int i = 1; i <= 30; i++)
        {
            Persona persona = new Persona("Persona " + i);
            montañaRusa.LlegadaPersona(persona);
        }

        // Intento de registrar una persona más
        montañaRusa.LlegadaPersona(new Persona("Persona 31"));

        // Iniciar la atracción
        montañaRusa.IniciarAtraccion();

        Console.WriteLine("\nPresione una tecla para salir...");
        Console.ReadKey();
    }
}
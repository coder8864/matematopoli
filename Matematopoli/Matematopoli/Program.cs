using System;

public class Matematopoli
{
    // Costanti che definiscono il numero di serrature da aprire, il massimo numero di tentativi per ogni equazione, il range di valori generabili, e l'offset.
    private const int NUMERO_DI_SERRATURE = 7;
    private const int MAX_TENTATIVI = 3;
    private const int RANGE = 21;
    private const int OFFSET = 10;

    public static void Main(string[] args)
    {
        // Messaggio di benvenuto e istruzioni
        Console.WriteLine("Benvenuto a Matematopoli!");
        Console.WriteLine("Risolvi le seguenti equazioni per aprire la cassaforte.");

        // Istanza di Random per generare numeri casuali
        Random random = new Random();
        int livelloDifficolta = 1; // Inizializzazione del livello di difficoltà
        int serratureAperte = 0;   // Contatore di serrature aperte

        // Ciclo principale che continua finché non si aprono tutte le serrature
        while (serratureAperte < NUMERO_DI_SERRATURE)
        {
            int tipoEquazione = random.Next(2); // Genera casualmente il tipo di equazione: 0 per lineare, 1 per quadratica
            int a = GeneraNumeroCasualeDiversoDaZero(random, livelloDifficolta); // Genera il coefficiente a, diverso da zero
            int b = random.Next(RANGE * livelloDifficolta) - OFFSET * livelloDifficolta; // Genera il coefficiente b con offset e livello
            int c = tipoEquazione == 1 ? random.Next(RANGE * livelloDifficolta) - OFFSET * livelloDifficolta : 0; // c è zero per equazioni lineari

            // Se l'equazione è risolta correttamente, incrementa le serrature aperte e il livello di difficoltà
            if (RisolviEquazione(random, tipoEquazione, a, b, c, livelloDifficolta))
            {
                serratureAperte++;
                Console.WriteLine($"Equazione corretta! La serratura {serratureAperte} si apre.");
                livelloDifficolta++; // Aumenta la difficoltà
            }
            else
            {
                // Se l'equazione è sbagliata dopo tre tentativi, resetta serrature e livello
                Console.WriteLine("Hai esaurito i tuoi tentativi. Il gioco si riavvia e tutte le serrature si chiudono.");
                serratureAperte = 0;
                livelloDifficolta = 1;
            }
        }

        // Messaggio finale di successo
        Console.WriteLine("Complimenti! Hai aperto tutte le serrature della cassaforte!");
        Console.WriteLine("Fine del gioco!");
    }

    // Metodo per generare un numero casuale diverso da zero
    private static int GeneraNumeroCasualeDiversoDaZero(Random random, int livelloDifficolta)
    {
        int numero;
        do
        {
            numero = random.Next(RANGE * livelloDifficolta) - OFFSET * livelloDifficolta; // Genera il numero con un certo range e offset
        } while (numero == 0); // Continua a generare finché il numero non è diverso da zero
        return numero;
    }

    // Metodo per risolvere l'equazione e verificare la risposta dell'utente
    private static bool RisolviEquazione(Random random, int tipoEquazione, int a, int b, int c, int livelloDifficolta)
    {
        for (int tentativi = 0; tentativi < MAX_TENTATIVI; tentativi++)
        {
            try
            {
                if (tipoEquazione == 0) // Se l'equazione è lineare
                {
                    Console.Write($"Inserisci il risultato dell'equazione {a}x + {b} = 0: ");
                    double risultato = Convert.ToDouble(Console.ReadLine());
                    double soluzione = -b / (double)a; // Calcola la soluzione
                    if (Math.Abs(risultato - soluzione) < 0.01) // Controlla la correttezza con un margine di errore
                    {
                        return true;
                    }
                }
                else // Se l'equazione è quadratica
                {
                    Console.Write($"Inserisci UNA delle soluzioni dell'equazione {a}x^2 + {b}x + {c} = 0: ");
                    double risultato = Convert.ToDouble(Console.ReadLine());
                    double delta = b * b - 4 * a * c; // Calcola il discriminante
                    if (delta >= 0) // Se ci sono soluzioni reali
                    {
                        double soluzione1 = (-b + Math.Sqrt(delta)) / (2 * a);
                        double soluzione2 = (-b - Math.Sqrt(delta)) / (2 * a);
                        if (Math.Abs(risultato - soluzione1) < 0.01 || Math.Abs(risultato - soluzione2) < 0.01) // Verifica una delle due soluzioni
                        {
                            return true;
                        }
                    }
                    else
                    {
                        // Messaggio nel caso non ci siano soluzioni reali
                        Console.WriteLine("Non ci sono soluzioni reali per questa equazione. Riprova.");
                    }
                }
                Console.WriteLine("Equazione errata. Riprova."); // Messaggio di errore generico
            }
            catch (FormatException)
            {
                // Gestisce eventuali errori di input non numerico
                Console.WriteLine("Inserisci un numero valido.");
            }
        }
        return false; // Restituisce false se l'utente ha esaurito i tentativi
    }
}





using ClubeLeitura.ConsoleApp.Compartilhado;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloReserva
{
    public class RepositorioReserva : RepositorioBase<Reserva>
    {
        public override string Inserir(Reserva reserva)
        {
            reserva.numero = ++contadorNumero;

            reserva.Abrir();

            registros.Add(reserva);

            return "REGISTRO_VALIDO";
        }

        public Reserva[] SelecionarReservasEmAberto()
        {
            List<Reserva> reservasInseridas = new List<Reserva>();

            foreach (Reserva reserva in registros)
                if (reserva.estaAberta)
                    reservasInseridas.Add(reserva);

            return reservasInseridas.ToArray();
        }
    }
}

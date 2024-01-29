using OrdemServico.Interfaces;
using OrdemServico.Model;
using OrdemServico.ViewModel;

namespace OrdemServico.Endpoints
{
    public static class MapTicketEndpoints 
    {
        public static void TicketEndpoints(this IEndpointRouteBuilder app){
            app.MapGet("/", async (ITicketRepository _ticketRepository)=> {
                var tickets = await _ticketRepository.GetAll();
                return Results.Ok(tickets);
            });

            app.MapGet("/{id}", async (ITicketRepository _ticketRepository, int id) => {
                Ticket ticket = await _ticketRepository.GetById(id);
                if(ticket != null){
                  return Results.Ok(ticket);
                }

               return Results.NotFound();
            });

            app.MapPost("/", async (ITicketRepository _ticketRepository, TicketRequet ticket) => {
                Ticket newTicket = new (ticket.EquipamentoId, ticket.Observacao);

                await _ticketRepository.Create(newTicket);
                return Results.Created();
            });

            app.MapDelete("/", async (ITicketRepository _ticketRepository, int id)=> {
                await _ticketRepository.Delete(id);

                return Results.NoContent();
            });
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
        public Guid Id {get; set;}

        public string Title {get; set;}

        public string Description { get; set; }

        public string Category {get; set;}

        public DateTime? Date {get; set;}

        public string City {get; set;}

        public string Venue {get; set;}

        }

        class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                
                var activity = await _context.Activities.FindAsync(request.Id);

                    activity.Title = request.Title ?? activity.Title;
                    activity.Category = request.Category ?? activity.Category;
                    activity.Venue = request.Venue ?? activity.Venue;
                    activity.City = request.City ?? activity.City;
                    activity.Description = request.Description ?? activity.Description;
                    activity.Date = request.Date ?? activity.Date;


                if(activity == null) throw new Exception("Not found.");

                var success = await _context.SaveChangesAsync() > 0;
                if (success)
                {
                    return Unit.Value;
                };
                throw new Exception("Problem saving changes.");
            }
        }
    }
}
using System;

namespace DocumentManagement.Cqrs.Commands;

public interface ICommand<out T>;

public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand<TResult>
{
    Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
}


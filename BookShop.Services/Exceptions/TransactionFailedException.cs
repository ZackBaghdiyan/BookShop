﻿namespace BookShop.Services.Exceptions;

public class TransactionFailedException : Exception
{
    public TransactionFailedException()
    {
    }

    public TransactionFailedException(string message)
        : base(message)
    {
    }

    public TransactionFailedException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

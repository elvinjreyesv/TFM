﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TOURISM.App.Models.Enums
{
    public enum EAccessResponse
    {
        InvalidInput,
        Error,
        UnableToProcess,
        UnhandledError,
        Success,
        DisabledAccount,
        AccountNotFound,
        InvalidCredentials
    }
}

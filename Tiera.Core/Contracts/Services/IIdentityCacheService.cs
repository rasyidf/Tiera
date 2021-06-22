using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Identity.Client;

namespace Tiera.Core.Contracts.Services
{
    public interface IIdentityCacheService
    {
        void SaveMsalToken(byte[] token);

        byte[] ReadMsalToken();
    }
}

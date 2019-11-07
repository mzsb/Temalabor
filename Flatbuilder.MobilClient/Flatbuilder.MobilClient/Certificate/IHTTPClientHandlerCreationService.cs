using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Fb.MC.Certificate
{
    public interface IHTTPClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}

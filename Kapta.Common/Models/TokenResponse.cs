using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapta.Common.Models
{
    using System;
    using Newtonsoft.Json;

    public class TokenResponse
    {
        //mapeamos las propiedades
        //recibo como acces_token y lo cambio para manearlo con AcessToken
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = ".issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(PropertyName = ".expires")]
        public DateTime Expires { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }
    }
}
